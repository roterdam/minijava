using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Visitors;

namespace MiniJava.AST
{
    public abstract class BaseASTNode
    {
        public int lineNumber = -1;

        public abstract string toString();
    }
}
