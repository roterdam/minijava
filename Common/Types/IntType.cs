using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Types
{
    public class IntType : BaseType
    {
        #region Singleton

        private static IntType _instance;

        protected IntType(string name) : base(name)
        {
        }

        public static IntType Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new IntType("int");
                }
                return _instance;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
