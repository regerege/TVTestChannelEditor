using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TVTestChannelEditor;

namespace TVTestChannelEditorGUI
{
    /// <summary>
    /// ListViewItemヘルパー拡張メソッド
    /// </summary>
    public static class ListViewItemExtension
    {
        private static List<string> PropertiesName = new List<string>
            {
                "ChannelName",
                "TunerID",
                "TransportID",
                "RemoteControlNumber",
                "ServiceID",
                "NetworkID",
                "TransportStreamID",
            };

        /// <summary>
        /// リストアイテムの値設定ヘルパー
        /// </summary>
        /// <param name="item">リストアイテム</param>
        /// <param name="name">プロパティ名</param>
        /// <param name="value">変更後の値</param>
        public static void SetValuePropertyName(this ListViewItem item, string name, object value)
        {
            var text = Convert.ToString(value);
            switch (name)
            {
                case "Enabled":
                    item.Checked = (bool)value;
                    break;
                default:
                    var i = PropertiesName.FindIndex((s) => s == name);
                    if (0 <= i && i < item.SubItems.Count)
                    {
                        item.SubItems[i].Text = text;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// チャンネル情報リストアイテム
    /// </summary>
    public class ChannelListViewItem : ListViewItem
    {
        /// <summary>
        /// チャンネル情報の取得または設定
        /// </summary>
        public ChannelInfo ChannelInfo { get; private set; }

        /// <summary>
        /// 項目がチェックされているかどうかを示す値を取得または設定します。
        /// </summary>
        public new bool Checked
        {
            get { return this.ChannelInfo.Enabled; }
            set { this.ChannelInfo.Enabled = value; }
        }

        /// <summary>
        /// チャンネル情報のオブジェクトを生成します。
        /// </summary>
        /// <param name="info">チャンネル情報</param>
        public ChannelListViewItem(ChannelInfo info)
            : base(info.ToListItems())
        {
            this.ChannelInfo = info;
            base.Checked = info.Enabled;
        }
    }
}