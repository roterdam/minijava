using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Types
{
    public class BooleanType : BaseType
    {
        #region Singleton

        private static BooleanType _instance;

        protected BooleanType(string name) : base(name)
        {
        }

        public static BooleanType Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BooleanType("bool");
                }
                return _instance;
            }
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
