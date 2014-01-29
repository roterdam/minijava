using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Types;

namespace MiniJava.AST
{
    public class IntegerArrayTypeNode : TypeNode
    {
        public IntegerArrayTypeNode(int lineNumber)
        {
            ArrayType IntArrayType = new ArrayType("int[]");
            IntArrayType.ElementType = IntType.Instance;
            IntArrayType.NoOfDimensions = 1;
            Type = IntArrayType;
            this.lineNumber = lineNumber;
        }

        public override string toString()
        {
            return "Integer[]";
        }

        public override void Accept(MiniJava.Visitors.IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
