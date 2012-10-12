﻿namespace TVTestChannelEditor

module StringCommon =
    let private map =
        Seq.cache <|
            seq {
                yield ('!','！')
                yield ('"','”')
                yield ('#','＃')
                yield ('$','＄')
                yield ('%','％')
                yield ('&','＆')
                yield ('\'','’')
                yield ('(','（')
                yield (')','）')
                yield ('*','＊')
                yield ('+','＋')
                yield (',','，')
                yield ('-','－')
                yield ('.','．')
                yield ('/','／')
                yield (':','：')
                yield (';','；')
                yield ('<','＜')
                yield ('=','＝')
                yield ('>','＞')
                yield ('?','？')
                yield ('@','＠')
                yield ('A','Ａ')
                yield ('B','Ｂ')
                yield ('C','Ｃ')
                yield ('D','Ｄ')
                yield ('E','Ｅ')
                yield ('F','Ｆ')
                yield ('G','Ｇ')
                yield ('H','Ｈ')
                yield ('I','Ｉ')
                yield ('J','Ｊ')
                yield ('K','Ｋ')
                yield ('L','Ｌ')
                yield ('M','Ｍ')
                yield ('N','Ｎ')
                yield ('O','Ｏ')
                yield ('P','Ｐ')
                yield ('Q','Ｑ')
                yield ('R','Ｒ')
                yield ('S','Ｓ')
                yield ('T','Ｔ')
                yield ('U','Ｕ')
                yield ('V','Ｖ')
                yield ('W','Ｗ')
                yield ('X','Ｘ')
                yield ('Y','Ｙ')
                yield ('Z','Ｚ')
                yield ('[','［')
                yield ('\\','￥')
                yield (']','］')
                yield ('^','＾')
                yield ('_','＿')
                yield ('`','‘')
                yield ('a','ａ')
                yield ('b','ｂ')
                yield ('c','ｃ')
                yield ('d','ｄ')
                yield ('e','ｅ')
                yield ('f','ｆ')
                yield ('g','ｇ')
                yield ('h','ｈ')
                yield ('i','ｉ')
                yield ('j','ｊ')
                yield ('k','ｋ')
                yield ('l','ｌ')
                yield ('m','ｍ')
                yield ('n','ｎ')
                yield ('o','ｏ')
                yield ('p','ｐ')
                yield ('q','ｑ')
                yield ('r','ｒ')
                yield ('s','ｓ')
                yield ('t','ｔ')
                yield ('u','ｕ')
                yield ('v','ｖ')
                yield ('w','ｗ')
                yield ('x','ｘ')
                yield ('y','ｙ')
                yield ('z','ｚ')
                yield ('{','｛')
                yield ('|','｜')
                yield ('}','｝')
                yield ('~','￣')
                yield ('｀','｀')
                yield ('～','～')
                yield ('｡','。')
                yield ('｢','「')
                yield ('｣','」')
                yield ('､','、')
                yield ('･','・')
                yield ('ｦ','ヲ')
                yield ('ｧ','ァ')
                yield ('ｨ','ィ')
                yield ('ｩ','ゥ')
                yield ('ｪ','ェ')
                yield ('ｫ','ォ')
                yield ('ｬ','ャ')
                yield ('ｭ','ュ')
                yield ('ｮ','ョ')
                yield ('ｯ','ッ')
                yield ('ｰ','ー')
                yield ('ｱ','ア')
                yield ('ｲ','イ')
                yield ('ｳ','ウ')
                yield ('ｴ','エ')
                yield ('ｵ','オ')
                yield ('ｶ','カ')
                yield ('ｷ','キ')
                yield ('ｸ','ク')
                yield ('ｹ','ケ')
                yield ('ｺ','コ')
                yield ('ｻ','サ')
                yield ('ｼ','シ')
                yield ('ｽ','ス')
                yield ('ｾ','セ')
                yield ('ｿ','ソ')
                yield ('ﾀ','タ')
                yield ('ﾁ','チ')
                yield ('ﾂ','ツ')
                yield ('ﾃ','テ')
                yield ('ﾄ','ト')
                yield ('ﾅ','ナ')
                yield ('ﾆ','ニ')
                yield ('ﾇ','ヌ')
                yield ('ﾈ','ネ')
                yield ('ﾉ','ノ')
                yield ('ﾊ','ハ')
                yield ('ﾋ','ヒ')
                yield ('ﾌ','フ')
                yield ('ﾍ','ヘ')
                yield ('ﾎ','ホ')
                yield ('ﾏ','マ')
                yield ('ﾐ','ミ')
                yield ('ﾑ','ム')
                yield ('ﾒ','メ')
                yield ('ﾓ','モ')
                yield ('ﾔ','ヤ')
                yield ('ﾕ','ユ')
                yield ('ﾖ','ヨ')
                yield ('ﾗ','ラ')
                yield ('ﾘ','リ')
                yield ('ﾙ','ル')
                yield ('ﾚ','レ')
                yield ('ﾛ','ロ')
                yield ('ﾜ','ワ')
                yield ('ﾝ','ン')
                yield ('ﾞ','゛')
                yield ('ﾟ','゜')
            }
    let private dmap =
        Seq.cache <|
            seq {
                yield (('ｶ','ﾞ'),'ガ')
                yield (('ｷ','ﾞ'),'ギ')
                yield (('ｸ','ﾞ'),'グ')
                yield (('ｹ','ﾞ'),'ゲ')
                yield (('ｺ','ﾞ'),'ゴ')
                yield (('ｻ','ﾞ'),'ザ')
                yield (('ｼ','ﾞ'),'ジ')
                yield (('ｽ','ﾞ'),'ズ')
                yield (('ｾ','ﾞ'),'ゼ')
                yield (('ｿ','ﾞ'),'ゾ')
                yield (('ﾀ','ﾞ'),'ダ')
                yield (('ﾁ','ﾞ'),'ヂ')
                yield (('ﾂ','ﾞ'),'ヅ')
                yield (('ﾃ','ﾞ'),'デ')
                yield (('ﾄ','ﾞ'),'ド')
                yield (('ﾊ','ﾞ'),'バ')
                yield (('ﾊ','ﾟ'),'パ')
                yield (('ﾋ','ﾞ'),'ビ')
                yield (('ﾋ','ﾟ'),'ピ')
                yield (('ﾌ','ﾞ'),'ブ')
                yield (('ﾌ','ﾟ'),'プ')
                yield (('ﾍ','ﾞ'),'ベ')
                yield (('ﾍ','ﾟ'),'ペ')
                yield (('ﾎ','ﾞ'),'ボ')
                yield (('ﾎ','ﾟ'),'ポ')
                yield (('ｳ','ﾞ'),'ヴ')
            }
    /// charの初期値として使用
    let private cn = '\000'
    /// 全角へ変換
    let private fwtransfer c =
        map |> Seq.tryFind(fst >> (=)c)
        |> (fun o -> if o.IsSome then snd o.Value else c)
    /// 濁点半濁点が続く半角2文字を全角文字に変換
    let private dfwtransfer t =
        dmap |> Seq.tryFind(fst >> (=)t)
        |> (fun o -> if o.IsSome then Some <| snd o.Value else None)
    /// 配列を文字列に変換
    let private toString (arr:char[]) = System.String(arr)
    type System.String with
        member x.StrCnv() =
//            x |> Seq.map fwtransfer |> Seq.toArray |> toString        // 半角1文字を全角にする
            x |> Seq.fold (fun (l,(a,b)) c ->
                match (a,b) with
                | ('\000',_)
                | (_,'\000') -> l,(b,c)
                | t ->
                    match (dfwtransfer t) with
                    | Some d -> l@[d],(cn,c)
                    | None -> l@[fwtransfer a],(b,c)
            ) ([],(cn,cn))
            // ↓がなんとかならないのか・・・ださい！
            |> (fun (l,(a,b)) ->
                match (a,b) with
                | ('\000',b) -> l,[b]
                | (a,'\000') -> l,[a]
                | (a,b) ->
                    match (dfwtransfer (a,b)) with
                    | Some d -> l,[d]
                    | None -> l,[a;b;]
                |> (fun (l,l2) ->
                    l2 |> List.map fwtransfer
                    |> List.append l
                    |> List.toArray)
                |> toString
            )

