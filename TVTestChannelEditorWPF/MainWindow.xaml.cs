using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TVTestChannelEditor;

namespace TVTestChannelEditorWPF
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<TunerInfo> Tuner { get; private set; }

        public MainWindow()
        {
            this.Tuner = new List<TunerInfo>
            {
                TunerInfo.CreateTestData()
            };

            InitializeComponent();

            this.gridChannles.ItemsSource = this.Tuner[0].Channels;
            this.gridChannles.SelectedCellsChanged +=
                (sender, e) =>
                {
                    this.editPanel.DataContext = null;
                    if (0 < e.AddedCells.Count)
                    {
                        var item = e.AddedCells[0];
                        var info = item.Item as ChannelInfo;
                        if (info != null)
                        {
                            this.editPanel.DataContext = info;
                        }
                    }
                };
        }
    }
}
