using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Definitions
{
    public class SymbolTable<T> where T : SymbolDefinition, new()
    {
        private Dictionary<string, T> SymbolDictionary;

        public SymbolTable()
        {
            SymbolDictionary = new Dictionary<string, T>();
        }

        public T Add(string name)
        {
            T symbolDefinition = new T();
            symbolDefinition.Name = name;
            SymbolDictionary.Add(name, symbolDefinition);
            return symbolDefinition;
        }

        public void Remove(string name)
        {
            SymbolDictionary.Remove(name);
        }

        public T Lookup(string name)
        {
            if (!SymbolDictionary.ContainsKey(name))
            {
                //T UnknownSymbol = new T();
                //UnknownSymbol.Name = name;
                //SymbolDictionary.Add(name, UnknownSymbol);
                throw new Exception("Unknown symbol: " + name);
            }
            return SymbolDictionary[name];
        }

        public bool Contains(string name)
        {
            return SymbolDictionary.ContainsKey(name);
        }

        public override string ToString()
        {
            Dictionary<string, T>.Enumerator e = SymbolDictionary.GetEnumerator();
            StringBuilder sb = new StringBuilder();
            if(SymbolDictionary.Count > 0)
            while (e.MoveNext())
                sb.AppendLine(e.Current.Value.ToString());
            if (SymbolDictionary.Count > 0)
                sb.AppendLine("------------------------------------------------");
            return sb.ToString();
        }

        public int Count
        {
            get
            {
                return SymbolDictionary.Count;
            }
        }

        public T ItemAt(int index)
        {
             return SymbolDictionary.ElementAt(index).Value;
        }
    }
}
