using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithmics
{
    public class Node<T>
    {
        T value;
        List<Node<T>> nodes = new List<Node<T>>();

        public Node() 
        { }

        public Node(T value)
        {
            this.value = value;
        }

        public T Value { get { return value; } }

        public void AddChild(Node<T> node)
        {
            nodes.Add(node);
        }

        public IList<Node<T>> Children { get { return nodes; } }
    }
}
