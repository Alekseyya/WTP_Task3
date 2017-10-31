using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using DAL.Repositories.Base;
using Model.Entities;
using WPF_Andersen.Tree;

namespace WPF_Andersen
{
    public class UpdateViewModel : PropertyChangedEvent
    {
        #region Рабочий код
        //private Client _selectedClient;
        //private ICommand _updateMember;

        //public Client SelectedClient
        //{
        //    get { return _selectedClient; }
        //    set
        //    {
        //        _selectedClient = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public ICommand UpdateMember
        //{
        //    get
        //    {
        //        if (_updateMember == null)
        //        {
        //            Update();
        //        }
        //        return _updateMember;
        //    }
        //}

        //public void Update()
        //{
        //    _updateMember = new RelayCommand(obj =>
        //    {
        //        UpdateMemberOnDatabase();
        //        MessageBox.Show("Update competed");
        //    });
        //}

        //public void UpdateMemberOnDatabase()
        //{
        //    using (IClientRepository repo = IoC.IoC.Get<IClientRepository>())
        //    {
        //        var client = SelectedClient;
        //        repo.Update(client);
        //    }
        //}

        //public UpdateViewModel(Client client)
        //{
        //    SelectedClient = client;
        //}
        #endregion

        /*
                public SuperNode SelectedItem
                {
                    get { return (SuperNode)GetValue(SelectedItemProperty); }
                    set { SetValue(SelectedItemProperty, value); }
                }

                // Using a DependencyProperty as the backing store for SelectedItm.  This enables animation, styling, binding, etc...  
                public static readonly DependencyProperty SelectedItemProperty =
                    DependencyProperty.Register("SelectedItem", typeof(SuperNode), typeof(UpdateViewModel), new UIPropertyMetadata());

                private SuperNode GetValue(DependencyProperty comboboxSelectedItem)
                {
                    var aa = 0;
                    throw new NotImplementedException();
                }*/

        public UpdateViewModel()
        {
            var n1 = new SuperNode { Name = "First" };
            var n2 = new SuperNode { Name = "Second" };
            var n3 = new SuperNode { Name = "Third" };
            var n4 = new SuperNode { Name = "First-First" };
            var n5 = new SuperNode { Name = "Second-First" };
            var n6 = new SuperNode { Name = "First-First-First" };

            Directories = new ObservableCollection<SuperNode>();
            n2.Children.Add(n5);
            n4.Children.Add(n6);
            n1.Children.Add(n4);
            Directories.Add(n1);
            Directories.Add(n2);
            Directories.Add(n3);

            Nodes = new ObservableCollection<string>();
            Nodes.Add(n1.Name);
            Nodes.Add(n2.Name);
            Nodes.Add(n3.Name);
            Nodes.Add(n4.Name);
            Nodes.Add(n5.Name);
            Nodes.Add(n6.Name);


            LoadDirectories = new RelayCommand(obj =>
            {
                Load();
                /*var path = @"C:\111";
                SetDirection(path);
                OutputAllSubfolders();*/
                //Directories = new ObservableCollection<string>(ComboList);
            });
            /*

            Nodes = new ObservableCollection<string>();
            foreach (var node in Directories)
            {
                Nodes.Add(node.Name);
            }*/
            
        }

        public int _level;
        public int Level { get { return _level; } set { _level = value; } }

        public List<string> SelectedDirectory;
        public List<string> TmpDirectories;
        public static List<string> ComboList;

        private SuperNode _selectedNode;

        public SuperNode SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                _selectedNode = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _nodes;
        public ObservableCollection<string> Nodes
        {
            get
            {
                return _nodes;
            }
            set
            {
                _nodes = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SuperNode> _directories;
        public ObservableCollection<SuperNode> Directories
        {
            get
            {
                return _directories;
            }
            set
            {
                _directories = value;
                OnPropertyChanged();
            }
        }

        private ICommand _loadDirectories;
        public ICommand LoadDirectories
        {
            get
            {
                return _loadDirectories;
            }
            set
            {
                _loadDirectories = value;
                OnPropertyChanged();
            }
        }

        private ICommand _test;

        public ICommand Test
        {
            get
            {
                _test = new RelayCommand(obj =>
                {
                    //Node<string> node = new Node<string>("11", new NodeList<string>(2));

                    BinaryTree<string> btree = new BinaryTree<string>(); //111

                    List<BinaryTreeNode<string>> nodeList = new List<BinaryTreeNode<string>>();

                    BinaryTreeNode<string> n11 = new BinaryTreeNode<string>();
                    n11.Value = "11";

                    BinaryTreeNode<string> n12 = new BinaryTreeNode<string>();
                    n12.Value = "12";

                    nodeList.Add(n11);
                    nodeList.Add(n12);


                    btree.Root = new BinaryTreeNode<string>("11", nodeList);
                    //11
                    //btree.Root.Left = new BinaryTreeNode<int>(2);

                    //Directories = new ObservableCollection<string>(ComboList);
                });
                return _test;
            }
        }


        private void Load()
        {
            
            /* _loadDirectories = new RelayCommand(obj =>
             {
                 var path = @"C:\111";
                 SetDirection(path);
                 OutputAllSubfolders();
                 //Directories = new ObservableCollection<string>(ComboList);
             });*/
        }

        public void OutputAllSubfolders()
        {
            //пока не пустой список 
            while (SelectedDirectory.Count != 0)
            {
                var findElemet = SelectedDirectory[0];
                TmpDirectories.Add(findElemet);

                //Устанавлием уровень
                Level = 1;
                ComboList.Add(new ComboItem(findElemet, Level).ToString());

                CLRTree(findElemet, SelectedDirectory);

                //выйдет если проверит одно дерево
                while (TmpDirectories.Count != 0)
                {
                    var findElement = TmpDirectories.Last();
                    CLRTree(findElement, SelectedDirectory);
                }
                //ComboList.Clear(); //Очистку комбобокса пока не удалять!!
            }
        }

        public void CLRTree(string findElemet, List<string> directions)
        {
            if (TmpDirectories.Count == 0)
                TmpDirectories.Add(findElemet);

            while (true)
            {
                findElemet = FindEqualsSubfolder(findElemet, directions); 
                if (findElemet != "")
                {
                    ++Level;
                    ComboList.Add(new ComboItem(findElemet, Level).ToString());
                    TmpDirectories.Add(findElemet);
                }
                else
                {
                    var element = TmpDirectories.Last();
                    TmpDirectories.Remove(element);
                    SelectedDirectory.Remove(element);
                    --Level;
                    break;
                }
            }
        }

        public string FindEqualsSubfolder(string findElement, List<string> directories)
        {
            foreach (var path in directories)
            {
                if (path.Contains(findElement + "\\"))
                {
                    return path;
                }
            }
            return "";
        }

        public void SetDirection(string path)
        {
            SelectedDirectory = Directory.GetDirectories(path, "*", SearchOption.AllDirectories).ToList();
            TmpDirectories = new List<string>();
            ComboList = new List<string>();
        }

        public async Task<List<string>> GetDirectories(string path, string searchPattern = "*",
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            //Если выбрана С:\\111\11
            if (searchOption == SearchOption.TopDirectoryOnly)
                return await Task.Run(() =>
                {
                    return Directory.GetDirectories(path, searchPattern).ToList();
                });

            //Все директории
            return await Task.Run(() =>
            {
                return Directory.GetDirectories(path, searchPattern).ToList();
            });
        }

       
        
    }
}