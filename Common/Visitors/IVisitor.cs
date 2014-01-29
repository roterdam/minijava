using MiniJava.AST;

namespace MiniJava.Visitors
{
    public interface IVisitor
    {
        void Visit(AddExpressionNode node);
        void Visit(AndExpressionNode node);
        void Visit(ArrayAssignmentStatementNode node);
        void Visit(FieldAssignmentStatementNode node);
        void Visit(ArrayLookupExpressionNode node);
        void Visit(AssignmentStatementNode node);
        void Visit(BooleanConstantExpressionNode node);
        void Visit(BooleanTypeNode node);
        void Visit(ClassDeclNode node);
        void Visit(ExtendsNode node);
        void Visit(IdentifierExpressionNode node);
        void Visit(IdentifierNode node);
        void Visit(IdentifierTypeNode node);
        void Visit(IfStatementNode node);
        void Visit(IntegerArrayTypeNode node);
        void Visit(IntegerConstantExpressionNode node);
        void Visit(IntegerTypeNode node);
        void Visit(LengthExpressionNode node);
        void Visit(LessThanExpressionNode node);
        void Visit(MainClassDeclNode node);
        void Visit(MethodCallExpressionNode node);
        void Visit(FieldAccessExpressionNode node);
        void Visit(MethodDeclNode node);
        void Visit(MultiplyExpressionNode node);
        void Visit(NewIntegerArrayExpressionNode node);
        void Visit(NewObjectExpressionNode node);
        void Visit(NotExpressionNode node);
        void Visit(ParamDeclNode node);
        void Visit(ProgramNode node);
        void Visit(StatementBlockNode node);
        void Visit(SubtractExpressionNode node);
        void Visit(SystemOutPrintLnStatementNode node);
        void Visit(ThisExpressionNode node);
        void Visit(VariableDeclNode node);
        void Visit(WhileStatementNode node);
        void Visit(ReturnStatementNode node);
        void Visit(InvalidExpressionNode node);
    }
}
