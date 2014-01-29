using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MiniJava.AST;
using MiniJava.Visitors;

namespace MiniJava.Backend.Visitors
{
    internal class MethodTableGeneratorVisitor : BaseVisitor
    {
        private TextWriter Out;

        #region Gen

        private void GenText(string text)
        {
            Out.WriteLine(text);
        }

        #endregion

        public MethodTableGeneratorVisitor(ProgramAnalysis analysis, TextWriter outputStream) : base(analysis)
        {
            Out = outputStream;
        }

        #region Visitor Members

        public override void Visit(ProgramNode node)
        {
            node.mainClassDecl.Accept(this);
            GenText(".data");
            for (int x = 0; x < node.classDeclList.classDeclList.Count; x++)
                node.classDeclList.ClassDeclAtIndex(x).Accept(this);
        }

        #region Declarations

        public override void Visit(ClassDeclNode node)
        {
            ClassBeingVisited = Analysis.Environment.Classes.Lookup(node.className.name);
            if (node.extendsClass != null)
                GenText(node.className.name + "$$ dd " + node.extendsClass.className.name + "$$");
            else
                GenText(node.className.name + "$$ dd 0");

            Dictionary<string, string> VTable = GenerateVTable(ClassBeingVisited);
            Dictionary<string, string>.Enumerator enumerator = VTable.GetEnumerator();
            int MethodOffset = 4;
            while (enumerator.MoveNext())
            {
                GenText("\tdd " + enumerator.Current.Value + "$" + enumerator.Current.Key);
                if (ClassBeingVisited.Methods.Contains(enumerator.Current.Key))
                    ClassBeingVisited.Methods.Lookup(enumerator.Current.Key).Location = MethodOffset;
                MethodOffset += 4;
            }
        }

        private Dictionary<string,string> GenerateVTable(Definitions.ClassDefinition classDefinition)
        {
            Dictionary<string, string> VTable;
            if (classDefinition.ClassType.BaseClassType == null)
            {
                VTable = new Dictionary<string,string>();
                for (int x = 0; x < classDefinition.Methods.Count; x++)
                {
                    string MethodName = classDefinition.Methods.ItemAt(x).Name;
                    VTable.Add(MethodName, classDefinition.Name);
                }
            }
            else
            {
                VTable = GenerateVTable(Analysis.Environment.Classes.Lookup(classDefinition.ClassType.BaseClassType.Name));
                for (int x = 0; x < classDefinition.Methods.Count; x++)
                {
                    string MethodName = classDefinition.Methods.ItemAt(x).Name;
                    if (VTable.ContainsKey(MethodName))
                        VTable[MethodName] = classDefinition.Name;
                    else
                        VTable.Add(MethodName, classDefinition.Name);
                }
            }
            return VTable;
        }

        #endregion

        #endregion
    }
}
