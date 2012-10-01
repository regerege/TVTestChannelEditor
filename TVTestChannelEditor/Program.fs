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
    let mutable _selectedIndex = -1
    let mutable _selectedChannle = ChannelInfo("",0,0,0,0,0,0,false)

    // コントロール
    let _tabTuner = wpf.FindName("tabTuner") :?> TabControl
    let mutable _editGrid : DataGrid = null

    /// コンストラクタ
    do
        _tabTuner.SelectionChanged.Add(fun _ ->
            let c = Commons.GetChildElement _tabTuner typeof<DataGrid>
            if c.IsSome then _editGrid <- c.Value :?> DataGrid)
        wpf.Loaded.Add(fun _ -> x.SelectedIndex <- 0)
        wpf.DataContext <- x

    /// 読み込み後のチューナーリスト
    member x.Tuners = _tuners.Tuners
    member x.EditGrid = _editGrid
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
    /// コマンド
    member x.KeyCommand =
        Commons.CreateCommand (fun _ -> true)
            (fun o ->
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
                | _ -> ())

module Program =
    [<STAThread>]
    [<EntryPoint>]
    let run(_) =
        let wpf = Application.LoadComponent(new Uri("ChannelEditor.xaml", System.UriKind.Relative)) :?> Window
        let model = new ChannelEditorModel(wpf)
        (new Application()).Run(wpf)

