using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Types;

namespace MiniJava.Definitions
{
    public class FieldDefinition : SymbolDefinition
    {
        public BaseType FieldType;
        public int SizeInBytes;
        public int Location;

        //public FieldDefinition()
        //{
        //    FieldType = InvalidType.Instance;
        //}

        public override string ToString()
        {
            return "Field: " + Name + ", Type: " + FieldType.ToString() + ", Location: " + Location.ToString();

        }
    }
}
