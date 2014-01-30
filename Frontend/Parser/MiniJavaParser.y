%using MiniJava
%using MiniJava.AST

%{
    int[] regs = new int[26];
    public bool ErrorFlag;
    public static int LineNumber = 1;
    public ProgramAnalysis Analysis;
%}

%YYSTYPE BaseASTNode

%start Program

%token Null Identifier IntKeyword 
        BooleanKeyword IfKeyword ElseKeyword WhileKeyword SystemOutPrintLnKeyword TrueKeyword
        ThisKeyword NewKeyword ClassKeyword PublicKeyword StaticKeyword VoidKeyword MainKeyword
        StringKeyword ExtendsKeyword ReturnKeyword LengthKeyword FalseKeyword RoundBracketOpen 
        RoundBracketClose CurlyBracketOpen CurlyBracketClose  
        SemiColon IntegerConstant EOF SquareBracketClose 

%right  EqualsOperator 

%left   Comma
%left   AndAndOperator
%left   LessThanOperator
%left   AddOperator SubtractOperator
%left   MultiplyOperator DivideOperator  
%right  NotOperator
%left   Dot
%right  SquareBracketOpen 

%%

Program     :   MainClassDecl ClassDeclList_Opt { $$ = new ProgramNode((MainClassDeclNode)$1,(ClassDeclListNode)$2, LineNumber); Analysis.AST = (ProgramNode)$$; };

MainClassDecl   :   ClassKeyword Identifier CurlyBracketOpen
                    PublicKeyword StaticKeyword VoidKeyword MainKeyword 
                    RoundBracketOpen StringKeyword SquareBracketOpen SquareBracketClose Identifier RoundBracketClose
                    CurlyBracketOpen Statement CurlyBracketClose
                    CurlyBracketClose { $$ = new MainClassDeclNode((IdentifierNode)$2,(IdentifierNode)$12,(StatementNode)$15,LineNumber); };

ClassDeclList_Opt   :   ClassDeclList_Opt ClassDecl {  ((ClassDeclListNode)$1).AddClassDecl((ClassDeclNode)$2); $$=$1; }
                    |   /* Empty */ { $$ = new ClassDeclListNode(LineNumber); };

ClassDecl   :   ClassKeyword Identifier Extends_Opt CurlyBracketOpen
                VariableDeclList_Opt MethodDeclList_Opt CurlyBracketClose { $$ = new ClassDeclNode((IdentifierNode)$2,(ExtendsNode)$3,(VariableDeclListNode)$5,(MethodDeclListNode)$6,LineNumber); };

Extends_Opt :   ExtendsKeyword Identifier { $$ = new ExtendsNode((IdentifierNode)$2,LineNumber); }
            |   /* Empty */ ;
        
MethodDeclList_Opt  :   MethodDeclList_Opt MethodDecl {  ((MethodDeclListNode)$1).AddMethodDecl((MethodDeclNode)$2); $$=$1; }
                    |   /* Empty */ { $$ = new MethodDeclListNode(LineNumber); };

MethodDecl  :   PublicKeyword Type Identifier RoundBracketOpen ParamDeclList_Opt RoundBracketClose
                CurlyBracketOpen VariableDeclList_Opt StatementListEndingInReturn CurlyBracketClose { ((StatementListNode)$9).statementList.Reverse(); $$ = new MethodDeclNode((TypeNode)$2,(IdentifierNode)$3,(ParamDeclListNode)$5,(VariableDeclListNode)$8,(StatementListNode)$9,LineNumber); };

VariableDeclList_Opt    :   VariableDeclList_Opt VariableDecl {  ((VariableDeclListNode)$1).AddVariableDecl((VariableDeclNode)$2); $$=$1; }
                        |   /* Empty */ { $$ = new VariableDeclListNode(LineNumber); };

VariableDecl        :   Type Identifier SemiColon { $$ = new VariableDeclNode((TypeNode)$1,(IdentifierNode)$2,LineNumber); };
                    /*| error SemiColon { yyclearin(); Analysis.LogSyntaxError("Syntax error", LineNumber); }; */

