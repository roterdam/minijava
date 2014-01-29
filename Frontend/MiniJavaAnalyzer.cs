using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MiniJava.AST;
using MiniJava.Frontend.Visitors;

namespace MiniJava.Frontend
{
    public class MiniJavaAnalyzer
    {
        public ProgramAnalysis Analyze(Stream sourceStream)
        {
            ProgramAnalysis Analysis = new ProgramAnalysis();
            Parser parser = new Parser(new MiniJavaCompiler.Scanner(sourceStream));
            parser.Analysis = Analysis;
            parser.ErrorFlag = !parser.Parse();
            if (parser.ErrorFlag)
            {
                Analysis.LogSyntaxError("Syntax error(s)", 0);
            }
            //else
            {
                CheckStaticSemantics(Analysis);
            }

            return Analysis;
        }

        private static void CheckStaticSemantics(ProgramAnalysis analysis)
        {
            try
            {
                ClassTypesExtractorVisitor CTEVisitor = new ClassTypesExtractorVisitor(analysis);
                ClassBaseTypesExtractorVisitor CBTEVisitor = new ClassBaseTypesExtractorVisitor(analysis);
                SymbolDefinitionsExtractorVisitor SDEVisitor = new SymbolDefinitionsExtractorVisitor(analysis);
                TypeCheckerVisitor TCVisitor = new TypeCheckerVisitor(analysis);
                CTEVisitor.Visit(analysis.AST);
                CBTEVisitor.Visit(analysis.AST);
                SDEVisitor.Visit(analysis.AST);
                TCVisitor.Visit(analysis.AST);
            }
            catch (Exception e)
            {
                analysis.LogSemanticError(e.Message, 0);
            }
        }
    }
}
