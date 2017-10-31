using System.Collections.Generic;

namespace WPF_Andersen.Tree
{
    public class SuperNode
    {
        public SuperNode()
        {
            Children = new List<SuperNode>();
        }
        public string Name { get; set; }
        public List<SuperNode> Children { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    class Tree
    {
        public Tree()
        {
            Nodes = new List<SuperNode>();
            var n1 = new SuperNode {Name = "First"};
            var n2 = new SuperNode { Name = "Second" };
            var n3 = new SuperNode { Name = "Third" };
            var n4 = new SuperNode { Name = "First-First" };
            var n5 = new SuperNode { Name = "Second-First" };
            var n6 = new SuperNode { Name = "First-First-First" };

            n2.Children.Add(n5);
            n4.Children.Add(n6);
            n1.Children.Add(n4);
            Nodes.Add(n1);
            Nodes.Add(n2);
            Nodes.Add(n3);
        }

        public List<SuperNode> Nodes { get; set; }
    }
}
