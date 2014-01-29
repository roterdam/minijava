using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class ExpressionListNode : BaseASTNode
    {
        public List<ExpressionNode> expressionList;

        public ExpressionListNode(int lineNumber)
        {
            this.expressionList = new List<ExpressionNode>();
            this.lineNumber = lineNumber;
        }

        public void AddExpression(ExpressionNode expression)
        {
            this.expressionList.Add(expression);
        }

        public ExpressionNode ExpressionAtIndex(int index)
        {
            if (this.expressionList.Count <= index || index < 0)
                return null;

            return this.expressionList[index];
        }


        public override string toString()
        {
            return "";
        }

    }
}
