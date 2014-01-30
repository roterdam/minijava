﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class FieldAccessExpressionNode : ExpressionNode
    {
        public ExpressionNode expression;
        public IdentifierNode identifier;

        public FieldAccessExpressionNode(ExpressionNode expressionNode, IdentifierNode identifierNode, int lineNumber)
        {
            this.expression = expressionNode;
            this.identifier = identifierNode;
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