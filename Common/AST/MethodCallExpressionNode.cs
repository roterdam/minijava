using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class MethodCallExpressionNode : ExpressionNode
    {
        public ExpressionNode expression;
        public IdentifierNode identifier;
        public ExpressionListNode expressionList;

        public MethodCallExpressionNode(ExpressionNode expressionNode, IdentifierNode identifierNode, ExpressionListNode expressionListNode, int lineNumber)
        {
            this.expression = expressionNode;
            this.identifier = identifierNode;
            this.expressionList = expressionListNode;
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
