using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.AST;
using MiniJava.Definitions;
using MiniJava.Visitors;

namespace MiniJava.Backend.Visitors
{
    internal class SymbolPreprocessorVisitor : BaseVisitor
    {
        public SymbolPreprocessorVisitor(ProgramAnalysis analysis) : base(analysis)
        {
            this.Analysis = analysis;
        }

        #region Visitor Members

        public override void Visit(ProgramNode node)
        {
            node.mainClassDecl.Accept(this);
            for (int x = 0; x < node.classDeclList.classDeclList.Count; x++)
                node.classDeclList.ClassDeclAtIndex(x).Accept(this);
        }

        #region Declarations

        public override void Visit(MainClassDeclNode node)
        {
            ClassBeingVisited = Analysis.Environment.Classes.Lookup(node.classIdentifier.name);
            ClassBeingVisited.SizeInBytes = 0;
        }

        public override void Visit(ClassDeclNode node)
        {
            ClassBeingVisited = Analysis.Environment.Classes.Lookup(node.className.name);
            ClassBeingVisited.SizeInBytes = SizeOf(ClassBeingVisited);
            int Offset = 4;
            if(ClassBeingVisited.ClassType.BaseClassType != null)
                Offset += SizeOf(Analysis.Environment.Classes.Lookup(ClassBeingVisited.ClassType.BaseClassType.Name));
            for (int x = 0; x < ClassBeingVisited.Fields.Count; x++)
            {
                FieldDefinition field = ClassBeingVisited.Fields.ItemAt(x);
                field.SizeInBytes = 4;
                Offset += field.SizeInBytes;
                field.Location = Offset;
            }

            if (node.methodDeclList != null)
                for (int x = 0; x < node.methodDeclList.methodDeclList.Count; x++)
                    node.methodDeclList.MethodDeclAtIndex(x).Accept(this);
        }

        public override void Visit(ExtendsNode node)
        {
        }

        public override void Visit(VariableDeclNode node)
        {
            node.type.Accept(this);
            node.identifier.Accept(this);
        }

        public override void Visit(MethodDeclNode node)
        {
            MethodBeingVisited = ClassBeingVisited.Methods.Lookup(node.methodName.name);

            int Offset = 4; // Initialize to 4 because we need to reserve 4 bytes for return address

            for (int x = 0; x < MethodBeingVisited.Parameters.Count; x++)
            {
                ParameterDefinition parameter = MethodBeingVisited.Parameters.ItemAt(x);
                parameter.SizeInBytes = 4;
                Offset += parameter.SizeInBytes;
                parameter.Location = Offset;
            }

            MethodBeingVisited.SizeOfParametersInBytes = Offset - 4;

            Offset = 0;
            for (int x = 0; x < MethodBeingVisited.LocalVariables.Count; x++)
            {
                VariableDefinition variable = MethodBeingVisited.LocalVariables.ItemAt(x);
                variable.SizeInBytes = 4;
                Offset += variable.SizeInBytes;
                variable.Location = Offset;
            }

            MethodBeingVisited.SizeOfLocalVariablesInBytes = Offset;
        }

        public override void Visit(ParamDeclNode node)
        {
            node.type.Accept(this);
            node.identifier.Accept(this);
        }

        #endregion

        #endregion

        private int SizeOf(Definitions.ClassDefinition classDefinition)
        {
            int Size = classDefinition.Fields.Count * 4;
            if (classDefinition.ClassType.BaseClassType != null)
                Size += SizeOf(Analysis.Environment.Classes.Lookup(classDefinition.ClassType.BaseClassType.Name));
            return Size;
        }
    }
}
