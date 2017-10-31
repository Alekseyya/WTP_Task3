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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Andersen.Tree;

namespace WPF_Andersen
{
    /// <summary>
    /// Логика взаимодействия для ComboTree.xaml
    /// </summary>
    public partial class ComboTree
    {
        public ComboTree()
        {
            InitializeComponent();
        }

        protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
        {
            MySelectedItem = e.NewValue as SuperNode;
            base.OnSelectedItemChanged(e);
        }

        public static readonly DependencyProperty MySelectedItemProperty =
            DependencyProperty.Register("MySelectedItem", typeof(SuperNode), typeof(ComboTree), new UIPropertyMetadata(Callback));

        private static void Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = d as ComboTree;
            (sender.DataContext as UpdateViewModel).SelectedNode = e.NewValue as SuperNode;
        }

        private SuperNode _mySelectedItem;
        public SuperNode MySelectedItem
        {
            get { return (SuperNode) GetValue(MySelectedItemProperty); }
            set { SetValue(MySelectedItemProperty, value); }
        }
    }
}
