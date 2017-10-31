using System.Collections.Generic;

namespace WPF_Andersen.Tree
{
    public class BinaryTreeNode<T> : Node<T>
    {
        public BinaryTreeNode() : base() { }
        public BinaryTreeNode(T data) : base(data, null) { }
        public BinaryTreeNode(T data, List<BinaryTreeNode<T>> nodas)
        {
            base.Value = data;
            NodeList<T> children = new NodeList<T>(nodas.Count);
            for (int i = 0; i < nodas.Count; i++)
            {
                children[i] = nodas[i];
            }
            
            base.Neighbors = children;
        }

        public BinaryTreeNode<T> Element
        {
            get
            {
                if (base.Neighbors == null)
                    return null;
                else
                    return (BinaryTreeNode<T>)base.Neighbors[0];
            }
            set
            {
                if (base.Neighbors == null)
                    base.Neighbors = new NodeList<T>(1);

                base.Neighbors[0] = value;
            }
        }

        //public BinaryTreeNode<T> Right
        //{
        //    get
        //    {
        //        if (base.Neighbors == null)
        //            return null;
        //        else
        //            return (BinaryTreeNode<T>)base.Neighbors[1];
        //    }
        //    set
        //    {
        //        if (base.Neighbors == null)
        //            base.Neighbors = new NodeList<T>(2);

        //        base.Neighbors[1] = value;
        //    }
        //}
    }
}
