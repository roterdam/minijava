using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.AST;
using MiniJava.Visitors;
using MiniJava.Definitions;
using MiniJava.Types;

namespace MiniJava.Frontend.Visitors
{
    internal class SymbolDefinitionsExtractorVisitor : BaseVisitor
    {
        public SymbolDefinitionsExtractorVisitor(ProgramAnalysis analysis)
            : base(analysis)
        {
        }

        #region Visitor Members

        public override void Visit(MainClassDeclNode node)
        {
            ClassBeingVisited = Analysis.Environment.Classes.Lookup(node.classIdentifier.name);
            MethodBeingVisited = ClassBeingVisited.Methods.Add("main");
            ParameterDefinition parameterDefinition = MethodBeingVisited.Parameters.Add("args");
            parameterDefinition.ParameterType = Analysis.Environment.Classes.Lookup("String").ClassType;
        }

        public override void Visit(ClassDeclNode node)
        {
            ClassBeingVisited = Analysis.Environment.Classes.Lookup(node.className.name);

            if (node.variableDeclList != null)
                for (int x = 0; x < node.variableDeclList.variableDeclList.Count; x++)
                {
                    FieldDefinition fieldDefinition = ClassBeingVisited.Fields.Add(node.variableDeclList.VariableDeclAtIndex(x).identifier.name);
                    fieldDefinition.FieldType = node.variableDeclList.VariableDeclAtIndex(x).type.Type;
                    if (fieldDefinition.FieldType.GetType() == typeof(ClassType))
                        Analysis.Environment.Classes.Lookup(fieldDefinition.FieldType.Name);
                }
            if (node.methodDeclList != null)
                for (int x = 0; x < node.methodDeclList.methodDeclList.Count; x++)
                    node.methodDeclList.MethodDeclAtIndex(x).Accept(this);
        }

        public override void Visit(VariableDeclNode node)
        {
            node.identifier.Accept(this);
            node.type.Accept(this);
        }

        public override void Visit(MethodDeclNode node)
        {
            MethodBeingVisited = ClassBeingVisited.Methods.Add(node.methodName.name);
            MethodBeingVisited.ClassDefinition = ClassBeingVisited;

            MethodBeingVisited.ReturnType = node.methodType.Type;

            if (node.paramDeclList != null)
                for (int x = 0; x < node.paramDeclList.paramDeclList.Count; x++)
                    node.paramDeclList.ParamDeclAtIndex(x).Accept(this);

            if (node.variableDeclList != null)
                for (int x = 0; x < node.variableDeclList.variableDeclList.Count; x++)
                {
                    VariableDefinition variableDefinition = MethodBeingVisited.LocalVariables.Add(node.variableDeclList.VariableDeclAtIndex(x).identifier.name);
                    variableDefinition.VariableType = node.variableDeclList.VariableDeclAtIndex(x).type.Type;
                    if (variableDefinition.VariableType.GetType() == typeof(ClassType))
                        variableDefinition.VariableType = Analysis.Environment.Classes.Lookup(variableDefinition.VariableType.Name).ClassType;
                }
        }

        public override void Visit(ParamDeclNode node)
        {
            ParameterDefinition parameterDefinition = MethodBeingVisited.Parameters.Add(node.identifier.name);
            parameterDefinition.ParameterType = node.type.Type;
        }

        #endregion
    }
}
