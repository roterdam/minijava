using System;
using System.Collections.Generic;
using System.Text;
using MiniJava.AST;
using MiniJava.Visitors;
using MiniJava.Types;
using MiniJava.Definitions;
using MiniJava;

namespace MiniJava.Frontend.Visitors
{
    internal class TypeCheckerVisitor : BaseVisitor
    {
        public TypeCheckerVisitor(ProgramAnalysis analysis) : base(analysis)
        {
        }

        private BaseType LookupSymbolType(string name)
        {
            if (MethodBeingVisited.LocalVariables.Contains(name))
                return MethodBeingVisited.LocalVariables.Lookup(name).VariableType;
            else
            {
                try
                {
                    if (MethodBeingVisited.Parameters.Contains(name))
                        return MethodBeingVisited.Parameters.Lookup(name).ParameterType;
                    else
                        return Analysis.Environment.LookupFieldInClass(name, ClassBeingVisited).FieldType;
                }
                catch (Exception)
                {
                    throw new Exception("No valid Symbol Table Entry found for '" + name + "'!");
                }
            }
        }

        private bool AreTypeCompatible(Type type1, Type type2)
        {
            return (type1 == type2 || type1 == typeof(InvalidType) || type2 == typeof(InvalidType));
        }

        private bool IsClassCompatible(ClassType actualType, ClassType expectedType)
        {
            if (actualType.Name == expectedType.Name)
                return true;
            if (actualType.BaseClassType == null)
                return false;
            else
                return IsClassCompatible(actualType.BaseClassType, expectedType);
        }

        #region Visitor Members

        public override void Visit(IdentifierExpressionNode node)
        {
            node.ExpressionType = LookupSymbolType(node.identifier.name);
        }

        public override void Visit(BooleanConstantExpressionNode node)
        {
            node.ExpressionType = BooleanType.Instance;
        }

        public override void Visit(IntegerConstantExpressionNode node)
        {
            node.ExpressionType = IntType.Instance;
        }

        public override void Visit(ArrayAssignmentStatementNode node)
        {
            try
            {
                if (!AreTypeCompatible(LookupSymbolType(node.identifier.name).GetType(), typeof(ArrayType)))
                    throw new Exception("Array Identifier '" + node.identifier.name + "' is not an array!");

                node.identifier.Accept(this);
                node.indexExpression.Accept(this);
                node.assignExpression.Accept(this);

                if (!AreTypeCompatible(node.indexExpression.ExpressionType.GetType(), typeof(IntType)))
                    throw new Exception("Index Expression's Type is not Int!");
                if (!AreTypeCompatible(node.assignExpression.ExpressionType.GetType(), typeof(IntType)))
                    throw new Exception("Assigned Expression's Type is not Int!");
            }
            catch (Exception e)
            {
                Analysis.LogSemanticError(e.Message, node.lineNumber);
            }
        }

        public override void Visit(FieldAssignmentStatementNode node)
        {
            try
            {
                node.classIdentifier.Accept(this);
                if (!AreTypeCompatible(LookupSymbolType(node.classIdentifier.name).GetType(), typeof(ClassType)))
                    throw new Exception("Object used for field access is not an instance of any Class");

                FieldDefinition fieldDefinitionOfClass = Analysis.Environment.LookupFieldInClass(node.classIdentifier.name, ClassBeingVisited);
                ClassDefinition classDefinition = Analysis.Environment.Classes.Lookup(fieldDefinitionOfClass.FieldType.Name);
                FieldDefinition fieldDefinition;

                try
                {
                    fieldDefinition = Analysis.Environment.LookupFieldInClass(node.fieldIdentifier.name, classDefinition);
                }
                catch (Exception)
                {
                    throw new Exception("Field '" + node.fieldIdentifier.name + "' not found in class '" + node.classIdentifier.name + "'");
                }


                node.assignExpression.Accept(this);

                if (!AreTypeCompatible(node.assignExpression.ExpressionType.GetType(), fieldDefinition.FieldType.GetType()))
                    throw new Exception("Uncompatible types in assignment statement");
            }
            catch (Exception e)
            {
                Analysis.LogSemanticError(e.Message, node.lineNumber);
            }
        }

        public override void Visit(ArrayLookupExpressionNode node)
        {
            if (!AreTypeCompatible(node.arrayExpression.GetType(), typeof(IdentifierExpressionNode)))
                throw new Exception("Array Lookup Expression must be an Identifier!");

            node.arrayExpression.Accept(this);
            node.indexExpression.Accept(this);

            if (!AreTypeCompatible(node.arrayExpression.ExpressionType.GetType(), typeof(ArrayType)))
                throw new Exception("Array Identifier '" + ((IdentifierExpressionNode)node.arrayExpression).identifier.name + "' is not an array!");
            if (!AreTypeCompatible(node.indexExpression.ExpressionType.GetType(), typeof(IntType)))
                throw new Exception("Index Expression's Type is not Int!");

            node.ExpressionType = IntType.Instance;
        }

