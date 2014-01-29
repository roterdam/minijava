using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Types;

namespace MiniJava.Definitions
{
    public class VariableDefinition : SymbolDefinition
    {
        public BaseType VariableType;
        public int SizeInBytes;
        public int Location;

        //public VariableDefinition()
        //{
        //    VariableType = InvalidType.Instance;
        //}

        public override string ToString()
        {
            return "Variable: " + Name + ", Location: " + Location.ToString();
        }
    }
}
