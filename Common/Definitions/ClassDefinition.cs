using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Types;

namespace MiniJava.Definitions
{
    [Serializable]
    public class ClassDefinition : SymbolDefinition 
    {
        public ClassType ClassType;
        public SymbolTable<FieldDefinition> Fields;
        public SymbolTable<MethodDefinition> Methods;
        public int SizeInBytes;

        public ClassDefinition()
        {
            //ClassType = InvalidType.Instance;
            Fields = new SymbolTable<FieldDefinition>();
            Methods = new SymbolTable<MethodDefinition>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if(ClassType != null)
                sb.Append(ClassType.ToString());
            sb.Append(Fields.ToString());
            sb.Append(Methods.ToString());
            return sb.ToString();
        }
    }
}
