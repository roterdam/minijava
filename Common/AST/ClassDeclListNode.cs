using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class ClassDeclListNode : BaseASTNode, IEnumerator, IEnumerable
    {
        private int position = -1;
        public List<ClassDeclNode> classDeclList;

        public ClassDeclListNode(int lineNumber)
        {
            classDeclList = new List<ClassDeclNode>();
            this.lineNumber = lineNumber;
        }

        public void AddClassDecl(ClassDeclNode classDecl)
        {
            this.classDeclList.Add(classDecl);
        }

        public ClassDeclNode ClassDeclAtIndex(int index)
        {
            if (this.classDeclList.Count <= index || index < 0)
                return null;
           
            return this.classDeclList[index];
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
            return (position < classDeclList.Count);
        }

        //IEnumerable
        public void Reset()
        {
            position = 0;
        }

        //IEnumerable
        public object Current
        {
            get { return classDeclList[position]; }
        }
    }
}
