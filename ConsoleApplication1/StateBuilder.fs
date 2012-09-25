namespace Commons
    type StateBuilder() =
        member x.Return a = (fun s -> a, s)
        member x.ReturnFrom a = a
        member x.Bind(m, f) = (fun s -> let a, b = m s in f a b)
//        member x.For(s,f) = s |> Seq.map(f)
//        member x.Combine a b = b
//        member x.Delay a = a
        member x.Zero() = fun _ -> 0,0
    module StateHelper =
        let state = new StateBuilder()
        let stateGet = fun s -> s,s
        let stateSet v = fun _ -> (),v
        let stateExec f s = f s |> snd

