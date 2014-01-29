using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class InvalidExpressionNode : ExpressionNode
    {
        public InvalidExpressionNode(int lineNumber)
        {
            this.lineNumber = lineNumber;
        }

        public override string toString()
        {
            return "InvalidExpression";
        }

        public override void Accept(MiniJava.Visitors.IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
