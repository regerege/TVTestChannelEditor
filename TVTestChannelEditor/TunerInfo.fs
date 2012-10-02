namespace TVTestChannelEditor

open System
open System.Collections.ObjectModel
open System.IO
open System.Text
open System.Text.RegularExpressions
open Commons

/// チャンネル情報
/// <summary>チャンネル情報</summary>
/// <param name="channelName">チャンネル番組名</param>
/// <param name="tunerID">チューナー番号</param>
/// <param name="transportID">トランスポートID</param>
/// <param name="remoteControlNumber">リモコン番号</param>
/// <param name="serviceID">サービスID</param>
/// <param name="networkID">ネットワークID</param>
/// <param name="transportStreamID">トランスポートストリームID</param>
/// <param name="enabled">表示/非表示</param>
type ChannelInfo(channelName,tunerID,transportID,remoteControlNumber,serviceID,networkID,transportStreamID,enabled) =
    inherit ViewModelBase()

    let mutable _channelName : string = channelName
    let mutable _tunerID : int = tunerID
    let mutable _transportID : int = transportID
    let mutable _remoteControlNumber : int = remoteControlNumber
    let mutable _serviceID : int = serviceID
    let mutable _networkID : int = networkID
    let mutable _transportStreamID : int = transportStreamID
    let mutable _enabled : bool = enabled

    /// チャンネル名の取得または設定
    member x.ChannelName
        with get() = _channelName
        and set(v) =
            _channelName <- v
            x.OnPropertyChanged(<@ x.ChannelName @>)
    /// チューナー番号の取得または設定
    member x.TunerID
        with get() = _tunerID
        and set(v) =
            _tunerID <- v
            x.OnPropertyChanged(<@ x.TunerID @>)
    /// トランスポートIDの取得または設定
    member x.TransportID
        with get() = _transportID
        and set(v) =
            _transportID <- v
            x.OnPropertyChanged(<@ x.TransportID @>)
    /// リモコン番号の取得また設定
    member x.RemoteControlNumber
        with get() = _remoteControlNumber
        and set(v) =
            _remoteControlNumber <- v
            x.OnPropertyChanged(<@ x.RemoteControlNumber @>)
    /// サービスIDの取得または設定
    member x.ServiceID
        with get() = _serviceID
        and set(v) =
            _serviceID <- v
            x.OnPropertyChanged(<@ x.ServiceID @>)
    /// ネットワークIDの取得または設定
    member x.NetworkID
        with get() = _networkID
        and set(v) =
            _networkID <- v
            x.OnPropertyChanged(<@ x.NetworkID @>)
    /// トランスポートストリームIDの取得または設定
    member x.TransportStreamID
        with get() = _transportStreamID
        and set(v) =
            _transportStreamID <- v
            x.OnPropertyChanged(<@ x.TransportStreamID @>)
    /// チャンネルの表示/非表示の取得または設定
    member x.Enabled
        with get() = _enabled
        and set(v) =
            _enabled <- v
            x.OnPropertyChanged(<@ x.Enabled @>)
    /// 複製を作成する。
    member x.CopyTo(index:int) =
        new ChannelInfo(
            x.ChannelName,
            index,
            x.TransportID,
            x.RemoteControlNumber,
            x.ServiceID,
            x.NetworkID,
            x.TransportStreamID,
            x.Enabled)
    override x.ToString() =
        sprintf "%s,%d,%d,%d,,%d,%d,%d,%d"
            <| _channelName
            <| _tunerID
            <| _transportID
            <| _remoteControlNumber
            <| _serviceID
            <| _networkID
            <| _transportStreamID
            <| (if _enabled then 1 else 0)

/// チューナ情報
type TunerInfo(tunerName) =
    inherit ViewModelBase()

    let mutable _tunerName : string = tunerName
    let mutable _channels = new ObservableCollection<ChannelInfo>()
    
    /// チューナー名の取得または設定
    member x.TunerName
        with get() = _tunerName
        and set v =
            _tunerName <- v
            x.OnPropertyChanged(<@ x.TunerName @>)
    /// チャンネルリストの取得または設定
    member x.Channels
        with get() = _channels
        and set v =
            _channels <- v
            x.OnPropertyChanged(<@ x.Channels @>)
    /// チャンネルすべてのチューナーIDを一括を行う。
    member x.ChangeTunerNumber id =
        for c in x.Channels do c.TunerID <- id

    /// 指定チューナへのチャンネルリストの複製上書きを行う
    member x.CopyWriteChannels(tt:TunerInfo, index) =
        if x.TunerName = tt.TunerName then failwith "同一チューナーにチャンネルをコピー出来ません。"
        let arr = Array.init (x.Channels.Count) (fun i -> x.Channels.[i].CopyTo(index))
        let c = ObservableCollection<ChannelInfo>(arr)
        tt.Channels <- c

