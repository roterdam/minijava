using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class BooleanConstantExpressionNode : ExpressionNode
    {
        public bool value;

        public BooleanConstantExpressionNode(bool value, int lineNumber)
        {
            this.value = value;
            this.lineNumber = lineNumber;
        }

        public override string toString()
        {
            return value.ToString();
        }

        public override void Accept(MiniJava.Visitors.IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
