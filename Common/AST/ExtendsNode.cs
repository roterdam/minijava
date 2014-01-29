using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class ExtendsNode : BaseASTNode
    {
        public IdentifierNode className;

        public ExtendsNode(IdentifierNode classNameNode, int lineNumber)
        {
            this.className = classNameNode;
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
