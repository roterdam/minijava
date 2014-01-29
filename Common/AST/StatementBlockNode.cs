﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class StatementBlockNode : StatementNode
    {
        public StatementListNode statementList;

        public StatementBlockNode(StatementListNode statementListNode, int lineNumber)
        {
            this.statementList = statementListNode;
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
