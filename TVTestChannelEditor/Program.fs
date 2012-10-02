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

type ChannelEditorModel(wpf:Window) as x =
    inherit ViewModelBase()

    let mutable _tuners = TunerList.ReadConfig(@"..\..\BonDriver_ptmr.ch2")
    let mutable _selectedTabIndex = -1
    let mutable _selectedIndex = -1
    let mutable _selectedChannle = ChannelInfo("",0,0,0,0,0,0,false)

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
                let m = _tabTuner.Items.Count - 1
                let i = x.SelectedTabIndex
                let gt = o :?> string
                match gt with
                | "Ctrl+S" -> _tuners.Save("test.ch2")
                | "Alt+K"
                | "Alt+J" ->
                    let i = _editGrid.SelectedIndex
                    _editGrid.SelectedIndex <-
                        if gt = "Alt+K" then
                            if i <= 0 then 0 else i - 1
                        else
                            let m = _editGrid.Items.Count - 1
                            if m <= i then m else i + 1
                    _editGrid.ScrollIntoView(_editGrid.SelectedItem)
                | "Alt+H" | "Ctrl+PageUp" | "Ctrl+Shift+F6" | "Ctrl+Shift+Tab" ->
                    x.SelectedTabIndex <- if i <= 0 then m else i - 1
                | "Alt+L" | "Ctrl+PageDown" | "Ctrl+F6" | "Ctrl+Tab" ->
                    x.SelectedTabIndex <- if m <= i then 0 else i + 1
                | "Ctrl+Alt+C" ->
                    x.Tuners.[0].CopyWriteChannels(x.Tuners.[2], 2)
                    x.SelectedTabIndex <- 2
                | _ -> ())
    /// ウィンドウコマンド（エディット編）
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

