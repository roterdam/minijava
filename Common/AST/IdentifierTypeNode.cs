using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Types;

namespace MiniJava.AST
{
    public class IdentifierTypeNode : TypeNode
    {
        public string name;

        public IdentifierTypeNode(string identifierName, int lineNumber)
        {
            Type = new ClassType(identifierName);
            this.name = identifierName;
            this.lineNumber = lineNumber;
        }

        public override string toString()
        {
            return name;
        }

        public override void Accept(MiniJava.Visitors.IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
