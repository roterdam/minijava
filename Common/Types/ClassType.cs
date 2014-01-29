using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Types
{
    public class ClassType : BaseType 
    {
        public ClassType BaseClassType;

        public ClassType(string name)
            : base(name)
        {
        }

        public override string ToString()
        {
            return Name + "\n";
        }
    }
}
