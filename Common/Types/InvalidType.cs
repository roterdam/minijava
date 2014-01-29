using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Types
{
    public class InvalidType : BaseType 
    {
        #region Singleton

        private static InvalidType _instance;

        protected InvalidType(string name)
            : base(name)
        {
        }

        public static InvalidType Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InvalidType("invalid");
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
