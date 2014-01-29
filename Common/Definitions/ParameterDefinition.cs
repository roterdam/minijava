using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Types;

namespace MiniJava.Definitions
{
    public class ParameterDefinition : SymbolDefinition
    {
        public BaseType ParameterType;
        public int SizeInBytes;
        public int Location;

        //public ParameterDefinition()
        //{
        //    ParameterType = InvalidType.Instance;
        //}
        
        public override string ToString()
        {
            return "Parameter: " + Name + ", Type: " + ParameterType.ToString() + ", Location: " + Location.ToString();
        }
    }
}
