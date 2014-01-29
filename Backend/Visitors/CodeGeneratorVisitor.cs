using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MiniJava.AST;
using MiniJava.Visitors;
using MiniJava.Definitions;

namespace MiniJava.Backend.Visitors
{
    internal class CodeGeneratorVisitor : BaseVisitor
    {
        private TextWriter Out;
        private LabelGenerator WhileLabelGenerator;
        private LabelGenerator ElseLabelGenerator;
        private LabelGenerator BooleanExpressionLabelGenerator;

        #region Gen

        private void GenText(string text)
        {
            Out.WriteLine(text);
        }

        private void Gen(string opCode)
        {
            Gen(opCode, "", "", "");
        }

        private void Gen(string opCode, string operand1)
        {
            Gen(opCode, operand1, "", "");
        }

        private void Gen(string opCode, string operand1, string operand2)
        {
            Gen(opCode, operand1, operand2, "");
        }

        private void Gen(string opCode, string operand1, string operand2, string comment)
        {
            Out.Write("\t");
            Out.Write(opCode);
            if (operand1 != null && operand1.Length > 0)
            {
                Out.Write(" ");
                Out.Write(operand1);
            }
            if (operand2 != null && operand2.Length > 0)
            {
                Out.Write(",");
                Out.Write(operand2);
            }
            if(comment != null && comment.Length > 0)
            {
                Out.Write("\t\t; ");
                Out.Write(comment);
            }
            Out.WriteLine("");
        }

        #endregion

        public CodeGeneratorVisitor(ProgramAnalysis analysis, TextWriter outputStream) : base(analysis)
        {
            Out = outputStream;
            WhileLabelGenerator = new LabelGenerator("While");
            ElseLabelGenerator = new LabelGenerator("Else");
            BooleanExpressionLabelGenerator = new LabelGenerator("BooleanExpression");
        }

        #region Visitor Members

        public override void Visit(ProgramNode node)
        {
            GenText(".code");
            node.mainClassDecl.Accept(this);
            for (int x = 0; x < node.classDeclList.classDeclList.Count; x++)
                node.classDeclList.ClassDeclAtIndex(x).Accept(this);
        }

        #region Declarations

        public override void Visit(MainClassDeclNode node)
        {
            GenText("asm_main:");
            ClassBeingVisited = Analysis.Environment.Classes.Lookup(node.classIdentifier.name);
            MethodBeingVisited = ClassBeingVisited.Methods.Lookup("main");
            node.classIdentifier.Accept(this);
            node.mainParameterIdentifier.Accept(this);
            node.statement.Accept(this);
            Gen("ret", "", "", "Retrun from asm_main");
        }

        public override void Visit(MethodDeclNode node)
        {
            MethodBeingVisited = ClassBeingVisited.Methods.Lookup(node.methodName.name);
            GenText(ClassBeingVisited.Name + "$" + node.methodName.name + ":");
            Gen("push", "ebp", "", "Save caller's stack frame pointer");
            Gen("mov", "ebp", "esp");
            if(MethodBeingVisited.SizeOfLocalVariablesInBytes > 0)
                Gen("sub", "esp", MethodBeingVisited.SizeOfLocalVariablesInBytes.ToString(), "Allocate new stack frame");
            if (node.paramDeclList != null)
                for (int x = 0; x < node.paramDeclList.paramDeclList.Count; x++)
                    node.paramDeclList.ParamDeclAtIndex(x).Accept(this);

            if (node.variableDeclList != null)
                for (int x = 0; x < node.variableDeclList.variableDeclList.Count; x++)
                    node.variableDeclList.VariableDeclAtIndex(x).Accept(this);

            if (node.statementList != null)
                for (int x = 0; x < node.statementList.statementList.Count; x++)
                    node.statementList.StatementAtIndex(x).Accept(this);
            if (MethodBeingVisited.SizeOfLocalVariablesInBytes > 0)
                Gen("mov", "esp", "ebp", "Deallocate local variables from stack");
            Gen("pop", "ebp", "", "Restore ebp to caller's stack frame pointer");
            Gen("ret", "", "", "Retrun from " + ClassBeingVisited.Name + "$" + node.methodName.name);
        }

