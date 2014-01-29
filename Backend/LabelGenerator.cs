using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Backend
{
    internal class LabelGenerator
    {
        private int LabelCounter;
        private string LavelPrefix;

        public LabelGenerator(string labelPrefix)
        {
            this.LabelCounter = 0;
            this.LavelPrefix = labelPrefix;
        }

        public string GetNewLabel()
        {
            LabelCounter++;
            return this.LavelPrefix + LabelCounter.ToString();
        }
    }
}