/// チューナー用ライブラリ
module TunerCommons =
    /// 設定ファイル読み込み用アクティブパターン
    let (|TunerLine|ChannelLine|NoneLine|) (line:string) =
        let tuner = Regex.Match(line, "^;#SPACE\((\d+),([ST]\d+)\)$")
        let channel = line.Split(',')
        if tuner.Success then
            TunerLine (Convert.ToInt32(tuner.Result("$1")), tuner.Result("$2"))
        elif line.StartsWith(";") then NoneLine
        elif channel.Length = 9 then
            ChannelLine (
                ChannelInfo(
                    channelName = channel.[0]
                    , tunerID = int(channel.[1])
                    , transportID = int(channel.[2])
                    , remoteControlNumber = int(channel.[3])
                    , serviceID = int(channel.[5])
                    , networkID = int(channel.[6])
                    , transportStreamID = int(channel.[7])
                    , enabled = (channel.[8] = "1")
                ))
        else NoneLine
    /// 設定情報シーケンス
    let read (path:string) f =
        use file = new StreamReader(path, SJIS)
        seq {
            while not(file.EndOfStream) do
                yield file.ReadLine()
        } |> f

/// チューナーリスト
type TunerList() =
    let mutable _list = new ObservableCollection<TunerInfo>()
    let mutable _tuner : TunerInfo option = None
    let mutable _path : string = ""
    /// チューナー一覧を取得
    member x.Tuners
        with get() = _list
    /// チューナーを追加する。
    member x.AddTuner(name) =
        _tuner <- Some <| TunerInfo(name)
        if _tuner.IsSome then
            _list.Add _tuner.Value
    /// 現在選択中のチューナーにチャンネルを追加する。
    member x.Add (info : ChannelInfo) =
        _tuner.Value.Channels.Add info
    /// インデクサの定義
    member x.Item
        with get i = _list.[i]
        and set i v = _list.[i] <- v

    /// オリジナルファイルの取得または設定
    member x.OriginalPath
        with get() = _path
        and set v = _path <- v
    /// 設定ファイルのディレクトリを取得する。
    member x.BasePath
        with get() = Path.GetDirectoryName(_path)

    /// 設定情報を保存する。
    member x.Save(path:string) =
        let path =
            if String.IsNullOrEmpty path then x.OriginalPath
            else Path.GetFullPath(path)
        let _ = File.Exists path    // チェック処理として使える？
        let temppath = Path.Combine(x.BasePath, "temp.ch2")
        let readFile path = new StreamWriter(path, false, SJIS)
        try
            use file = readFile temppath
            TunerCommons.read x.OriginalPath
                <| (fun s ->
                    s |> Seq.fold (fun index (line:string) ->
                        match line with
                        // チャンネルは出力しない
                        | TunerCommons.ChannelLine ci -> index
                        | TunerCommons.TunerLine (index, th) ->
                            let tuner = x.Tuners.[index]
                            // SPACEを出力
                            file.WriteLine(sprintf ";#SPACE(%d,%s)" index tuner.TunerName)
                            // チャンネルの一括出力
                            Seq.iter(fun info -> file.WriteLine(info.ToString())) tuner.Channels
                            index
                        | _ ->
                            // その他の出力
                            file.WriteLine(line)
                            index
                    ) -1) |> ignore
        with
        | ex ->
            failwith "設定ファイルの書き込みに失敗しました。"
        try // ↓超ださい
            let bkpath = path + ".bak"
            if File.Exists bkpath then File.Delete bkpath
            File.Move(path, bkpath)
            File.Copy(temppath, path, true)
            File.Delete temppath
        with
        | _ -> ()
    /// 設定ファイルを読み込む
    static member ReadConfig(path:string) =
        try
            let init = new TunerList()
            init.OriginalPath <- Path.GetFullPath(path)
            TunerCommons.read path
                <| (fun s ->
                    s |> Seq.fold (fun (tuners:TunerList) line ->
                        match line with
                        | TunerCommons.TunerLine (index, tn) ->
                            tuners.AddTuner tn
                        | TunerCommons.ChannelLine ci ->
                            tuners.Add ci
                        | _ -> ()
                        tuners
                    ) init)
        with
        | ex ->
            failwith "設定ファイルの読み込みに失敗しました。"

