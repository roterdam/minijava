using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class VariableDeclNode : BaseASTNode
    {
        public TypeNode type;
        public IdentifierNode identifier;

        public VariableDeclNode(TypeNode typeNode, IdentifierNode identifierNode, int lineNumber)
        {
            this.type = typeNode;
            this.identifier = identifierNode;
            this.lineNumber = lineNumber;
        }

        public override string toString()
        {
            return "";
        }

        public void Accept(MiniJava.Visitors.IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
