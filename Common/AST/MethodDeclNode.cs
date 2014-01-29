using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.AST
{
    public class MethodDeclNode : BaseASTNode
    {
        public TypeNode methodType;
        public IdentifierNode methodName;
        public ParamDeclListNode paramDeclList;
        public VariableDeclListNode variableDeclList;
        public StatementListNode statementList;

        public MethodDeclNode(TypeNode typeNode, IdentifierNode identifierNode, ParamDeclListNode paramListNode, VariableDeclListNode variableListNode, StatementListNode statementListNode, int lineNumber)
        {
            this.methodType = typeNode;
            this.methodName = identifierNode;
            this.paramDeclList = paramListNode;
            this.variableDeclList = variableListNode;
            this.statementList = statementListNode;
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
