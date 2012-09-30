namespace TVTestChannelEditor

open System
open System.ComponentModel
open System.Windows
open System.Windows.Controls
open System.Windows.Markup
open System.Windows.Media
open System.Windows.Input
open System.IO
open TVTestChannelEditor

type ChannelEditorModel() =
    inherit ViewModelBase()

    let mutable _tuners = TunerList.ReadConfig(@"..\..\BonDriver_ptmr.ch2")
    let mutable _selectedChannle = ChannelInfo("",0,0,0,0,0,0,false)

    /// 読み込み後のチューナーリスト
    member x.Tuners = _tuners.Tuners
    /// 現在選択しているチャンネルの取得または設定
    member x.SelectedChannel
        with get() = _selectedChannle
        and set v =
            _selectedChannle <- v
            x.OnPropertyChanged(<@ x.SelectedChannel @>)
    /// コマンド
    member x.SaveCommand =
        Commons.CreateCommand
            (fun _ -> true)
            (fun _ -> _tuners.Save("test.ch2"))

module Program =
    [<STAThread>]
    [<EntryPoint>]
    let run(_) =
        let model = new ChannelEditorModel()
        let mutable wpf = Application.LoadComponent(new Uri("ChannelEditor.xaml", System.UriKind.Relative)) :?> Window
        wpf.DataContext <- model
        let tabs = wpf.FindName("tabTuner") :?> TabControl
        (new Application()).Run(wpf)

