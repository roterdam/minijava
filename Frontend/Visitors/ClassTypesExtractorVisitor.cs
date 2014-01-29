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
    internal class ClassTypesExtractorVisitor : BaseVisitor
    {
        public ClassTypesExtractorVisitor(ProgramAnalysis analysis)
            : base(analysis)
        {
        }

        #region Visitor Members

        public override void Visit(ProgramNode node)
        {
            ClassDefinition StringClassDefinition = Analysis.Environment.Classes.Add("String");
            StringClassDefinition.ClassType = new ClassType("String");
            node.mainClassDecl.Accept(this);
            for (int x = 0; x < node.classDeclList.classDeclList.Count; x++)
                node.classDeclList.ClassDeclAtIndex(x).Accept(this);
        }

        public override void Visit(MainClassDeclNode node)
        {
            ClassBeingVisited = Analysis.Environment.Classes.Add(node.classIdentifier.name);
        }

        public override void Visit(ClassDeclNode node)
        {
            ClassType classType = new ClassType(node.className.name);
            ClassBeingVisited = Analysis.Environment.Classes.Add(node.className.name);
            ClassBeingVisited.ClassType = classType;

            if(node.extendsClass != null)
                node.extendsClass.Accept(this);
        }

        #endregion
    }
}