        #endregion

        #region Expression

        public override void Visit(IntegerConstantExpressionNode node)
        {
            Gen("mov", "eax", node.value.ToString());
        }

        public override void Visit(BooleanConstantExpressionNode node)
        {
            if (node.value == true)
                Gen("mov", "eax", "1");
            else
                Gen("mov", "eax", "0");
        }

        public override void Visit(AddExpressionNode node)
        {
            node.expression1.Accept(this);
            Gen("push", "eax");
            node.expression2.Accept(this);
            Gen("pop", "edx");
            Gen("add", "eax", "edx");
        }

        public override void Visit(SubtractExpressionNode node)
        {
            node.expression1.Accept(this);
            Gen("push", "eax");
            node.expression2.Accept(this);
            Gen("pop", "edx");
            Gen("sub", "edx", "eax");
            Gen("mov", "eax", "edx");
        }

        public override void Visit(MultiplyExpressionNode node)
        {
            node.expression1.Accept(this);
            Gen("push", "eax");
            node.expression2.Accept(this);
            Gen("pop", "edx");
            Gen("imul", "eax", "edx");
        }

        public override void Visit(NewIntegerArrayExpressionNode node)
        {
            node.expression.Accept(this);
            Gen("push", "eax");
            Gen("push", "ecx");
            Gen("shl", "eax", "2", "Determining bytes needed for array of integers");
            Gen("add", "eax", "4", "Add 4 bytes to store Array length at location 0");
            Gen("push", "eax");
            Gen("call", "memalloc", "", "Allocating memory for array of integers");
            Gen("add", "esp", "4");
            Gen("pop", "ecx");
            Gen("pop", "edx", "", "Recover number of array elements int eax");
            Gen("mov", "[eax]", "edx", "Store number of array elements into start of array memory");
        }

        public override void Visit(NewObjectExpressionNode node)
        {
            ClassDefinition newClassDefinition = Analysis.Environment.Classes.Lookup(node.identifier.name);
            Gen("push", "ecx");
            Gen("push", (newClassDefinition.SizeInBytes + 4).ToString(), "", "Instantiating class " + newClassDefinition.Name);
            Gen("call", "memalloc");
            Gen("add", "esp", "4");
            Gen("pop", "ecx");
            Gen("lea", "edx", newClassDefinition.Name + "$$");
            Gen("mov", "[eax]", "edx");
        }

        public override void Visit(IdentifierExpressionNode node)
        {
            GetValueOfIdentifier(node.identifier.name, "eax");
        }

        public override void Visit(ArrayLookupExpressionNode node)
        {
            GetValueOfIdentifier(((IdentifierExpressionNode)node.arrayExpression).identifier.name, "edx");
            Gen("push", "edx");
            node.indexExpression.Accept(this);
            Gen("pop", "edx");
            Gen("mov", "eax", "[edx+4*eax+4]");
        }

        public override void Visit(LengthExpressionNode node)
        {
            node.expression.Accept(this);
            GetValueOfIdentifier(((IdentifierExpressionNode)node.expression).identifier.name, "eax");
            Gen("mov", "eax", "[eax]");
        }

        public override void Visit(MethodCallExpressionNode node)
        {
            Gen("push", "ecx", "", "Save 'this' pointer for current function");
            if (node.expressionList != null)
                for (int x = node.expressionList.expressionList.Count - 1; x >= 0; x--)
                {
                    node.expressionList.ExpressionAtIndex(x).Accept(this);
                    Gen("push", "eax", "", "Pushing Parameter " + (x + 1).ToString());
                }
            node.expression.Accept(this);
            Gen("mov", "ecx", "eax");
            Gen("mov", "eax", "[ecx]");
            ClassDefinition classDefinition = Analysis.Environment.Classes.Lookup(node.expression.ExpressionType.Name);
            MethodDefinition methodDefinition = Analysis.Environment.LookupMethodInClass(node.identifier.name, classDefinition);
            Gen("call", "dword ptr [eax+" + methodDefinition.Location.ToString() + "]", "", "Calling method " + classDefinition.Name + "." + methodDefinition.Name + "()");
            if (node.expressionList != null)
                if (node.expressionList.expressionList.Count > 0)
                    Gen("add", "esp", methodDefinition.SizeOfParametersInBytes.ToString());
            Gen("pop", "ecx", "", "Recover 'this' pointer for current function");
        }

