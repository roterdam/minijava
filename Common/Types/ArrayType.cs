using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Types
{
    public class ArrayType : BaseType
    {
        public BaseType ElementType;
        public int NoOfDimensions;

        public ArrayType(string name)
            : base(name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name = ElementType.ToString() + "[]";
        }
    }
}
