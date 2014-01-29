using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class WhileStatementNode : StatementNode
    {
        public ExpressionNode expression;
        public StatementNode statement;

        public WhileStatementNode(ExpressionNode expressionNode, StatementNode statementNode, int lineNumber)
        {
            this.expression = expressionNode;
            this.statement = statementNode;
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
