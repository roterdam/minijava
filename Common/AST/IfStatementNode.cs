using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class IfStatementNode : StatementNode
    {
        public ExpressionNode expression;
        public StatementNode thenStatement, elseStatement;

        public IfStatementNode(ExpressionNode expressionNode, StatementNode thenStatementNode, StatementNode elseStatementNode, int lineNumber)
        {
            this.expression = expressionNode;
            this.thenStatement = thenStatementNode;
            this.elseStatement = elseStatementNode;
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
