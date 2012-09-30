namespace TVTestChannelEditor

open System
open System.ComponentModel
open System.IO
open System.Windows
open System.Windows.Controls
open System.Windows.Markup
open System.Windows.Media
open TVTestChannelEditor

module Program =
    type ChannelEditorModel() =
        inherit ViewModelBase()

        let mutable tuners = TunerList.ReadConfig(@"..\..\BonDriver_ptmr.ch2")
        let mutable _selectedChannle = ChannelInfo("",0,0,0,0,0,0,false)

        /// 読み込み後のチューナーリスト
        member x.Tuners = tuners.Tuners

        /// 現在選択しているチャンネルの取得または設定
        member x.SelectedChannel
            with get() = _selectedChannle
            and set v =
                _selectedChannle <- v
                x.OnPropertyChanged(<@ x.SelectedChannel @>)

    [<STAThread>]
    [<EntryPoint>]
    let run(_) =
        let model = new ChannelEditorModel()
        let mutable wpf = Application.LoadComponent(new Uri("ChannelEditor.xaml", System.UriKind.Relative)) :?> Window
        wpf.DataContext <- model
        let tabs = wpf.FindName("tabTuner") :?> TabControl
        (new Application()).Run(wpf)

