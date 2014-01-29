using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class MethodDeclListNode : BaseASTNode
    {
        public List<MethodDeclNode> methodDeclList;

        public MethodDeclListNode(int lineNumber)
        {
            methodDeclList = new List<MethodDeclNode>();
            this.lineNumber = lineNumber;
        }

        public void AddMethodDecl(MethodDeclNode methodDecl)
        {
            this.methodDeclList.Add(methodDecl);
        }

        public MethodDeclNode MethodDeclAtIndex(int index)
        {
            if (this.methodDeclList.Count <= index || index < 0)
                return null;

            return this.methodDeclList[index];
        }

        public override string toString()
        {
            return "";
        }
    }
}