        public override void Visit(FieldAccessExpressionNode node)
        {
            node.expression.Accept(this);
            ClassDefinition classDefinition = Analysis.Environment.Classes.Lookup(node.expression.ExpressionType.Name);
            FieldDefinition fieldDefinition = Analysis.Environment.LookupFieldInClass(node.identifier.name, classDefinition);
            Gen("mov", "eax", "[eax+" + fieldDefinition.Location.ToString() + "]", "Accessing field " + classDefinition.Name + "." + fieldDefinition.Name + "()");
        }

        public override void Visit(ThisExpressionNode node)
        {
            Gen("mov", "eax", "ecx");
        }

        public override void Visit(NotExpressionNode node)
        {
            node.expression.Accept(this);
            Gen("xor", "eax", "1");
        }

        public override void Visit(AndExpressionNode node)
        {
            string BooleanExpressionLabel = BooleanExpressionLabelGenerator.GetNewLabel();
            string BooleanExpressionIsFalseLabel = BooleanExpressionLabel + "IsFalse";
            string BooleanExpressionStoreLabel = BooleanExpressionLabel + "Store";
            node.expression1.Accept(this);
            Gen("cmp", "eax","0");
            Gen("je", BooleanExpressionIsFalseLabel);
            node.expression2.Accept(this);
            Gen("cmp", "eax", "0");
            Gen("je", BooleanExpressionIsFalseLabel);
            Gen("mov", "eax", "1");
            Gen("jmp", BooleanExpressionStoreLabel);
            GenText(BooleanExpressionIsFalseLabel + ":");
            Gen("mov", "eax", "0");
            GenText(BooleanExpressionStoreLabel + ":");
        }

        public override void Visit(LessThanExpressionNode node)
        {
            string BooleanExpressionLabel = BooleanExpressionLabelGenerator.GetNewLabel();
            string BooleanExpressionIsTrueLabel = BooleanExpressionLabel + "IsTrue";
            string BooleanExpressionStoreLabel = BooleanExpressionLabel + "Store";
            node.expression1.Accept(this);
            Gen("push", "eax");
            node.expression2.Accept(this);
            Gen("pop", "edx");
            Gen("cmp", "edx", "eax");
            Gen("jl", BooleanExpressionIsTrueLabel);
            Gen("mov", "eax", "0");
            Gen("jmp", BooleanExpressionStoreLabel);
            GenText(BooleanExpressionIsTrueLabel + ":");
            Gen("mov", "eax", "1");
            GenText(BooleanExpressionStoreLabel + ":");
        }

        #endregion

        #region Statement

        public override void Visit(SystemOutPrintLnStatementNode node)
        {
            node.expression.Accept(this);
            Gen("push", "ecx");
            Gen("push", "eax");
            Gen("call", "put", "", "Calling System.Out.Println()");
            Gen("add", "sp","4");
            Gen("pop", "ecx");
        }

        public override void Visit(ReturnStatementNode node)
        {
            node.expression.Accept(this);
        }

        public override void Visit(AssignmentStatementNode node)
        {
            node.expression.Accept(this);
            if (MethodBeingVisited.LocalVariables.Contains(node.identifier.name))
            {
                VariableDefinition LocalVariableDefinition = MethodBeingVisited.LocalVariables.Lookup(node.identifier.name);
                Gen("mov", "[ebp-" + LocalVariableDefinition.Location.ToString() + "]", "eax", "Assigning eax into variable " + LocalVariableDefinition.Name);
            }
            else if (MethodBeingVisited.Parameters.Contains(node.identifier.name))
            {
                ParameterDefinition parameterDefinition = MethodBeingVisited.Parameters.Lookup(node.identifier.name);
                Gen("mov", "[ebp+" + parameterDefinition.Location.ToString() + "]", "eax", "Assigning eax into parameter " + parameterDefinition.Name);
            }
            else
            {
                FieldDefinition fieldDefinition = Analysis.Environment.LookupFieldInClass(node.identifier.name, ClassBeingVisited);
                Gen("mov", "[ecx+" + fieldDefinition.Location.ToString() + "]", "eax", "Assigning eax into field " + fieldDefinition.Name);
            }
        }

