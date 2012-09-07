using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TVTestChannelEditor;

namespace TVTestChannelEditorGUI
{
    /// <summary>
    /// チャンネル編集GUIのメイン画面
    /// </summary>
    public partial class MainEditor : Form
    {
        /// <summary>
        /// メイン画面のインスタンスを作成する。
        /// </summary>
        public MainEditor()
        {
            InitializeComponent();

            this.InitEvents();
        }

        /// <summary>
        /// Formsのイベント定義
        /// </summary>
        public void InitEvents()
        {
            // デフォルトプロパティ設定を保存する。
            this.FormClosing += (sender, e) => { Properties.Settings.Default.Save(); };
            this.ChannelListView.SelectedIndexChanged += (sender, e) =>
            {
                var x = (ListView)sender;
                var items = x.SelectedItems;
                if (0 < items.Count)
                {
                    var item = items[0] as ChannelListViewItem;
                    if (item != null)
                    {
                        this.ChannelEditor.Tag = item;
                        this.ChannelEditor.SelectedObject = item.ChannelInfo;
                    }
                }
            };
            // プロパティ編集イベント
            this.ChannelEditor.PropertyValueChanged += (sender, e) =>
            {
                var item = ((PropertyGrid)sender).Tag as ListViewItem;
                item.SetValuePropertyName(e.ChangedItem.Label, e.ChangedItem.Value);
            };
            // ListView.Checkedイベント
            this.ChannelListView.ItemChecked += (sender, e) =>
            {
                ((ChannelListViewItem)e.Item).Checked = e.Item.Checked;
                this.ChannelEditor.SelectedObject = ((ChannelListViewItem)e.Item).ChannelInfo;
            };
        }
    }
}