StatementListEndingInReturn :   Statement StatementListEndingInReturn { ((StatementListNode)$2).AddStatement((StatementNode)$1); $$=$2; }
                            |   ReturnKeyword Expr SemiColon { $$ = new StatementListNode(LineNumber); ((StatementListNode)$$).AddStatement(new ReturnStatementNode((ExpressionNode)$2,LineNumber)); };

ParamDeclList_Opt   :   Type Identifier ParamDeclListRest_Opt { ((ParamDeclListNode)$3).paramDeclList.Reverse(); ((ParamDeclListNode)$3).AddParamDecl(new ParamDeclNode((TypeNode)$1,(IdentifierNode)$2,LineNumber)); ((ParamDeclListNode)$3).paramDeclList.Reverse(); $$=$3; }
                    |   /* Empty */ ;

ParamDeclListRest_Opt   :   ParamDeclListRest_Opt Comma Type Identifier {  ((ParamDeclListNode)$1).AddParamDecl(new ParamDeclNode((TypeNode)$3,(IdentifierNode)$4,LineNumber)); $$=$1; }
                        |   /* Empty */ { $$ = new ParamDeclListNode(LineNumber); };

Type        :   IntKeyword SquareBracketOpen SquareBracketClose { $$ = new IntegerArrayTypeNode(LineNumber); }
            |   IntKeyword { $$ = new IntegerTypeNode(LineNumber); }
            |   BooleanKeyword { $$ = new BooleanTypeNode(LineNumber); }
            |   Identifier { $$ = new IdentifierTypeNode(((IdentifierNode)$1).name,LineNumber); };

Statement   :   CurlyBracketOpen StatementList_Opt CurlyBracketClose { $$ = new StatementBlockNode((StatementListNode)$2,LineNumber); }
            |   IfKeyword RoundBracketOpen Expr RoundBracketClose Statement ElseKeyword Statement { $$ = new IfStatementNode((ExpressionNode)$3,(StatementNode)$5,(StatementNode)$7,LineNumber); }
            |   WhileKeyword RoundBracketOpen Expr RoundBracketClose Statement { $$ = new WhileStatementNode((ExpressionNode)$3,(StatementNode)$5,LineNumber); }
            |   SystemOutPrintLnKeyword RoundBracketOpen Expr RoundBracketClose SemiColon { $$ = new SystemOutPrintLnStatementNode((ExpressionNode)$3,LineNumber); }
            |   Identifier EqualsOperator Expr SemiColon { $$ = new AssignmentStatementNode((IdentifierNode)$1,(ExpressionNode)$3,LineNumber); }
            |   Identifier Dot Identifier EqualsOperator Expr SemiColon { $$ = new FieldAssignmentStatementNode((IdentifierNode)$1,(IdentifierNode)$3,(ExpressionNode)$5,LineNumber); } 
            |   Identifier SquareBracketOpen Expr SquareBracketClose EqualsOperator Expr SemiColon { $$ = new ArrayAssignmentStatementNode((IdentifierNode)$1,(ExpressionNode)$3,(ExpressionNode)$6,LineNumber); } 
            |   error SemiColon { $$ = new StatementBlockNode(new StatementListNode(LineNumber),LineNumber); yyclearin(); Analysis.LogSyntaxError("Syntax error", LineNumber);} 
            |   error CurlyBracketClose { $$ = new StatementBlockNode(new StatementListNode(LineNumber),LineNumber); yyclearin(); Analysis.LogSyntaxError("Syntax error", LineNumber);} ;
    
StatementList_Opt   :   StatementList_Opt Statement {  ((StatementListNode)$1).AddStatement((StatementNode)$2); $$=$1; }
                    |   /* Empty */ { $$ = new StatementListNode(LineNumber); };

