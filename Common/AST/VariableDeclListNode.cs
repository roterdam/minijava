using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class VariableDeclListNode : BaseASTNode, IEnumerator, IEnumerable
    {
        private int position = -1;
        public List<VariableDeclNode> variableDeclList;

        public VariableDeclListNode(int lineNumber)
        {
            variableDeclList = new List<VariableDeclNode>();
            this.lineNumber = lineNumber;
        }

        public void AddVariableDecl(VariableDeclNode VariableDecl)
        {
            if(VariableDecl != null)
                this.variableDeclList.Add(VariableDecl);
        }

        public VariableDeclNode VariableDeclAtIndex(int index)
        {
            if (this.variableDeclList.Count <= index || index < 0)
                return null;

            return this.variableDeclList[index];
        }

        public override string toString()
        {
            return "";
        }

        //IEnumerator and IEnumerable require these methods.
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        //IEnumerator
        public bool MoveNext()
        {
            position++;
            return (position < variableDeclList.Count);
        }

        //IEnumerable
        public void Reset()
        {
            position = 0;
        }

        //IEnumerable
        public object Current
        {
            get { return variableDeclList[position]; }
        }
    }
}
