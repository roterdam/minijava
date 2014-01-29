using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class AssignmentStatementNode : StatementNode
    {
        public ExpressionNode expression;
        public IdentifierNode identifier;

        public AssignmentStatementNode(IdentifierNode identifierNode, ExpressionNode expressionNode, int lineNumber)
        {
            this.identifier = identifierNode;
            this.expression = expressionNode;
            this.lineNumber = lineNumber;
        }

        public override string toString()
        {
            return "";
        }

        public override void Accept(MiniJava.Visitors.IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
