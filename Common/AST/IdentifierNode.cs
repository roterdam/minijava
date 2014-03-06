using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class IdentifierNode : BaseASTNode
    {
        public string name;

        public IdentifierNode(string identifierName, int lineNumber)
        {
            this.name = identifierName;
            this.lineNumber = lineNumber;
        }

        public override string toString()
        {
            return name;
        }

        public void Accept(MiniJava.Visitors.IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int GetHashCode()
        {
            int hashcode = 0;
            foreach (char c in this.name)
                hashcode += (int)c;

            return hashcode;
        }

        public override bool Equals(object obj)
        {
            return this.name == obj.ToString();
        }
    }
}
