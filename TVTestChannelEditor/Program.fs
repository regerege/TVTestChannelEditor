namespace TVTestChannelEditor

open System
open System.Collections.ObjectModel
open System.ComponentModel
open System.Windows
open System.Windows.Controls
open System.Windows.Markup
open System.Windows.Media
open System.Windows.Input
open System.IO
open TVTestChannelEditor

/// コピー先チューナー選択画面
type SelectionTunerModel(tuners:TunerList) as x =
    /// 親画面で選択中のチューナー
    let mutable _tuner : TunerInfo = new TunerInfo("")
    /// 選択チューナー以外のチューナー配列
    let mutable _tunerArr : TunerInfo[] = Array.empty
    /// 選択アイテム
    let mutable _items : TunerInfo[] = Array.empty
    
    // コントロール
    let _wpf = Application.LoadComponent(new Uri("SelectionTuner.xaml", System.UriKind.Relative)) :?> Window
    let _lvtuners = _wpf.FindName("livTuners") :?> ListView
    let _button = _wpf.FindName("btnSelected") :?> Button

    /// コンストラクタ
    do
        _button.Click.Add(fun _ ->
            if 0 < _lvtuners.SelectedItems.Count then
                _items <- [| for o in _lvtuners.SelectedItems -> o :?> TunerInfo |]
            _wpf.Close())
        _lvtuners.Focus() |> ignore
        _wpf.DataContext <- x

    /// 選択リスト用チューナーリストの設定または取得
    member x.Tuners = _tunerArr
    /// 選択中のチューナー
    member x.SelectTuner
        with get() : TunerInfo = _tuner
        and set v =
            _tuner <- v
            let name = v.TunerName
            let tunerType = name.[0]
            let list =
                tuners.Tuners
                |> Seq.filter(fun t -> t <> v && t.TunerName.[0] = tunerType)
                |> Seq.toArray
            _tunerArr <- list
    /// 選択チューナーリスト
    member x.SelectTuners = _items
    /// 画面インスタンスを取得
    member x.Window = _wpf

