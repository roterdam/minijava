using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Util
{
    public class NodeComparer<N> : IEqualityComparer<Node<N>>
    {
        public bool Equals(Node<N> n1, Node<N> n2)
        {
            return n1 == n2;
        }

        public int GetHashCode(Node<N> n)
        {
            return n.Content.GetHashCode();
        }
    }

    public class Node<N>
    {

        public Node(Graph<N> graph, int nodeID, N content)
        {
            m_nodeID = nodeID;
            m_graph = graph;
            m_content = content;
        }

        private int m_nodeID;
        public int NodeID
        {
            get { return m_nodeID; }
        }

        private Graph<N> m_graph;
        public Graph<N> Graph
        {
            get { return m_graph; }
        }

        private N m_content;
        public N Content
        {
            get { return m_content; }
            set { m_content = value; }
        }

        private HashSet<Node<N>> m_successors = new HashSet<Node<N>>();
        public HashSet<Node<N>> SuccessorNodes
        {
            get { return m_successors; }
        }

        private HashSet<Node<N>> m_predecessors = new HashSet<Node<N>>();
        public HashSet<Node<N>> PredeccessorNodes
        {
            get { return m_predecessors; }
        }

        private List<Node<N>> AdjacentNodes
        {
            get { return m_successors.Union(m_predecessors).ToList(); }
        }

        public int InDegree
        {
            get { return m_predecessors.Count; }
        }

        public int OutDegree
        {
            get { return m_successors.Count; }
        }

        public int Degree
        {
            get { return InDegree + OutDegree; }
        }

        public bool GoesTo(Node<N> n)
        {
            return m_successors.Contains(n);
        }

        public bool ComesFrom(Node<N> n)
        {
            return m_predecessors.Contains(n);
        }

        public bool IsAdjacentNode(Node<N> n)
        {
            return GoesTo(n) || ComesFrom(n);
        }

        public override string ToString()
        {
            return m_nodeID.ToString();
        }
    }
}
