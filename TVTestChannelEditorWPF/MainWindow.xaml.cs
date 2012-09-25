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
        public TunerList Tuner { get; private set; }

        public MainWindow()
        {
            this.Tuner = TunerList.ReadConfig(@"..\..\..\BonDriver_ptmr.ch2");

            InitializeComponent();

            this.tabTuner.ItemsSource = this.Tuner.Tuners;
            ////this.gridChannles.ItemsSource = this.Tuner[0].Channels;
            //this.tabTuner.SelectionChanged +=
            //    (sender, e) =>
            //    {
            //        if (0 < e.AddedItems.Count)
            //        {
            //        }
            //    };
            //this.gridChannles.SelectedCellsChanged +=
            //    (sender, e) =>
            //    {
            //        this.editPanel.DataContext = null;
            //        if (0 < e.AddedCells.Count)
            //        {
            //            var item = e.AddedCells[0];
            //            var info = item.Item as ChannelInfo;
            //            if (info != null)
            //            {
            //                this.editPanel.DataContext = info;
            //            }
            //        }
            //    };
        }

        private void StatusBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Debugger.Break();
        }

        private void gridChannels_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //var item = sender as DataGrid;
            //this.editPanel.DataContext = item.SelectedItem;
        }
    }
}
