namespace TVTestChannelEditor
open System
open System.Collections.ObjectModel
open System.ComponentModel
open System.Text
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns

/// プロパティ変更後通知オブジェクト
type ViewModelBase() =
    let _propertyChanged = Event<_,_>()
    let toPropName(query : Expr) =
        match query with
        | PropertyGet(a, b, list) -> b.Name
        | _ -> ""

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member x.PropertyChanged = _propertyChanged.Publish

    abstract member OnPropertyChanged : string -> unit
    default x.OnPropertyChanged propertyName =
        _propertyChanged.Trigger(x, new PropertyChangedEventArgs(propertyName))

    member x.OnPropertyChanged(expr : Expr) =
        expr
        |> toPropName
        |> x.OnPropertyChanged

/// 共通モジュール
module Commons =
    /// Shift-JISエンコーディング
    let SJIS = Encoding.GetEncoding(932)
    /// ICommandオブジェクトを作成する。
    let CreateCommand canExecute action =
        let event = Event<_,_>()
        { new System.Windows.Input.ICommand with
            member x.CanExecute obj = canExecute obj
            member x.Execute obj = action obj
            [<CLIEvent>]
            member x.CanExecuteChanged = event.Publish }
