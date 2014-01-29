using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.AST;
using MiniJava.Visitors;
using MiniJava.Types;
using MiniJava.Definitions;

namespace MiniJava.Frontend.Visitors
{
    internal class ClassBaseTypesExtractorVisitor : BaseVisitor
    {
        public ClassBaseTypesExtractorVisitor(ProgramAnalysis analysis)
            : base(analysis)
        {
        }

        #region Visitor Members

        public override void Visit(MainClassDeclNode node)
        {
        }
        public override void Visit(ClassDeclNode node)
        {
            ClassBeingVisited = Analysis.Environment.Classes.Lookup(node.className.name);

            if (node.extendsClass != null)
                node.extendsClass.Accept(this);
        }
        public override void Visit(ExtendsNode node)
        {
            if (Analysis.Environment.Classes.Contains(node.className.name))
                ClassBeingVisited.ClassType.BaseClassType = Analysis.Environment.Classes.Lookup(node.className.name).ClassType;
            else
                Analysis.LogSemanticError("Unknown class: " + node.className.name, node.lineNumber);
        }

        #endregion
    }
}