        public override void Visit(AssignmentStatementNode node)
        {
            try
            {
                node.expression.Accept(this);

                if (!AreTypeCompatible(LookupSymbolType(node.identifier.name).GetType(), node.expression.ExpressionType.GetType()) ||
                (LookupSymbolType(node.identifier.name).GetType() == typeof(ClassType) && !IsClassCompatible((ClassType)LookupSymbolType(node.identifier.name), (ClassType)node.expression.ExpressionType)))
                    throw new Exception("Type Mismatch in Assignment Statement!");
            }
            catch (Exception e)
            {
                Analysis.LogSemanticError(e.Message, node.lineNumber);
            }
        }

        public override void Visit(NewIntegerArrayExpressionNode node)
        {
            node.expression.Accept(this);

            if (!AreTypeCompatible(node.expression.ExpressionType.GetType(), typeof(IntType)))
                throw new Exception("Integer Array Size Expression's Type is not Int!");

            ArrayType nodeType = new ArrayType("int[]");
            nodeType.ElementType = IntType.Instance;
            nodeType.NoOfDimensions = 1;
            node.ExpressionType = nodeType;
        }

        public override void Visit(NewObjectExpressionNode node)
        {
            ClassDefinition definiton = Analysis.Environment.Classes.Lookup(node.identifier.name);

            if (definiton == null)
                throw new Exception("No class definition exists for '" + node.identifier.name + "'!");

            node.ExpressionType = definiton.ClassType;
        }

        public override void Visit(ThisExpressionNode node)
        {
            node.ExpressionType = ClassBeingVisited.ClassType;
        }

        public override void Visit(InvalidExpressionNode node)
        {
            node.ExpressionType = InvalidType.Instance;
        }

        public override void Visit(AddExpressionNode node)
        {
            node.expression1.Accept(this);
            node.expression2.Accept(this);

            if (!AreTypeCompatible(node.expression1.ExpressionType.GetType(), typeof(IntType)))
                throw new Exception("Expression 1 for + operation is not of type Int!");

            if (!AreTypeCompatible(node.expression2.ExpressionType.GetType(), typeof(IntType)))
                throw new Exception("Expression 2 for + operation is not of type Int!");

            node.ExpressionType = IntType.Instance;
        }

        public override void Visit(AndExpressionNode node)
        {
            node.expression1.Accept(this);
            node.expression2.Accept(this);

            if (!AreTypeCompatible(node.expression1.ExpressionType.GetType(), typeof(BooleanType)))
                throw new Exception("Expression 1 for && operation is not of type Boolean!");

            if (!AreTypeCompatible(node.expression2.ExpressionType.GetType(), typeof(BooleanType)))
                throw new Exception("Expression 2 for && operation is not of type Boolean!");

            node.ExpressionType = BooleanType.Instance;
        }

        public override void Visit(IfStatementNode node)
        {
            try
            {
                node.expression.Accept(this);

                if (!AreTypeCompatible(node.expression.ExpressionType.GetType(), typeof(BooleanType)))
                    throw new Exception("If condition expression is not of type Boolean!");
            }
            catch (Exception e)
            {
                Analysis.LogSemanticError(e.Message, node.lineNumber);
            }
            node.thenStatement.Accept(this);
            node.elseStatement.Accept(this);
        }

        public override void Visit(LengthExpressionNode node)
        {
            node.expression.Accept(this);

            if (!AreTypeCompatible(node.expression.ExpressionType.GetType(), typeof(ArrayType)))
                throw new Exception("Array Identifier '" + ((IdentifierExpressionNode)node.expression).identifier.name + "' is not an array!");

            node.ExpressionType = IntType.Instance;
        }

        public override void Visit(LessThanExpressionNode node)
        {
            node.expression1.Accept(this);
            node.expression2.Accept(this);

            if (!AreTypeCompatible(node.expression1.ExpressionType.GetType(), typeof(IntType)))
                throw new Exception("Expression 1 for < operation is not of type Int!");

            if (!AreTypeCompatible(node.expression2.ExpressionType.GetType(), typeof(IntType)))
                throw new Exception("Expression 2 for < operation is not of type Int!");

            node.ExpressionType = BooleanType.Instance;
        }

