﻿namespace TVTestChannelEditor

open System
open System.IO
open System.Text
open System.Text.RegularExpressions

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

/// チューナ情報
type TunerInfo =
    {
        /// チューナ番号
        mutable TunerName : string
        /// チューナに結びつくチャンネル一覧
        mutable Channels : ResizeArray<ChannelInfo>
//        ObservableCollection
    }
    member x.ChangeTunerNumber id =
        for c in x.Channels do c.TunerID <- id

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

/// チューナーリスト
type TunerList() =
    let mutable _list = new ResizeArray<TunerInfo>()
    let mutable _tuner : TunerInfo option = None
    /// チューナー一覧を取得
    member x.Tuners
        with get() = _list
    /// チューナーを追加する。
    member x.AddTuner(name) =
        _tuner <- Some { TunerName = name; Channels = new ResizeArray<ChannelInfo>() }
        if _tuner.IsSome then
            _list.Add _tuner.Value
    /// 現在選択中のチューナーにチャンネルを追加する。
    member x.Add (info : ChannelInfo) =
        _tuner.Value.Channels.Add info
    member x.Item
        with get i = _list.[i]
        and set i v = _list.[i] <- v

    /// 設定ファイルを読み込む
    static member ReadConfig(path:string) =
        try
            use file = new StreamReader(path, Encoding.GetEncoding(932))
            let init = new TunerList()
            let ioSeq =
                seq {
                    while not(file.EndOfStream) do
                        yield file.ReadLine()
                }
            ioSeq |> Seq.fold (fun (tuners:TunerList) line ->
                match line with
                | TunerCommons.TunerLine (index, tn) ->
                    tuners.AddTuner tn
                | TunerCommons.ChannelLine ci ->
                    tuners.Add ci
                | _ -> ()
                tuners
            ) init
        with
        | ex ->
            failwith "設定ファイルの読み込みに失敗しました。"
