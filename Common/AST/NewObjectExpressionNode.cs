﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class NewObjectExpressionNode : ExpressionNode
    {
        public IdentifierNode identifier;

        public NewObjectExpressionNode(IdentifierNode identifierNode, int lineNumber)
        {
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