        public override void Visit(ArrayAssignmentStatementNode node)
        {
            GetValueOfIdentifier(node.identifier.name, "edx");
            Gen("push", "edx");
            node.indexExpression.Accept(this);
            Gen("pop", "edx");
            Gen("lea", "edx", "[edx+4*eax+4]");
            Gen("push", "edx");
            node.assignExpression.Accept(this);
            Gen("pop", "edx");
            Gen("mov", "[edx]", "eax");
        }

        public override void Visit(FieldAssignmentStatementNode node)
        {
            node.classIdentifier.Accept(this);
            FieldDefinition fieldDefinitionOfClass = Analysis.Environment.LookupFieldInClass(node.classIdentifier.name, ClassBeingVisited);
            ClassDefinition classDefinition = Analysis.Environment.Classes.Lookup(fieldDefinitionOfClass.FieldType.Name);
            FieldDefinition fieldDefinition = Analysis.Environment.LookupFieldInClass(node.fieldIdentifier.name, classDefinition);
            Gen("lea", "eax", "[eax+" + fieldDefinition.Location.ToString() + "]", "Accessing field " + classDefinition.Name + "." + fieldDefinition.Name + "()");
            Gen("push", "eax");
            node.assignExpression.Accept(this);
            Gen("pop", "edx");
            Gen("mov", "[edx]", "eax");
        }

        public override void Visit(WhileStatementNode node)
        {
            string WhileLabel = WhileLabelGenerator.GetNewLabel();
            string WhileLabelLoop = WhileLabel + "Loop";
            string WhileLabelTest = WhileLabel + "Test";
            Gen("jmp", WhileLabelTest);
            GenText(WhileLabelLoop + ":");
            node.statement.Accept(this);
            GenText(WhileLabelTest + ":");
            node.expression.Accept(this);
            Gen("cmp", "eax", "0");
            Gen("jg", WhileLabelLoop);
        }

        public override void Visit(IfStatementNode node)
        {
            string ElseLabel = ElseLabelGenerator.GetNewLabel();
            string ElseEndLabel = ElseLabel + "End";
            node.expression.Accept(this);
            Gen("cmp", "eax", "0");
            Gen("je", ElseLabel);
            node.thenStatement.Accept(this);
            Gen("jmp", ElseEndLabel);
            GenText(ElseLabel + ":");
            node.elseStatement.Accept(this);
            GenText(ElseEndLabel + ":");
        }

        #endregion

        #endregion

        private void GetValueOfIdentifier(string identifierName, string registerName)
        {
            if (MethodBeingVisited.LocalVariables.Contains(identifierName))
            {
                VariableDefinition LocalVariableDefinition = MethodBeingVisited.LocalVariables.Lookup(identifierName);
                Gen("mov", registerName, "[ebp-" + LocalVariableDefinition.Location.ToString() + "]", "Assigning address of variable " + LocalVariableDefinition.Name + " into " + registerName);
            }
            else if (MethodBeingVisited.Parameters.Contains(identifierName))
            {
                ParameterDefinition parameterDefinition = MethodBeingVisited.Parameters.Lookup(identifierName);
                Gen("mov", registerName, "[ebp+" + parameterDefinition.Location.ToString() + "]", "Assigning address of parameter " + parameterDefinition.Name + " into " + registerName);
            }
            else
            {
                FieldDefinition fieldDefinition = Analysis.Environment.LookupFieldInClass(identifierName, ClassBeingVisited);
                Gen("mov", registerName, "[ecx+" + fieldDefinition.Location.ToString() + "]", "Assigning address of field " + fieldDefinition.Name + " into " + registerName);
            }
        }
    }
}
