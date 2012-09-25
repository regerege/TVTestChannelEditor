open Commons
open Commons.StateHelper

let add n = state {
        let! num = stateGet
        do! stateSet (num + n)
        return (num + n)
    }

let n = 0
printfn "n = %d" n 

let mutable num = 0
for i in 1..10 do
    num <- num + i
    ()

//(state {
//    for i in 1..10 do
//        let! num = add i
//        printfn "%A" num
//    return 1
//}) 0 |> ignore
for i in 1..10 do
    (state {
        let! nn = stateGet
        do! stateSet (nn+i)
        printfn "%A" (nn+i)
    }) i |> ignore


//open System
//open System.Drawing
//open System.Windows.Forms
//open Commons
//
//let forms = new Form(Name = "form1", Text = "てすと", ClientSize = new Size(284,62), Padding = new Padding(10))
//let button = new Button(Name = "button1", Text = "inc", Size = new Size(84, 42), Dock = DockStyle.Left)
//let text = new TextBox(Name = "text", Text = "9", Size = new Size(180, 19), Dock = DockStyle.Fill)
//text.Enabled <- false
//forms.Controls.Add(text)
//forms.Controls.Add(button)
//
//let setText (o:'a) = text.Text <- Convert.ToString(o)
//
//#if COMPILED
//let incNum() =
//    let f =
//        Builders.state {
//            let! a = Builders.state.Get
//            let b = int(text.Text)
//            do! Builders.state.Set b
//            let! c = Builders.state.Get a
//            return Builders.state.Get a
//        }
//    f <| int(text.Text) |> ignore
//
//
//[<STAThread>]
//do Application.Run(forms)
//#endif
