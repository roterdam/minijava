using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class StatementListNode : BaseASTNode
    {
        public List<StatementNode> statementList;

        public StatementListNode(int lineNumber)
        {
            this.statementList = new List<StatementNode>();
            this.lineNumber = lineNumber;
        }

        public void AddStatement(StatementNode statement)
        {
            this.statementList.Add(statement);
        }

        public StatementNode StatementAtIndex(int index)
        {
            if (this.statementList.Count <= index || index < 0)
                return null;

            return this.statementList[index];
        }

        public override string toString()
        {
            return "";
        }

    }
}
