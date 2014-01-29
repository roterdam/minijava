using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.AST;
using MiniJava.Frontend;
using MiniJava.Backend;

namespace MiniJava.Compiler
{
    class MiniJavaCompiler
    {
        static void Main(string[] args)
        {
            string sourceFilePath;
            string outputFilePath;

            if (args.Length > 0)
            {
                sourceFilePath = args[0];
                outputFilePath = args[0].Substring(0, args[0].LastIndexOf('.')) + ".asm";
            }
            else
                throw new Exception("File path required");

            if (Compile(sourceFilePath, "test.asm"))
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = "runasm.cmd";
                proc.Start();
            }
            Console.ReadLine();
        }

        private static bool Compile(string sourceFilePath, string outputFilePath)
        {
            System.IO.FileStream fstream = System.IO.File.OpenRead(sourceFilePath);
            MiniJavaAnalyzer Analyzer = new MiniJavaAnalyzer();
            ProgramAnalysis Analysis = Analyzer.Analyze(fstream);
            fstream.Close();

            if (Analysis.Errors.Count == 0)
            {
                System.IO.TextWriter outputStream = new System.IO.StreamWriter(outputFilePath);
                MiniJavaSynthesizer Synthesizer = new MiniJavaSynthesizer();
                Synthesizer.Synthesize(Analysis, outputStream);
                outputStream.Close();
                Console.WriteLine(Analysis.ToString());
                return true;
            }

            Console.WriteLine(Analysis.ToString());
            Console.WriteLine(Analysis.Errors.Count.ToString() + " Error(s) found");

            return false;
        }


    }
}
