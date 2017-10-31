using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для ComboboxTreeView.xaml
    /// </summary>
    public partial class ComboboxTreeView
    {
        public ComboboxTreeView()
        {
            InitializeComponent();
        }
        
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
          // TestSelectedItems = (e.AddedItems[0] as ComboTree).MySelectedItem.Name;            
           base.OnSelectionChanged(e);
        }
        

        private string _testSelectedItems;
        public string TestSelectedItems
        {
            get { return (string)GetValue(TestSelectedItem); }
            set { SetValue(TestSelectedItem, value); }
        }


        public static readonly DependencyProperty TestSelectedItem =
            DependencyProperty.Register("TestSelectedItems", typeof(string), 
                typeof(ComboboxTreeView), new UIPropertyMetadata(Callback));

        private static void Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = d as ComboboxTreeView;
            sender.SelectedItem = "aaaaa";
            var aa = (sender.DataContext as UpdateViewModel).SelectedNode;
            //(sender.DataContext as UpdateViewModel).SelectedNode = e.NewValue as SuperNode;
        }

    }
}
