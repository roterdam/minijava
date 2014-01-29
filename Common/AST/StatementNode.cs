using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Visitors;

namespace MiniJava.AST
{
    public abstract class StatementNode : BaseASTNode
    {
        public abstract void Accept(IVisitor v);
    }
}