        public override void Visit(MethodCallExpressionNode node)
        {
            node.expression.Accept(this);

            if (node.expression.ExpressionType.GetType() != typeof(ClassType))
                throw new Exception("Object used for method call is not an instance of any Class");

            ClassDefinition classDefinition = Analysis.Environment.Classes.Lookup(node.expression.ExpressionType.Name);
            MethodDefinition methodDefinition;

            try
            {
                methodDefinition = Analysis.Environment.LookupMethodInClass(node.identifier.name, classDefinition);
            }
            catch (Exception)
            {
                throw new Exception("No definition exists for method '" + node.identifier.name + "' in class '" + node.expression.ExpressionType.Name + "'!");
            }
            if(!(node.expressionList == null && methodDefinition.Parameters.Count == 0))
            {
                if (node.expressionList != null && node.expressionList.expressionList.Count == methodDefinition.Parameters.Count)
                {
                    for (int x = 0; x < methodDefinition.Parameters.Count; x++)
                    {
                        node.expressionList.ExpressionAtIndex(x).Accept(this);
                        if(node.expressionList.ExpressionAtIndex(x).ExpressionType.GetType() == typeof(ClassType))
                        {
                            if(!IsClassCompatible((ClassType)node.expressionList.ExpressionAtIndex(x).ExpressionType, (ClassType)methodDefinition.Parameters.ItemAt(x).ParameterType))
                                throw new Exception("Type mismatch in method call at parameter " + x);
                        }
                        else
                            if(!AreTypeCompatible(node.expressionList.ExpressionAtIndex(x).ExpressionType.GetType(), methodDefinition.Parameters.ItemAt(x).ParameterType.GetType()))
                                throw new Exception("Type mismatch in method call at parameter " + x);
                    }
                }
                else
                    throw new Exception("Method '" + methodDefinition.Name + "' takes " + methodDefinition.Parameters.Count + " Paramater(s)");
            }

            node.ExpressionType = methodDefinition.ReturnType;
        }

        public override void Visit(FieldAccessExpressionNode node)
        {
            node.expression.Accept(this);

            if (node.expression.ExpressionType.GetType() != typeof(ClassType))
                throw new Exception("Object used for field access is not an instance of any Class");

            ClassDefinition classDefinition = Analysis.Environment.Classes.Lookup(node.expression.ExpressionType.Name);
            FieldDefinition fieldDefinition;

            try
            {
                fieldDefinition = Analysis.Environment.LookupFieldInClass(node.identifier.name, classDefinition);
            }
            catch (Exception)
            {
                throw new Exception("Field '" + node.identifier.name + "' not found in class '" + node.expression.ExpressionType.Name + "'");
            }

            node.ExpressionType = fieldDefinition.FieldType;
        }

        public override void Visit(MultiplyExpressionNode node)
        {
            node.expression1.Accept(this);
            node.expression2.Accept(this);

            if (!AreTypeCompatible(node.expression1.ExpressionType.GetType(), typeof(IntType)))
                throw new Exception("Expression 1 for * operation is not of type Int!");

            if (!AreTypeCompatible(node.expression2.ExpressionType.GetType(), typeof(IntType)))
                throw new Exception("Expression 2 for * operation is not of type Int!");

            node.ExpressionType = IntType.Instance;
        }

        public override void Visit(NotExpressionNode node)
        {
            node.expression.Accept(this);

            if (!AreTypeCompatible(node.expression.ExpressionType.GetType(), typeof(BooleanType)))
                throw new Exception("Expression for ! operation is not of type Boolean!");

            node.ExpressionType = BooleanType.Instance;
        }

        public override void Visit(ReturnStatementNode node)
        {
            try
            {
                node.expression.Accept(this);

                if(node.expression.ExpressionType.GetType() == typeof(ClassType) && !IsClassCompatible((ClassType)node.expression.ExpressionType, (ClassType)MethodBeingVisited.ReturnType))
                {
                    throw new Exception("Return Expression should have compatible Type as in the Method Declaration!");
                }
                else
                    if (!AreTypeCompatible(node.expression.ExpressionType.GetType(), MethodBeingVisited.ReturnType.GetType()))
                        throw new Exception("Return Expression should have compatible Type as in the Method Declaration!");
            }
            catch (Exception e)
            {
                Analysis.LogSemanticError(e.Message, node.lineNumber);
            }
        }

        public override void Visit(SubtractExpressionNode node)
        {
            node.expression1.Accept(this);
            node.expression2.Accept(this);

            if (!AreTypeCompatible(node.expression1.ExpressionType.GetType(), typeof(IntType)))
                throw new Exception("Expression 1 for - operation is not of type Int!");

            if (!AreTypeCompatible(node.expression2.ExpressionType.GetType(), typeof(IntType)))
                throw new Exception("Expression 2 for - operation is not of type Int!");

            node.ExpressionType = IntType.Instance;
        }

        public override void Visit(SystemOutPrintLnStatementNode node)
        {
            try
            {
                node.expression.Accept(this);

                if (!AreTypeCompatible(node.expression.ExpressionType.GetType(), typeof(IntType)))
                    throw new Exception("Expression for System.out.println is not of type Int!");
            }
            catch (Exception e)
            {
                Analysis.LogSemanticError(e.Message, node.lineNumber);
            }
        }

        public override void Visit(WhileStatementNode node)
        {
            try
            {
                node.expression.Accept(this);

                if (!AreTypeCompatible(node.expression.ExpressionType.GetType(), typeof(BooleanType)))
                    throw new Exception("While condition expression is not of type Boolean!");
            }
            catch (Exception e)
            {
                Analysis.LogSemanticError(e.Message, node.lineNumber);
            }
            node.statement.Accept(this);
        }

        #endregion
    }
}
