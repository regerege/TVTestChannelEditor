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
type SelectionTunerModel(tuners:TunerList, title) as x =
    /// 親画面で選択中のチューナー
    let mutable _tuner : TunerInfo = new TunerInfo("",-1)
    /// 選択チューナー以外のチューナー配列
    let mutable _tunerArr : TunerInfo[] = Array.empty
    /// 選択アイテム
    let mutable _items : TunerInfo[] = Array.empty
    /// 画面タイトル
    let mutable _title = title
    
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

    member x.Title
        with get() = _title
        and set v = _title <- v
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
    /// ウィンドウコマンド
    member x.KeyCommand =
        Commons.CreateCommand (fun _ -> true)
            (fun o ->
                let gt = o :?> string
                match gt with
                | "ESC" -> _wpf.Hide()
                | _ -> ()
                ())

type ChannelEditorModel(wpf:Window) as x =
    inherit ViewModelBase()

    let mutable _tuners = TunerList.ReadConfig(@"..\..\BonDriver_ptmr.ch2")
    let mutable _selectedTabIndex = -1
    let mutable _selectedIndex = -1
    let mutable _selectedChannle = ChannelInfo.Create()
    // チャンネルリストの移動
    let mutable _channelsYank = List.empty

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
    /// 選択タブのTunerInfoを取得
    member x.SelectedTuner : TunerInfo = _tuners.Tuners.[_selectedTabIndex]
    /// 選択タブのChannelInfoのコレクションを取得
    member x.Channles : ObservableCollection<ChannelInfo> = x.SelectedTuner.Channels
    /// 表示中のDataGridを取得
    member x.EditGrid = _editGrid
    /// 現在選択チューナーIndexの取得または設定
    member x.SelectedTabIndex
        with get() = _selectedTabIndex
        and set v =
            // タブを切り替えると一度Yankをクリアする。
            if _selectedTabIndex <> v then
                _channelsYank <- List.empty
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
                | "Ctrl+S" -> _tuners.Save("")
                | "Ctrl+Alt+C" ->
                    x.CopyMergeTuner "コピー先チューナー選択画面"
                        (fun nm t ->
                            let text = sprintf "%s から %s にチャンネルリスト複製しました。" nm t
                            MessageBox.Show(text) |> ignore)
                        (fun s (t:TunerInfo) -> Array.iter t.CopyWriteChannels s)
                | "Ctrl+Alt+M" ->
                    x.CopyMergeTuner "マージ先チューナー選択画面"
                        (fun nm t ->
                            let text = sprintf "%s から %s にチャンネルリストをマージしました。" nm t
                            MessageBox.Show(text) |> ignore)
                        (fun s (t:TunerInfo) ->
                            t.MergeChannels s (fun a b -> a.ChannelName = b.ChannelName))
                | _ -> ())
    /// チャンネルの追加と削除と移動
    member x.ChannelCommand =
        Commons.CreateCommand (fun _ -> true)
            (fun o ->
                let index = x.SelectedIndex
                let info = ChannelInfo.Create()
                let channels = x.Channles
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
    /// ウィンドウコマンド
    member x.YankChannelMoveCommand =
        Commons.CreateCommand
            (fun o ->
                let gt = o :?> string
                match gt with
                | "Alt+Y" -> true
                | "Alt+P" -> 0 < _channelsYank.Length
                | "Alt+Shift+P" -> 0 < _channelsYank.Length
                | _-> false)
            (fun o ->
                let gt = o :?> string
                match gt with
                | "Alt+Y" ->
                    _channelsYank <- 
                        seq { for i in _editGrid.SelectedItems -> i :?> ChannelInfo }
                        |> Seq.fold(fun l a -> l@[a]) []
                | _ ->
                    let channels = x.Channles
                    let max = channels.Count
//                    let index =
//                        if gt =  "Alt+P" then _selectedIndex + 1
//                        else _selectedIndex
                    _channelsYank
                    |> List.scan(fun i s ->
                        let t =
                            seq { for i in x.Channles -> i }
                            |> Seq.findIndex(fun a -> a = s)
                        if i <> t then
                            if gt =  "Alt+P" then channels.Move(t,i+1)
                            else channels.Move(t,i)
                        i + 1) _selectedIndex
                    |> List.iter (fun _ -> ())
                    _channelsYank <- List.empty
                )
    /// コピーまたはマージ
    member x.CopyMergeTuner title msg ff =
        let subwind = new SelectionTunerModel(_tuners, title)
        subwind.Window.Owner <- wpf
        let f = x.SelectedTabIndex
        let st = x.Tuners.[f]
        let nm = st.TunerName
        subwind.SelectTuner <- st
        let b = subwind.Window.ShowDialog()
        if b.HasValue || b.Value then
            let tuners = subwind.SelectTuners
            if 0 < tuners.Length then
                let sts =
                    tuners
                    |> Array.fold(fun s t -> s + t.TunerName) ""
                    |> (fun s -> s.Substring(1))
                ff tuners st
                msg nm sts

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

