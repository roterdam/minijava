using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.AST;
using MiniJava.Visitors;

namespace MiniJava.Frontend.Visitors
{
    internal class PrettyPrintVisitor : BaseVisitor
    {
        string indentation = "";

        public PrettyPrintVisitor(ProgramAnalysis analysis)
            : base(analysis)
        {
        }
        #region Visitor Members

        public override void Visit(AddExpressionNode node)
        {
            Console.WriteLine(this.indentation + "+");
            indentation = indentation + "   ";
            node.expression1.Accept(this);
            node.expression2.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(AndExpressionNode node)
        {
            Console.WriteLine(this.indentation + "&&");
            indentation = indentation + "   ";
            node.expression1.Accept(this);
            node.expression2.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(ArrayAssignmentStatementNode node)
        {
            Console.WriteLine(this.indentation + "=           ---- Array Assignment Statement ----");
            indentation = indentation + "   ";
            Console.WriteLine(this.indentation + "<Array>[<Index>]");
            indentation = indentation + "   ";
            Console.WriteLine(this.indentation + "Array");
            indentation = indentation + "   ";
            node.identifier.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
            Console.WriteLine(this.indentation + "Index");
            indentation = indentation + "   ";
            node.indexExpression.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 6);
            node.assignExpression.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(ArrayLookupExpressionNode node)
        {
            Console.WriteLine(this.indentation + "<Array>[<Index>]");
            indentation = indentation + "   ";
            Console.WriteLine(this.indentation + "Array");
            indentation = indentation + "   ";
            node.arrayExpression.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
            Console.WriteLine(this.indentation + "Index");
            indentation = indentation + "   ";
            node.indexExpression.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 6);
        }

        public override void Visit(AssignmentStatementNode node)
        {
            Console.WriteLine(this.indentation + "=          ---- Assignment Statement ----");
            indentation = indentation + "   ";
            node.identifier.Accept(this);
            node.expression.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(BooleanConstantExpressionNode node)
        {
            Console.WriteLine(this.indentation + node.value);
        }

        public override void Visit(BooleanTypeNode node)
        {
            Console.WriteLine(this.indentation + "Type: Boolean");
        }

        public override void Visit(ClassDeclNode node)
        {
            Console.WriteLine(this.indentation + "Class <Identifier: " + node.className.name + ">           ---- Class Declaration ----");
            indentation = indentation + "   ";
            if (node.extendsClass != null)
                node.extendsClass.Accept(this);
            if (node.variableDeclList != null)
            {
                for (int x = 0; x < node.variableDeclList.variableDeclList.Count; x++)
                {
                    node.variableDeclList.VariableDeclAtIndex(x).Accept(this);
                }
            }
            if (node.methodDeclList != null)
            {
                for (int x = 0; x < node.methodDeclList.methodDeclList.Count; x++)
                {
                    node.methodDeclList.MethodDeclAtIndex(x).Accept(this);
                }
            }
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(ExtendsNode node)
        {
            Console.WriteLine(this.indentation + "extends class: " + node.className.name);
        }

        public override void Visit(IdentifierExpressionNode node)
        {
            Console.WriteLine(this.indentation + "Identifier: " + node.identifier.name);
        }

        public override void Visit(IdentifierNode node)
        {
            Console.WriteLine(this.indentation + "Identifier: " + node.name);
        }

        public override void Visit(IdentifierTypeNode node)
        {
            Console.WriteLine(this.indentation + "Type: " + node.name);
        }

        public override void Visit(IfStatementNode node)
        {
            Console.WriteLine(this.indentation + "If           --- Statement ----");
            indentation = indentation + "   ";
            node.expression.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
            Console.WriteLine(this.indentation + "Then");
            indentation = indentation + "   ";
            node.thenStatement.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
            Console.WriteLine(this.indentation + "Else");
            indentation = indentation + "   ";
            node.elseStatement.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(IntegerArrayTypeNode node)
        {
            Console.WriteLine(this.indentation + "Type: Integer[]");
        }

        public override void Visit(IntegerConstantExpressionNode node)
        {
            Console.WriteLine(this.indentation + "Integer Constant: " + node.value);
        }

        public override void Visit(IntegerTypeNode node)
        {
            Console.WriteLine(this.indentation + "Type: Integer");
        }

        public override void Visit(LengthExpressionNode node)
        {
            Console.WriteLine(this.indentation + "<Array>.Length");
            indentation = indentation + "   ";
            Console.WriteLine(this.indentation + "Array");
            indentation = indentation + "   ";
            node.expression.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 6);
        }

        public override void Visit(LessThanExpressionNode node)
        {
            Console.WriteLine(this.indentation + "<");
            indentation = indentation + "   ";
            node.expression1.Accept(this);
            node.expression2.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(MainClassDeclNode node)
        {
            Console.WriteLine(this.indentation + "Class <Identifier: " + node.classIdentifier.name + ">          ---- Main Class ---");
            indentation = indentation + "   ";
            Console.WriteLine(this.indentation + "public override static main          ---- Main Method ----");
            indentation = indentation + "   ";
            Console.WriteLine(this.indentation + "returns void");
            Console.WriteLine(this.indentation + "Parameter <Identifier: " + node.mainParameterIdentifier.name + ">");
            indentation = indentation + "   ";
            Console.WriteLine(this.indentation + "Type: String[]");
            indentation = indentation.Substring(0, indentation.Length - 3);
            node.statement.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 6);
        }

        public override void Visit(MethodCallExpressionNode node)
        {
            Console.WriteLine(this.indentation + "<object_instance>." + node.identifier.name + "()           ---- Method Call -----");
            indentation = indentation + "   ";
            Console.WriteLine(this.indentation + "object_instance");
            indentation = indentation + "   ";
            node.expression.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
            if (node.expressionList != null)
            {
                Console.WriteLine(this.indentation + "Paramaters");
                indentation = indentation + "   ";
                for (int x = 0; x < node.expressionList.expressionList.Count; x++)
                {
                    node.expressionList.ExpressionAtIndex(x).Accept(this);
                }
                indentation = indentation.Substring(0, indentation.Length - 3);
            }
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(MethodDeclNode node)
        {
            Console.WriteLine(this.indentation + "Method: " + node.methodName.name + "          ---- Method Declaration ----");
            indentation = indentation + "   ";
            Console.WriteLine(this.indentation + "returns " + node.methodType.toString());
            if (node.paramDeclList != null)
            {
                for (int x = 0; x < node.paramDeclList.paramDeclList.Count; x++)
                {
                    node.paramDeclList.ParamDeclAtIndex(x).Accept(this);
                }
            }
            if (node.variableDeclList != null)
            {
                for (int x = 0; x < node.variableDeclList.variableDeclList.Count; x++)
                {
                    node.variableDeclList.VariableDeclAtIndex(x).Accept(this);
                }
            }
            if (node.statementList != null)
            {
                for (int x = 0; x < node.statementList.statementList.Count; x++)
                {
                    node.statementList.StatementAtIndex(x).Accept(this);
                }
            }
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(MultiplyExpressionNode node)
        {
            Console.WriteLine(this.indentation + "*");
            indentation = indentation + "   ";
            node.expression1.Accept(this);
            node.expression2.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(NewIntegerArrayExpressionNode node)
        {
            Console.WriteLine(this.indentation + "New Integer[<Length>]");
            indentation = indentation + "   ";
            Console.WriteLine(this.indentation + "Length");
            indentation = indentation + "   ";
            node.expression.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 6);
        }

        public override void Visit(NewObjectExpressionNode node)
        {
            Console.WriteLine(this.indentation + "New Object <Class: " + node.identifier.name + ">");
        }

        public override void Visit(NotExpressionNode node)
        {
            Console.WriteLine(this.indentation + "!");
            indentation = indentation + "   ";
            node.expression.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(ParamDeclNode node)
        {
            Console.WriteLine(this.indentation + "Parameter <Identifier: " + node.identifier.name + ">");
            indentation = indentation + "   ";
            Console.WriteLine(this.indentation + "Type: " + node.type.toString());
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(ProgramNode node)
        {
            Console.WriteLine(this.indentation + "Program");
            indentation = indentation + "   ";
            node.mainClassDecl.Accept(this);
            for (int x = 0; x < node.classDeclList.classDeclList.Count; x++)
            {
                node.classDeclList.ClassDeclAtIndex(x).Accept(this);
            }
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(StatementBlockNode node)
        {
            Console.WriteLine(this.indentation + "{            ---- Start Statement Block ----");
            indentation = indentation + "   ";
            for (int x = 0; x < node.statementList.statementList.Count; x++)
            {
                node.statementList.StatementAtIndex(x).Accept(this);
            }
            indentation = indentation.Substring(0, indentation.Length - 3);
            Console.WriteLine(this.indentation + "}          ---- End Statement Block ----");
        }

        public override void Visit(SubtractExpressionNode node)
        {
            Console.WriteLine(this.indentation + "-");
            indentation = indentation + "   ";
            node.expression1.Accept(this);
            node.expression2.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(SystemOutPrintLnStatementNode node)
        {
            Console.WriteLine(this.indentation + "System.Out.Println           ---- Statement ----");
            indentation = indentation + "   ";
            node.expression.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(ThisExpressionNode node)
        {
            Console.WriteLine(this.indentation + "this");
        }

        public override void Visit(VariableDeclNode node)
        {
            Console.WriteLine(this.indentation + "Variable <Identifier: " + node.identifier.name + ">");
            indentation = indentation + "   ";
            Console.WriteLine(this.indentation + "Type: " + node.type.toString());
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(WhileStatementNode node)
        {
            Console.WriteLine(this.indentation + "While           ---- Statement ----");
            indentation = indentation + "   ";
            node.expression.Accept(this);
            node.statement.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        public override void Visit(ReturnStatementNode node)
        {
            Console.WriteLine(this.indentation + "return           ---- Statement ----");
            indentation = indentation + "   ";
            node.expression.Accept(this);
            indentation = indentation.Substring(0, indentation.Length - 3);
        }

        #endregion
    }
}
