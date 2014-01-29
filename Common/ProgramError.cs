using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava
{
    public enum ErrorType
    {
        Syntax,
        Semantic
    }

    public class ProgramError
    {
        public ErrorType Type;
        public string Message;
        public int LineNumber;
        public int? ColumnNumber;

        public override string ToString()
        {
            return Type.ToString() + " Error at Line " + LineNumber.ToString() + ": " + Message;
        }
    }
}
