using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class IntegerConstantExpressionNode : ExpressionNode
    {
        public int value;

        public IntegerConstantExpressionNode(int value, int lineNumber)
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
