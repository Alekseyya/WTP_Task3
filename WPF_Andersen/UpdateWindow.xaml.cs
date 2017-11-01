using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Andersen.Tree;

namespace WPF_Andersen
{
    /// <summary>
    /// Логика взаимодействия для UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        public UpdateWindow()
        {
            InitializeComponent();
            DataContext = new UpdateViewModel();
        }


        //private void Test_click(object sender, RoutedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void header_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    //PopupTest.Placement = System.Windows.Controls.Primitives.PlacementMode.RelativePoint;
        //    //PopupTest.VerticalOffset = header.Height;
        //    //PopupTest.StaysOpen = true;
        //    //PopupTest.Height = Tree1.Height;
        //    //PopupTest.Width = Tree1.Width;
        //    //PopupTest.IsOpen = true;
        //}

        //private void Tree1_Initialized(object sender, EventArgs e)
        //{
        //    var trv = sender as TreeView;
        //    var trvItem = new TreeViewItem() { Header = "Initialized item" };
        //    var trvItemSel = trv.Items[1] as TreeViewItem;
        //    trvItemSel.Items.Add(trvItem);
        //}

        //private void Tree1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    var trv = sender as TreeView;
        //    if (trv != null)
        //    {
        //        var trvItem = trv.SelectedItem as SuperNode;
        //        //header.sele = trvItem.Header.ToString();
        //    }
        //    //PopupTest.IsOpen = false;
        //}

        //private void Tree_Selection(object sender, SelectionChangedEventArgs e)
        //{
        //    var trv = sender as ComboBox;
        //    var aa = 1;
        //}

        
    }
}
