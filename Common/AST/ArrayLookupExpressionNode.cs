using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class ArrayLookupExpressionNode : ExpressionNode
    {
        public ExpressionNode arrayExpression, indexExpression;

        public ArrayLookupExpressionNode(ExpressionNode arrayExpressionNode, ExpressionNode indexExpressionNode, int lineNumber)
        {
            this.arrayExpression = arrayExpressionNode;
            this.indexExpression = indexExpressionNode;
            this.lineNumber = lineNumber;
        }

        public override string toString()
        {
            return "";
        }

        public override void Accept(MiniJava.Visitors.IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
