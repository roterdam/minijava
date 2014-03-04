using MiniJava.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Backend.Visitors
{
    class LivenessVisitor : BaseVisitor
    {
        HashSet<string> m_R = new HashSet<string>();
        HashSet<string> m_W = new HashSet<string>();

        private Dictionary<AST.BaseASTNode, HashSet<string>> m_livenessAtNode
            = new Dictionary<AST.BaseASTNode, HashSet<string>>();

        public LivenessVisitor(ProgramAnalysis analysis)
            : base(analysis)
        {

        }

        #region Visitor Members
        public override void Visit(AST.ProgramNode node)
        {
            base.Visit(node);
        }

        #region Declarations
        public override void Visit(AST.MainClassDeclNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.MethodDeclNode node)
        {
            MethodBeingVisited = ClassBeingVisited.Methods.Lookup(node.methodName.name);
            
            if (node.paramDeclList != null)
                foreach (AST.ParamDeclNode paramDecl in node.paramDeclList)
                    paramDecl.Accept(this);

            if (node.variableDeclList != null)
                foreach (AST.VariableDeclNode variableDecl in node.variableDeclList)
                    variableDecl.Accept(this);

            if (node.statementList != null)
            {
                var reverseList = node.statementList.statementList;
                reverseList.Reverse();
                
                HashSet<string> afterLiveness = new HashSet<string>();

                foreach (AST.StatementNode statement in reverseList)
                {
                    m_R.Clear();
                    m_W.Clear();
                    statement.Accept(this);
                    afterLiveness.ExceptWith(m_W);
                    afterLiveness.UnionWith(m_R);

                    m_livenessAtNode[statement] = new HashSet<string>(afterLiveness);
                }
            }
        }

        public override void Visit(AST.ClassDeclNode node)
        {
            base.Visit(node);
        }
        #endregion

        #region Expression
        public override void Visit(AST.IntegerConstantExpressionNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.BooleanConstantExpressionNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.AddExpressionNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.SubtractExpressionNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.MultiplyExpressionNode node)
        {
            node.expression1.Accept(this);
            node.expression2.Accept(this);
            base.Visit(node);
        }

        public override void Visit(AST.NewIntegerArrayExpressionNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.NewObjectExpressionNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.IdentifierExpressionNode node)
        {
            m_R.Add(node.identifier.name);
            base.Visit(node);
        }

        public override void Visit(AST.ArrayLookupExpressionNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.LengthExpressionNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.MethodCallExpressionNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.FieldAccessExpressionNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.ThisExpressionNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.NotExpressionNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.AndExpressionNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.LessThanExpressionNode node)
        {
            base.Visit(node);
        }
        #endregion

        #region Statement
        public override void Visit(AST.SystemOutPrintLnStatementNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.ReturnStatementNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.AssignmentStatementNode node)
        {
            m_W.Add(node.identifier.name);
            node.expression.Accept(this);

            base.Visit(node);
        }

        public override void Visit(AST.ArrayAssignmentStatementNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.FieldAssignmentStatementNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.WhileStatementNode node)
        {
            base.Visit(node);
        }

        public override void Visit(AST.IfStatementNode node)
        {
            base.Visit(node);
        }
        #endregion

        #endregion
    }
}
