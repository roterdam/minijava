using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class ArrayAssignmentStatementNode : StatementNode
    {
        public ExpressionNode indexExpression, assignExpression;
        public IdentifierNode identifier;

        public ArrayAssignmentStatementNode(IdentifierNode identifierNode, ExpressionNode indexExpressionNode, ExpressionNode assignExpressionNode, int lineNumber)
        {
            this.identifier = identifierNode;
            this.assignExpression = assignExpressionNode;
            this.indexExpression = indexExpressionNode;
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
