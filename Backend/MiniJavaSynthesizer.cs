using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MiniJava.Backend.Visitors;
using MiniJava;

namespace MiniJava.Backend
{
    public class MiniJavaSynthesizer
    {
        TextWriter Out;
        private void GenText(string text)
        {
            Out.WriteLine(text);
        }

        public void Synthesize(ProgramAnalysis sourceProgramAnalysis, TextWriter outputStream)
        {
            LivenessVisitor liveness = new LivenessVisitor(sourceProgramAnalysis);
            liveness.Visit(sourceProgramAnalysis.AST);

            Out = outputStream;
            GenText(".386");
            GenText(".model flat,c");
            GenText("public asm_main");
            GenText("extern put:near,memalloc:near;");
            CodeGeneratorVisitor CGVisitor = new CodeGeneratorVisitor(sourceProgramAnalysis, outputStream);
            MethodTableGeneratorVisitor MTGVisitor = new MethodTableGeneratorVisitor(sourceProgramAnalysis, outputStream);
            SymbolPreprocessorVisitor SPVisitor = new SymbolPreprocessorVisitor(sourceProgramAnalysis);
            MTGVisitor.Visit(sourceProgramAnalysis.AST);
            SPVisitor.Visit(sourceProgramAnalysis.AST);
            CGVisitor.Visit(sourceProgramAnalysis.AST);
            GenText("end");
        }
    }
}
