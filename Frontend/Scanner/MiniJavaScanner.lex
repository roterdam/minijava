%namespace MiniJavaCompiler
%option unicode
%using MiniJava.AST;

alphabet [a-zA-Z_$]
digit [0-9]
alphanumeric ({alphabet}|{digit})
hexdigit [0-9A-Fa-f]
octdigit [0-7]
nonzerodigit [1-9]

%%

/*********Reserved Words********/

"if"                    return (int)Tokens.IfKeyword;
"else"                  return (int)Tokens.ElseKeyword;
"while"                 return (int)Tokens.WhileKeyword;
"class"                 return (int)Tokens.ClassKeyword;;
"extends"               return (int)Tokens.ExtendsKeyword;
"public"                return (int)Tokens.PublicKeyword;
"static"                return (int)Tokens.StaticKeyword;
"void"                  return (int)Tokens.VoidKeyword;
"main"                  return (int)Tokens.MainKeyword;
"boolean"               return (int)Tokens.BooleanKeyword;
"int"                   return (int)Tokens.IntKeyword;
"String"                return (int)Tokens.StringKeyword;
"true"                  yylval = new BooleanConstantExpressionNode(true,yyline); return (int)Tokens.TrueKeyword;
"false"                 yylval = new BooleanConstantExpressionNode(false,yyline); return (int)Tokens.FalseKeyword;
"this"                  return (int)Tokens.ThisKeyword;
"new"                   return (int)Tokens.NewKeyword;
"System.out.println"    return (int)Tokens.SystemOutPrintLnKeyword;
"return"                return (int)Tokens.ReturnKeyword;
"length"                return (int)Tokens.LengthKeyword;

/**********Operators********/

"+"         return (int)Tokens.AddOperator;
"-"         return (int)Tokens.SubtractOperator;
"*"         return (int)Tokens.MultiplyOperator;
"/"         return (int)Tokens.DivideOperator;
"="         return (int)Tokens.EqualsOperator;
"<"         return (int)Tokens.LessThanOperator;
"&&"        return (int)Tokens.AndAndOperator;
"!"         return (int)Tokens.NotOperator;

/********Punctuation********/

"("         return (int)Tokens.RoundBracketOpen;
")"         return (int)Tokens.RoundBracketClose;
"{"         return (int)Tokens.CurlyBracketOpen;
"}"         return (int)Tokens.CurlyBracketClose;
"["         return (int)Tokens.SquareBracketOpen;
"]"         return (int)Tokens.SquareBracketClose;
";"         return (int)Tokens.SemiColon;
"."         return (int)Tokens.Dot;
","         return (int)Tokens.Comma;

/*******Integer Constants********/

(("0"|{nonzerodigit}{digit}*)|("0"[xX]{hexdigit}+)|("0"{octdigit}+))    yylval = new IntegerConstantExpressionNode(int.Parse(yytext),yyline);  return (int)Tokens.IntegerConstant;

/***********Identifier***********/

{alphabet}({alphanumeric})*     yylval = new IdentifierNode(yytext,yyline);	return (int)Tokens.Identifier;

/*************EOF**************/

<<EOF>>                         return (int)Tokens.EOF;

/******Stuff to Ignore********/

"//".*"\n"                          {Parser.LineNumber++; tokLin++;}        /* Single line Comment */
"/*"(.|"\n")*"*/"                   {Parser.LineNumber++; tokLin++;}        /* Multi-line Comment */
[ \t\r]                             /* Whitespace */
"\n"                                {Parser.LineNumber++; tokLin++;}

/*********Invalid Character****/

[\u0000-\uffff]                     return (int)Tokens.Null;

%%
