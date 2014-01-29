using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Types;

namespace MiniJava.AST
{
    public class IntegerTypeNode : TypeNode
    {
        public IntegerTypeNode(int lineNumber)
        {
            this.lineNumber = lineNumber;
            this.Type = IntType.Instance;
        }

        public override string toString()
        {
            return "Integer";
        }

        public override void Accept(MiniJava.Visitors.IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
