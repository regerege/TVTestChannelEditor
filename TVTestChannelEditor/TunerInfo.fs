namespace TVTestChannelEditor
open  System.ComponentModel

/// チャンネル情報
type ChannelInfo =
    {
        /// チャンネル名の取得または設定
        [<
            Category("チャンネル情報");
            Description("チャンネル番号に対する番組名の設定項目");
            DefaultValue("");
        >]
        mutable ChannelName : string
        /// チューナ番号の取得または設定
        [<
            Category("チャンネル情報");
            Description("0から始まるチューナ番号");
            DefaultValue(0);
        >]
        mutable TunerID : int
        /// トランスポートIDの取得または設定
        [<
            Category("チャンネル情報");
            Description("トランスポートID");
            DefaultValue(0);
        >]
        mutable TransportID : int
        /// リモコン番号の取得または設定
        [<
            Category("チャンネル情報");
            Description("リモコン番号");
            DefaultValue("");
        >]
        mutable RemoteControlNumber : int
        /// サービスIDの取得または設定
        [<
            Category("チャンネル情報");
            Description("サービスID");
            DefaultValue("");
        >]
        mutable ServiceID : int
        /// ネットワークIDの取得または設定
        [<
            Category("チャンネル情報");
            Description("ネットワークID");
            DefaultValue("");
        >]
        mutable NetworkID : int
        /// トランスポートストリームIDの取得または設定
        [<
            Category("チャンネル情報");
            Description("トランスポートストリームID");
            DefaultValue("");
        >]
        mutable TransportStreamID : int
        /// チャンネルの有効または無効を取得または設定
        [<
            Category("チャンネル情報");
            Description("チャンネルの表示/非表示の設定");
            DefaultValue("");
        >]
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

/// チューナ情報
type TunerInfo =
    {
        /// チューナ番号
        TunerNumber : int
        /// チューナに結びつくチャンネル一覧
        Channels : ChannelInfo list
    }
    member x.ChangeTunerNumber id =
        {
            TunerNumber = id
            Channels = x.Channels |> List.map(fun c -> c.TunerID <- id; c)
        }

