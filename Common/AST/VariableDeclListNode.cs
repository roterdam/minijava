using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class VariableDeclListNode : BaseASTNode
    {
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

    }
}
