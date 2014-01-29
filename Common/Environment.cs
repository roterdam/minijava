using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Definitions;
using MiniJava.Types;

namespace MiniJava
{
    public class Environment
    {
        public SymbolTable<ClassDefinition> Classes;

        public Environment()
        {
            Classes = new SymbolTable<ClassDefinition>();
        }

        public FieldDefinition LookupFieldInClass(string fieldName, ClassDefinition classDefinition)
        {
            ClassDefinition currentClass = classDefinition;
            while (true)
            {
                if (currentClass.Fields.Contains(fieldName))
                    return currentClass.Fields.Lookup(fieldName);
                if (currentClass.ClassType.BaseClassType == null)
                {
                    currentClass.Fields.Add(fieldName).FieldType = InvalidType.Instance;
                    throw new Exception("Undeclared identifier: " + fieldName);
                }
                currentClass = Classes.Lookup(currentClass.ClassType.BaseClassType.Name);
            }
        }

        public MethodDefinition LookupMethodInClass(string methodName, ClassDefinition classDefinition)
        {
            ClassDefinition currentClass = classDefinition;
            while (true)
            {
                if (currentClass.Methods.Contains(methodName))
                    return currentClass.Methods.Lookup(methodName);
                if (currentClass.ClassType.BaseClassType == null)
                {
                    //currentClass.Methods.Add(methodName).ReturnType = InvalidType.Instance;
                    throw new Exception("Undeclared identifier: " + methodName);
                }
                currentClass = Classes.Lookup(currentClass.ClassType.BaseClassType.Name);
            }
        }

        public override string ToString()
        {
            return Classes.ToString();
        }
    }
}
