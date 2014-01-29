using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class FieldAssignmentStatementNode : StatementNode
    {
        public IdentifierNode classIdentifier; 
        public IdentifierNode fieldIdentifier;
        public ExpressionNode assignExpression;

        public FieldAssignmentStatementNode(IdentifierNode classIdentifier, IdentifierNode fieldIdentifier, ExpressionNode assignExpressionNode, int lineNumber)
        {
            this.classIdentifier = classIdentifier;
            this.fieldIdentifier = fieldIdentifier;
            this.assignExpression = assignExpressionNode;
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