Expr    :   RoundBracketOpen Expr RoundBracketClose { $$ = $2; }
        |   NotOperator Expr { $$ = new NotExpressionNode((ExpressionNode)$2,LineNumber); }
        |   NewKeyword IntKeyword SquareBracketOpen Expr SquareBracketClose { $$ = new NewIntegerArrayExpressionNode((ExpressionNode)$4,LineNumber); }
        |   NewKeyword Identifier RoundBracketOpen RoundBracketClose { $$ = new NewObjectExpressionNode((IdentifierNode)$2,LineNumber); }
        |   Expr AndAndOperator Expr { $$ = new AndExpressionNode((ExpressionNode)$1,(ExpressionNode)$3,LineNumber); }
        |   Expr LessThanOperator Expr { $$ = new LessThanExpressionNode((ExpressionNode)$1,(ExpressionNode)$3,LineNumber); }
        |   Expr AddOperator Expr { $$ = new AddExpressionNode((ExpressionNode)$1,(ExpressionNode)$3,LineNumber); }
        |   Expr SubtractOperator Expr { $$ = new SubtractExpressionNode((ExpressionNode)$1,(ExpressionNode)$3,LineNumber); }
        |   Expr MultiplyOperator Expr { $$ = new MultiplyExpressionNode((ExpressionNode)$1,(ExpressionNode)$3,LineNumber); }
        |   Expr SquareBracketOpen Expr SquareBracketClose { $$ = new ArrayLookupExpressionNode((ExpressionNode)$1,(ExpressionNode)$3,LineNumber); }
        |   Expr Dot LengthKeyword { $$ = new LengthExpressionNode((ExpressionNode)$1,LineNumber); }
        |   Expr Dot Identifier { $$ = new FieldAccessExpressionNode((ExpressionNode)$1,(IdentifierNode)$3,LineNumber); }
        |   Expr Dot Identifier RoundBracketOpen ExprList_Opt RoundBracketClose  { $$ = new MethodCallExpressionNode((ExpressionNode)$1,(IdentifierNode)$3,(ExpressionListNode)$5,LineNumber); }
        |   ThisKeyword { $$ = new ThisExpressionNode(LineNumber); }
        |   Identifier { $$ = new IdentifierExpressionNode((IdentifierNode)$1,LineNumber); }
        |   IntegerConstant { $$ = $1; }
        |   TrueKeyword { $$ = $1; }
        |   FalseKeyword { $$ = $1; }
        |   error IntegerConstant {  $$ = new InvalidExpressionNode(LineNumber); yyclearin(); Analysis.LogSyntaxError("Syntax error", LineNumber); }
        |   error TrueKeyword {  $$ = new InvalidExpressionNode(LineNumber); yyclearin(); Analysis.LogSyntaxError("Syntax error", LineNumber); }
        |   error FalseKeyword {  $$ = new InvalidExpressionNode(LineNumber); yyclearin(); Analysis.LogSyntaxError("Syntax error", LineNumber); } 
        |   error Identifier {  $$ = new InvalidExpressionNode(LineNumber); yyclearin(); Analysis.LogSyntaxError("Syntax error", LineNumber); } 
        |   error ThisKeyword {  $$ = new InvalidExpressionNode(LineNumber); yyclearin(); Analysis.LogSyntaxError("Syntax error", LineNumber); } 
        |   error SquareBracketClose {  $$ = new InvalidExpressionNode(LineNumber); yyclearin(); Analysis.LogSyntaxError("Syntax error", LineNumber); } 
        |   error RoundBracketClose {  $$ = new InvalidExpressionNode(LineNumber); yyclearin(); Analysis.LogSyntaxError("Syntax error", LineNumber); }
        |   error LengthKeyword {  $$ = new InvalidExpressionNode(LineNumber); yyclearin(); Analysis.LogSyntaxError("Syntax error", LineNumber); }; 

ExprList_Opt    :   Expr ExprListRest_Opt { ((ExpressionListNode)$2).expressionList.Reverse(); ((ExpressionListNode)$2).AddExpression((ExpressionNode)$1); ((ExpressionListNode)$2).expressionList.Reverse(); $$=$2; }
                |   /* Empty */ { new ExpressionListNode(LineNumber); };

ExprListRest_Opt:   ExprListRest_Opt Comma Expr  {  ((ExpressionListNode)$1).AddExpression((ExpressionNode)$3); $$=$1; }
                |   /* Empty */ { $$ = new ExpressionListNode(LineNumber); };

%%

    internal Parser(AbstractScanner<BaseASTNode,LexLocation> scanner) : base(scanner) {  }
