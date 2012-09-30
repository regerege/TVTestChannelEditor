namespace TVTestChannelEditor
//
//open Microsoft.FSharp.Control
//open System.Windows.Input
//
//type SaveCommand() =
//    let canExecuteChangedEvent = new Event<_,_>()
//    interface ICommand with
//        member x.CanExecute(param:obj) = true
//        member x.Execute(param:obj) =
//            System.Diagnostics.Debugger.Break()
//        [<CLIEvent>]
//        member x.CanExecuteChanged = canExecuteChangedEvent.Publish
