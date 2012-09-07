namespace TVTestChannelEditorGUI
{
    partial class MainEditor
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMS = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tunerSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.channelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.upaddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttomAddtoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.delToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ChannelListView = new System.Windows.Forms.ListView();
            this.chChannelName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTunerID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTPID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRCNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chServiceID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNetID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTSID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel5 = new System.Windows.Forms.Panel();
            this.ChannelEditor = new System.Windows.Forms.PropertyGrid();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.statusTextBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMS
            // 
            this.mainMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.mainMS.Location = new System.Drawing.Point(0, 0);
            this.mainMS.Name = "mainMS";
            this.mainMS.Size = new System.Drawing.Size(934, 26);
            this.mainMS.TabIndex = 0;
            this.mainMS.Text = "メインメニュー";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tunerSelectToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(89, 22);
            this.fileToolStripMenuItem.Text = "ファイル (&F)";
            // 
            // tunerSelectToolStripMenuItem
            // 
            this.tunerSelectToolStripMenuItem.Name = "tunerSelectToolStripMenuItem";
            this.tunerSelectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.tunerSelectToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.tunerSelectToolStripMenuItem.Text = "チューナの選択 (&S)";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.quitToolStripMenuItem.Text = "終了 (&Q)";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator2,
            this.channelToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(65, 22);
            this.editToolStripMenuItem.Text = "編集 (&E)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.undoToolStripMenuItem.Text = "Undo (&U)";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.redoToolStripMenuItem.Text = "Redo (&R)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(155, 6);
            // 
            // channelToolStripMenuItem
            // 
            this.channelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem,
            this.toolStripSeparator3,
            this.upaddToolStripMenuItem,
            this.buttomAddtoolStripMenuItem1,
            this.delToolStripMenuItem,
            this.toolStripSeparator4,
            this.editToolStripMenuItem1});
            this.channelToolStripMenuItem.Name = "channelToolStripMenuItem";
            this.channelToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.channelToolStripMenuItem.Text = "チャンネル (&C)";
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.K)));
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.moveUpToolStripMenuItem.Text = "上に移動 (&U)";
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.J)));
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.moveDownToolStripMenuItem.Text = "下に移動 (&D)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(268, 6);
            // 
            // upaddToolStripMenuItem
            // 
            this.upaddToolStripMenuItem.Name = "upaddToolStripMenuItem";
            this.upaddToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.upaddToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.upaddToolStripMenuItem.Text = "上に追加 (&A)";
            // 
            // buttomAddtoolStripMenuItem1
            // 
            this.buttomAddtoolStripMenuItem1.Name = "buttomAddtoolStripMenuItem1";
            this.buttomAddtoolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.buttomAddtoolStripMenuItem1.Size = new System.Drawing.Size(271, 22);
            this.buttomAddtoolStripMenuItem1.Text = "下に追加 (&A)";
            // 
            // delToolStripMenuItem
            // 
            this.delToolStripMenuItem.Name = "delToolStripMenuItem";
            this.delToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.delToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.delToolStripMenuItem.Text = "削除 (&D)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(268, 6);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(271, 22);
            this.editToolStripMenuItem1.Text = "チャンネルまとめて編集 (&E)";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(10, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel5);
            this.splitContainer1.Size = new System.Drawing.Size(914, 554);
            this.splitContainer1.SplitterDistance = 581;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ChannelListView);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(5);
            this.panel4.Size = new System.Drawing.Size(579, 552);
            this.panel4.TabIndex = 0;
            // 
            // ChannelListView
            // 
            this.ChannelListView.CheckBoxes = true;
            this.ChannelListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chChannelName,
            this.chTunerID,
            this.chTPID,
            this.chRCNumber,
            this.chServiceID,
            this.chNetID,
            this.chTSID});
            this.ChannelListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChannelListView.FullRowSelect = true;
            this.ChannelListView.GridLines = true;
            this.ChannelListView.Location = new System.Drawing.Point(5, 5);
            this.ChannelListView.Name = "ChannelListView";
            this.ChannelListView.Size = new System.Drawing.Size(569, 542);
            this.ChannelListView.TabIndex = 0;
            this.ChannelListView.UseCompatibleStateImageBehavior = false;
            this.ChannelListView.View = System.Windows.Forms.View.Details;
            // 
            // chChannelName
            // 
            this.chChannelName.Text = "番組名";
            this.chChannelName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chChannelName.Width = 200;
            // 
            // chTunerID
            // 
            this.chTunerID.Text = "チューナ空間";
            this.chTunerID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chTunerID.Width = 65;
            // 
            // chTPID
            // 
            this.chTPID.Text = "TPID";
            this.chTPID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chRCNumber
            // 
            this.chRCNumber.Text = "リモコン番号";
            this.chRCNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chServiceID
            // 
            this.chServiceID.Text = "サービスID";
            this.chServiceID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chNetID
            // 
            this.chNetID.Text = "ネットワークID";
            this.chNetID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chTSID
            // 
            this.chTSID.Text = "TSID";
            this.chTSID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ChannelEditor);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(10);
            this.panel5.Size = new System.Drawing.Size(321, 552);
            this.panel5.TabIndex = 2;
            // 
            // ChannelEditor
            // 
            this.ChannelEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChannelEditor.Location = new System.Drawing.Point(10, 10);
            this.ChannelEditor.Name = "ChannelEditor";
            this.ChannelEditor.Size = new System.Drawing.Size(301, 532);
            this.ChannelEditor.TabIndex = 0;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.splitContainer1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 26);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.mainPanel.Size = new System.Drawing.Size(934, 564);
            this.mainPanel.TabIndex = 0;
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusTextBar});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 590);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(934, 22);
            this.mainStatusStrip.TabIndex = 1;
            this.mainStatusStrip.Text = "ステータス";
            // 
            // statusTextBar
            // 
            this.statusTextBar.Name = "statusTextBar";
            this.statusTextBar.Size = new System.Drawing.Size(0, 17);
            // 
            // MainEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 612);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainMS);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::TVTestChannelEditorGUI.Properties.Settings.Default, "StartupPosition", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::TVTestChannelEditorGUI.Properties.Settings.Default.StartupPosition;
            this.MainMenuStrip = this.mainMS;
            this.Name = "MainEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "チャンネル単一編集画面";
            this.mainMS.ResumeLayout(false);
            this.mainMS.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMS;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tunerSelectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListView ChannelListView;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PropertyGrid ChannelEditor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem channelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem upaddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem buttomAddtoolStripMenuItem1;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusTextBar;
        private System.Windows.Forms.ColumnHeader chChannelName;
        private System.Windows.Forms.ColumnHeader chTunerID;
        private System.Windows.Forms.ColumnHeader chTPID;
        private System.Windows.Forms.ColumnHeader chRCNumber;
        private System.Windows.Forms.ColumnHeader chServiceID;
        private System.Windows.Forms.ColumnHeader chNetID;
        private System.Windows.Forms.ColumnHeader chTSID;
    }
}

