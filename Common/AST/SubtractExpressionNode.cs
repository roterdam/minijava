﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class SubtractExpressionNode : ExpressionNode
    {
        public ExpressionNode expression1, expression2;

        public SubtractExpressionNode(ExpressionNode expressionNode1, ExpressionNode expressionNode2, int lineNumber)
        {
            this.expression1 = expressionNode1;
            this.expression2 = expressionNode2;
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
