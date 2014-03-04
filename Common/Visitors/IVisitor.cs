using MiniJava.AST;

namespace MiniJava.Visitors
{
    public interface IVisitor<R>
    {
        R Visit(AddExpressionNode node);
        R Visit(AndExpressionNode node);
        R Visit(ArrayAssignmentStatementNode node);
        R Visit(FieldAssignmentStatementNode node);
        R Visit(ArrayLookupExpressionNode node);
        R Visit(AssignmentStatementNode node);
        R Visit(BooleanConstantExpressionNode node);
        R Visit(BooleanTypeNode node);
        R Visit(ClassDeclNode node);
        R Visit(ExtendsNode node);
        R Visit(IdentifierExpressionNode node);
        R Visit(IdentifierNode node);
        R Visit(IdentifierTypeNode node);
        R Visit(IfStatementNode node);
        R Visit(IntegerArrayTypeNode node);
        R Visit(IntegerConstantExpressionNode node);
        R Visit(IntegerTypeNode node);
        R Visit(LengthExpressionNode node);
        R Visit(LessThanExpressionNode node);
        R Visit(MainClassDeclNode node);
        R Visit(MethodCallExpressionNode node);
        R Visit(FieldAccessExpressionNode node);
        R Visit(MethodDeclNode node);
        R Visit(MultiplyExpressionNode node);
        R Visit(NewIntegerArrayExpressionNode node);
        R Visit(NewObjectExpressionNode node);
        R Visit(NotExpressionNode node);
        R Visit(ParamDeclNode node);
        R Visit(ProgramNode node);
        R Visit(StatementBlockNode node);
        R Visit(SubtractExpressionNode node);
        R Visit(SystemOutPrintLnStatementNode node);
        R Visit(ThisExpressionNode node);
        R Visit(VariableDeclNode node);
        R Visit(WhileStatementNode node);
        R Visit(ReturnStatementNode node);
        R Visit(InvalidExpressionNode node);
    }
}
