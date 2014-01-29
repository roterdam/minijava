using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class ThisExpressionNode : ExpressionNode
    {
        public ThisExpressionNode(int lineNumber)
        {
            this.lineNumber = lineNumber;
        }

        public override string toString()
        {
            return "this";
        }

        public override void Accept(MiniJava.Visitors.IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
