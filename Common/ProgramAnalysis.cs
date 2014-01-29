using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.AST;
using MiniJava;

namespace MiniJava
{
    public class ProgramAnalysis
    {
        public ProgramNode AST;
        public Environment Environment;
        public List<ProgramError> Errors;

        public ProgramAnalysis()
        {
            Environment = new Environment();
            Errors = new List<ProgramError>();
        }

        public void LogSyntaxError(string errorMessage, int lineNumber)
        {
            LogSyntaxError(errorMessage, lineNumber, 0);
        }

        public void LogSyntaxError(string errorMessage, int lineNumber, int columnNumber)
        {
            ProgramError Error = new ProgramError();
            Error.Type = ErrorType.Syntax;
            Error.Message = errorMessage;
            Error.LineNumber = lineNumber;
            Error.ColumnNumber = columnNumber;
            Errors.Add(Error);
        }

        public void LogSemanticError(string errorMessage, int lineNumber)
        {
            LogSemanticError(errorMessage, lineNumber, 0);
        }

        public void LogSemanticError(string errorMessage, int lineNumber, int columnNumber)
        {
            ProgramError Error = new ProgramError();
            Error.Type = ErrorType.Semantic;
            Error.Message = errorMessage;
            Error.LineNumber = lineNumber;
            Error.ColumnNumber = columnNumber;
            Errors.Add(Error);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("\nPrinting Symbol Tables");
            sb.AppendLine(Environment.ToString());

            sb.AppendLine("----------------------------------------------------");
            foreach (ProgramError Error in Errors)
                sb.AppendLine(Error.ToString());
            return sb.ToString();            
        }
    }

    
}
