using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MiniJava.Util
{
    public class Graph<N>
    {
        public Graph()
        {

        }

        private List<N> m_nodes = new List<N>();
        public List<N> Nodes
        {
            get { return m_nodes; }
        }

        public Node<N> NewNode(N content)
        {
            Node<N> n = new Node<N>(this, 0, content);
            return n;
        }

        private void Check(Node<N> n)
        {
            if (n.Graph != this)
                throw new Exception("Graph.AddEdge using nodes from the wrong graph");
        }

        public void AddEdge(Node<N> from, Node<N> to)
        {
            if (from.GoesTo(to))
                return;

            to.PredeccessorNodes.Add(from);
            from.SuccessorNodes.Add(to);
        }

        public void RemoveEdge(Node<N> from, Node<N> to)
        {
            to.PredeccessorNodes.Remove(from);
            from.SuccessorNodes.Remove(to);
        }
    }
}
