using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class ClassDeclNode : BaseASTNode
    {
        public IdentifierNode className;
        public ExtendsNode extendsClass;
        public VariableDeclListNode variableDeclList;
        public MethodDeclListNode methodDeclList;

        public ClassDeclNode(IdentifierNode classNameNode, ExtendsNode extendsClassNode, VariableDeclListNode variableDeclListNode, MethodDeclListNode methodDeclListNode, int lineNumber)
        {
            this.className = classNameNode;
            this.extendsClass = extendsClassNode;
            this.variableDeclList = variableDeclListNode;
            this.methodDeclList = methodDeclListNode;
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