type ChannelEditorModel(wpf:Window) as x =
    inherit ViewModelBase()

    let mutable _tuners = TunerList.ReadConfig(@"..\..\BonDriver_ptmr.ch2")
    let mutable _selectedTabIndex = -1
    let mutable _selectedIndex = -1
    let mutable _selectedChannle = ChannelInfo.Create()

    // コントロール
    let _tabTuner = wpf.FindName("tabTuner") :?> TabControl
    let _editControls = [
            wpf.FindName("txtChannelName") :?> Control
            wpf.FindName("txtTuner") :?> Control
            wpf.FindName("txtTPID") :?> Control
            wpf.FindName("txtRCNM") :?> Control
            wpf.FindName("txtServiceID") :?> Control
            wpf.FindName("txtNetworkID") :?> Control
            wpf.FindName("txtTSID") :?> Control
            wpf.FindName("chkEnabled") :?> Control
        ]
    let mutable _editGrid : DataGrid = null

    /// コンストラクタ
    do
        // デフォルト選択アイテム
        x.SelectedChannel <- x.Tuners.[0].Channels.[0]
        // TabControl.SelectionChangedイベント
        _tabTuner.SelectionChanged.Add(fun _ ->
            let c = Commons.GetChildElement _tabTuner typeof<DataGrid>
            if c.IsSome then _editGrid <- c.Value :?> DataGrid)
        // Window Loadedイベント
        wpf.Loaded.Add(fun _ ->
            x.SelectedTabIndex <- 0
            x.SelectedIndex <- 0
            _editControls.[0].Focus() |> ignore)
        // バインド
        wpf.DataContext <- x

    /// 読み込み後のチューナーリスト
    member x.Tuners : ObservableCollection<TunerInfo> = _tuners.Tuners
    member x.EditGrid = _editGrid
    /// 現在選択チューナーIndexの取得または設定
    member x.SelectedTabIndex
        with get() = _selectedTabIndex
        and set v =
            _selectedTabIndex <- v
            x.OnPropertyChanged(<@ x.SelectedTabIndex @>)
    /// 現在選択チャンネルIndexの取得または設定
    member x.SelectedIndex
        with get() = _selectedIndex
        and set v =
            _selectedIndex <- v
            x.OnPropertyChanged(<@ x.SelectedIndex @>)
    /// 現在選択しているチャンネルの取得または設定
    member x.SelectedChannel
        with get() = _selectedChannle
        and set v =
            _selectedChannle <- v
            x.OnPropertyChanged(<@ x.SelectedChannel @>)
    /// ウィンドウコマンド
    member x.KeyCommand =
        Commons.CreateCommand (fun _ -> true)
            (fun o ->
                let gt = o :?> string
                match gt with
                | "Ctrl+S" -> _tuners.Save("test.ch2")
                | "Ctrl+Alt+C" ->
                    let subwind = new SelectionTunerModel(_tuners)
                    subwind.Window.Owner <- wpf
                    let f = x.SelectedTabIndex
                    let st = x.Tuners.[f]
                    let nm = st.TunerName
                    subwind.SelectTuner <- st
                    let b = subwind.Window.ShowDialog()
                    if b.HasValue || b.Value then
                        let tuners = subwind.SelectTuners
                        if 0 < tuners.Length then
                            tuners
                            |> Array.iter (fun t ->
                                st.CopyWriteChannels
                                    <| (t,t.Channels.[0].TunerID)
                            )
                            let tuners =
                                tuners
                                |> Array.fold(fun s t ->
                                    s
                                    + (if String.IsNullOrEmpty(s) then "" else ", ")
                                    + t.TunerName) ""
                            MessageBox.Show(sprintf "%s から %s にチャンネルリスト複製しました。" nm tuners)
                            |> ignore
                | _ -> ())
    /// チャンネルの追加と削除と移動
    member x.ChannelCommand =
        Commons.CreateCommand (fun _ -> true)
            (fun o ->
                let tabindex = x.SelectedTabIndex
                let index = x.SelectedIndex
                let info = ChannelInfo.Create()
                let channels = _tuners.Tuners.[tabindex].Channels
                (o :?> string) |> (function
                    | "Ctrl+Shift+K" -> channels.Insert(index, info)
                    | "Ctrl+Shift+J" -> channels.Insert(index + 1, info)
                    | "Ctrl+Shift+D" -> channels.RemoveAt(index)
                    | _ -> ()))
    /// 選択タブ変更コマンド
    member x.TunerSelectedMoveCommand =
        Commons.CreateCommand (fun _ -> true)
            (fun o ->
                let m = _tabTuner.Items.Count - 1
                let i = x.SelectedTabIndex
                (o :?> string) |> (function
                    | "Alt+H" | "Ctrl+PageUp" | "Ctrl+Shift+F6" | "Ctrl+Shift+Tab" ->
                        if i <= 0 then m else i - 1
                    | "Alt+L" | "Ctrl+PageDown" | "Ctrl+F6" | "Ctrl+Tab" ->
                        if m <= i then 0 else i + 1
                    | _ -> 0)
                |> (fun i -> x.SelectedTabIndex <- i)
                _editGrid.ScrollIntoView(_editGrid.SelectedItem))
    /// 選択チャンネル変更コマンド
    member x.ChannelSelectedMoveCommand =
        Commons.CreateCommand (fun _ -> true)
            (fun o ->
                let m = _editGrid.Items.Count - 1
                let i = _editGrid.SelectedIndex
                (o :?> string) |> (function
                    | "Alt+K" -> if i <= 0 then 0 else i - 1
                    | "Alt+J" -> if m <= i then m else i + 1
                    | _ -> 0)
                |> (fun i -> _editGrid.SelectedIndex <- i)
                _editGrid.ScrollIntoView(_editGrid.SelectedItem))
    /// 入力項目のフォーカス変更
    member x.SelectedEditControlCommand =
        Commons.CreateCommand (fun _ -> true)
            (fun o ->
                let gt = o :?> string
                let t = gt.Substring(4)
                if gt.StartsWith("Alt+") && t.Length = 1 then
                    _editControls.[(int t) - 1].Focus() |> ignore)

module Program =
    [<STAThread>]
    [<EntryPoint>]
    let run(_) =
        // TextBoxがフォーカスを受け取ると全選択する
        EventManager.RegisterClassHandler(
            typeof<TextBox>,
            TextBox.GotFocusEvent,
            new RoutedEventHandler(fun a b ->
                match a with
                | :? TextBox as t -> t.SelectAll()
                | _ -> ()))
        let wpf = Application.LoadComponent(new Uri("ChannelEditor.xaml", System.UriKind.Relative)) :?> Window
        let model = new ChannelEditorModel(wpf)
        (new Application()).Run(wpf)

