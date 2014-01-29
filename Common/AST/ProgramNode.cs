using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class ProgramNode : BaseASTNode
    {
        public MainClassDeclNode mainClassDecl;
        public ClassDeclListNode classDeclList;

        public ProgramNode(MainClassDeclNode mainNode, ClassDeclListNode classListNode, int lineNumber)
        {
            this.mainClassDecl = mainNode;
            this.classDeclList = classListNode;
            this.lineNumber = lineNumber;
        }

        public override string toString()
        {
            return "";
        }

        public void Accept(MiniJava.Visitors.IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
