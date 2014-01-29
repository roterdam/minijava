using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class ParamDeclNode : BaseASTNode
    {
        public TypeNode type;
        public IdentifierNode identifier;

        public ParamDeclNode(TypeNode typeNode, IdentifierNode identifierNode, int lineNumber)
        {
            this.identifier = identifierNode;
            this.type = typeNode;
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
