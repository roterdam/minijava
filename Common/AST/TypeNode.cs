using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Visitors;
using MiniJava.Types;

namespace MiniJava.AST
{
    abstract public class TypeNode : BaseASTNode
    {
        public MiniJava.Types.BaseType Type;
        public abstract void Accept(IVisitor v);
    }
}
