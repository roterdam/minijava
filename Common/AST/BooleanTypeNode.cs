using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Types;

namespace MiniJava.AST
{
    public class BooleanTypeNode : TypeNode
    {
        public BooleanTypeNode(int lineNumber)
        {
            this.lineNumber = lineNumber;
            Type = BooleanType.Instance;
        }

        public override string toString()
        {
            return "Boolean";
        }

        public override void Accept(MiniJava.Visitors.IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
