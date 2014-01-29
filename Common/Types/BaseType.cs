using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Types
{
    public abstract class BaseType
    {
        public string Name;

        public BaseType(string name)
        {
            Name = name;
        }
    }
}
