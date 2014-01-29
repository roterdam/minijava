using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class MainClassDeclNode : BaseASTNode
    {
        public IdentifierNode classIdentifier, mainParameterIdentifier;
        public StatementNode statement;

        public MainClassDeclNode(IdentifierNode classIdNode, IdentifierNode parameterIDNode, StatementNode statementNode, int lineNumber)
        {
            this.classIdentifier = classIdNode;
            this.mainParameterIdentifier = parameterIDNode;
            this.statement = statementNode;
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
