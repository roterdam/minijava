using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class ClassDeclListNode : BaseASTNode
    {
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

    }
}
