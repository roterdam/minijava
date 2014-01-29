using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Definitions
{
    public abstract class SymbolDefinition 
    {
        public string Name;
        
        public override string ToString()
        {
            return Name;
        }
    }
}
