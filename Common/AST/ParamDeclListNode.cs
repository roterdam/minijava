using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class ParamDeclListNode : BaseASTNode, IEnumerator, IEnumerable
    {
        private int position = -1;
        public List<ParamDeclNode> paramDeclList;

        public ParamDeclListNode(int lineNumber)
        {
            paramDeclList = new List<ParamDeclNode>();
            this.lineNumber = lineNumber;
        }

        public void AddParamDecl(ParamDeclNode paramDecl)
        {
            this.paramDeclList.Add(paramDecl);
        }

        public ParamDeclNode ParamDeclAtIndex(int index)
        {
            if (this.paramDeclList.Count <= index || index < 0)
                return null;

            return this.paramDeclList[index];
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
            return (position < paramDeclList.Count);
        }

        //IEnumerable
        public void Reset()
        {
            position = 0;
        }

        //IEnumerable
        public object Current
        {
            get { return paramDeclList[position]; }
        }
    }
}
