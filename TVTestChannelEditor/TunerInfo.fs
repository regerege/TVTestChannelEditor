namespace TVTestChannelEditor
open System.Collections.Generic

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

type ChannelInfo2 =
    {
        /// チャンネル名の取得または設定
        mutable ChannelName : string
        /// チューナ番号の取得または設定
        mutable TunerID : int
        /// トランスポートIDの取得または設定
        mutable TransportID : int
        /// リモコン番号の取得または設定
        mutable RemoteControlNumber : int
        /// サービスIDの取得または設定
        mutable ServiceID : int
        /// ネットワークIDの取得または設定
        mutable NetworkID : int
        /// トランスポートストリームIDの取得または設定
        mutable TransportStreamID : int
        /// チャンネルの有効または無効を取得または設定
        mutable Enabled : bool
    }
    member x.ToListItems() =
        [|
            x.ChannelName
            sprintf "%A" x.TunerID
            sprintf "%A" x.TransportID
            sprintf "%A" x.RemoteControlNumber
            sprintf "%A" x.ServiceID
            sprintf "%A" x.NetworkID
            sprintf "%A" x.TransportStreamID
        |]
    static member Create(enable,name,tunerid,tpid,rcnum,serviceid,netid,tsid) : ChannelInfo2 =
        {
            ChannelName = name
            TunerID = tunerid
            TransportID = tpid
            RemoteControlNumber = rcnum
            ServiceID = serviceid
            NetworkID = netid
            TransportStreamID = tsid
            Enabled = enable
        }

/// チューナ情報
type TunerInfo =
    {
        /// チューナ番号
        mutable TunerNumber : int
        /// チューナに結びつくチャンネル一覧
        mutable Channels : List<ChannelInfo>
    }
    member x.ChangeTunerNumber id =
        x.TunerNumber <- id
        for c in x.Channels do c.TunerID <- id
    static member CreateTestData() : TunerInfo =
        {
            TunerNumber = 0
            Channels =
                new List<ChannelInfo>(
                    [|
                        ChannelInfo("ＢＳアニマックス", 0, 7, 236, 236, 4, 18033, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("あいうえお", 0, 1, 999, 999, 1, 10000, true)
                        ChannelInfo("ＢＳアニマックス", 0, 7, 236, 236, 4, 18033, true)
                    |]
                )
        }
