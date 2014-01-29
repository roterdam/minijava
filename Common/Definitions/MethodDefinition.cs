using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Types;

namespace MiniJava.Definitions
{
    public class MethodDefinition : SymbolDefinition
    {
        public ClassDefinition ClassDefinition;
        public SymbolTable<ParameterDefinition> Parameters;
        public SymbolTable<VariableDefinition> LocalVariables;
        public BaseType ReturnType;
        public int SizeOfParametersInBytes;
        public int SizeOfLocalVariablesInBytes;
        public int Location;

        public MethodDefinition()
        {
            //ReturnType = InvalidType.Instance;
            Parameters = new SymbolTable<ParameterDefinition>();
            LocalVariables = new SymbolTable<VariableDefinition>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Method: ");
            sb.AppendLine(Name);
            sb.Append(Parameters.ToString());
            sb.Append(LocalVariables.ToString());
            sb.Append(" Location: " + Location.ToString());
            return sb.ToString();
        }
    }
}
