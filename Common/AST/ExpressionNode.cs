using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Visitors;
using MiniJava.Types;

namespace MiniJava.AST
{
    public abstract class ExpressionNode : BaseASTNode
    {
        public BaseType ExpressionType;
        public abstract void Accept(IVisitor v);
    }
}
