using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.AST;
using MiniJava.Definitions;

namespace MiniJava.Visitors
{
    public abstract class BaseVisitor : IVisitor
    {
        public ClassDefinition ClassBeingVisited;
        public MethodDefinition MethodBeingVisited;
        protected ProgramAnalysis Analysis;

        public BaseVisitor(ProgramAnalysis analysis)
        {
            this.Analysis = analysis;
        }

        #region Visitor Members

        public virtual void Visit(ProgramNode node)
        {
            node.mainClassDecl.Accept(this);
            for (int x = 0; x < node.classDeclList.classDeclList.Count; x++)
                node.classDeclList.ClassDeclAtIndex(x).Accept(this);
        }

        #region Declarations

        public virtual void Visit(MainClassDeclNode node)
        {
            ClassBeingVisited = Analysis.Environment.Classes.Lookup(node.classIdentifier.name);
            MethodBeingVisited = ClassBeingVisited.Methods.Lookup("main");
            node.classIdentifier.Accept(this);
            node.mainParameterIdentifier.Accept(this);
            node.statement.Accept(this);
        }

        public virtual void Visit(ClassDeclNode node)
        {
            ClassBeingVisited = Analysis.Environment.Classes.Lookup(node.className.name);
            if (node.extendsClass != null)
                node.extendsClass.Accept(this);
            if (node.variableDeclList != null)
                for (int x = 0; x < node.variableDeclList.variableDeclList.Count; x++)
                    node.variableDeclList.VariableDeclAtIndex(x).Accept(this);
            if (node.methodDeclList != null)
                for (int x = 0; x < node.methodDeclList.methodDeclList.Count; x++)
                    node.methodDeclList.MethodDeclAtIndex(x).Accept(this);
        }

        public virtual void Visit(ExtendsNode node)
        {
        }

        public virtual void Visit(VariableDeclNode node)
        {
            node.type.Accept(this);
            node.identifier.Accept(this);
        }

        public virtual void Visit(MethodDeclNode node)
        {
            MethodBeingVisited = ClassBeingVisited.Methods.Lookup(node.methodName.name);
            if (node.paramDeclList != null)
                for (int x = 0; x < node.paramDeclList.paramDeclList.Count; x++)
                    node.paramDeclList.ParamDeclAtIndex(x).Accept(this);

            if (node.variableDeclList != null)
                for (int x = 0; x < node.variableDeclList.variableDeclList.Count; x++)
                    node.variableDeclList.VariableDeclAtIndex(x).Accept(this);

            if (node.statementList != null)
                for (int x = 0; x < node.statementList.statementList.Count; x++)
                    node.statementList.StatementAtIndex(x).Accept(this);
        }

        public virtual void Visit(ParamDeclNode node)
        {
            node.type.Accept(this);
            node.identifier.Accept(this);
        }

        #endregion

        #region Types

        public virtual void Visit(BooleanTypeNode node)
        {
        }

        public virtual void Visit(IdentifierTypeNode node)
        {
        }

        public virtual void Visit(IntegerArrayTypeNode node)
        {
        }

        public virtual void Visit(IntegerTypeNode node)
        {
        }

        #endregion

        #region Expression

        public virtual void Visit(IntegerConstantExpressionNode node)
        {
        }

        public virtual void Visit(BooleanConstantExpressionNode node)
        {
        }

        public virtual void Visit(AddExpressionNode node)
        {
            node.expression1.Accept(this);
            node.expression2.Accept(this);
        }

        public virtual void Visit(SubtractExpressionNode node)
        {
            node.expression1.Accept(this);
            node.expression2.Accept(this);
        }

        public virtual void Visit(MultiplyExpressionNode node)
        {
            node.expression1.Accept(this);
            node.expression2.Accept(this);
        }

        public virtual void Visit(NewIntegerArrayExpressionNode node)
        {
            node.expression.Accept(this);
        }

        public virtual void Visit(NewObjectExpressionNode node)
        {
        }

        public virtual void Visit(NotExpressionNode node)
        {
            node.expression.Accept(this);
        }

        public virtual void Visit(AndExpressionNode node)
        {
            node.expression1.Accept(this);
            node.expression2.Accept(this);
        }

        public virtual void Visit(ArrayLookupExpressionNode node)
        {
            node.arrayExpression.Accept(this);
            node.indexExpression.Accept(this);
        }

        public virtual void Visit(IdentifierExpressionNode node)
        {
        }

        public virtual void Visit(LengthExpressionNode node)
        {
            node.expression.Accept(this);
        }

        public virtual void Visit(LessThanExpressionNode node)
        {
            node.expression1.Accept(this);
            node.expression2.Accept(this);
        }

        public virtual void Visit(MethodCallExpressionNode node)
        {
            node.expression.Accept(this);
            if (node.expressionList != null)
                for (int x = 0; x < node.expressionList.expressionList.Count; x++)
                    node.expressionList.ExpressionAtIndex(x).Accept(this);
        }

        public virtual void Visit(FieldAccessExpressionNode node)
        {
            node.expression.Accept(this);
            node.identifier.Accept(this);
        }

        public virtual void Visit(ThisExpressionNode node)
        {
        }

        public virtual void Visit(InvalidExpressionNode node)
        {
        }

        #endregion

        #region Statement

        public virtual void Visit(StatementBlockNode node)
        {
            for (int x = 0; x < node.statementList.statementList.Count; x++)
                node.statementList.StatementAtIndex(x).Accept(this);
        }

        public virtual void Visit(SystemOutPrintLnStatementNode node)
        {
            node.expression.Accept(this);
        }

        public virtual void Visit(ReturnStatementNode node)
        {
            node.expression.Accept(this);
        }

        public virtual void Visit(AssignmentStatementNode node)
        {
            node.identifier.Accept(this);
            node.expression.Accept(this);
        }

        public virtual void Visit(ArrayAssignmentStatementNode node)
        {
            node.identifier.Accept(this);
            node.indexExpression.Accept(this);
            node.assignExpression.Accept(this);
        }

        public virtual void Visit(FieldAssignmentStatementNode node)
        {
            node.classIdentifier.Accept(this);
            node.fieldIdentifier.Accept(this);
            node.assignExpression.Accept(this);
        }

        public virtual void Visit(IfStatementNode node)
        {
            node.expression.Accept(this);
            node.thenStatement.Accept(this);
            node.elseStatement.Accept(this);
        }

        public virtual void Visit(WhileStatementNode node)
        {
            node.expression.Accept(this);
            node.statement.Accept(this);
        }

        #endregion

        public virtual void Visit(IdentifierNode node)
        {
        }

        #endregion
    }
}
