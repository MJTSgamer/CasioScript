using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using CasioScript.CasioScript;

namespace CasioScript
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = new System.IO.StreamReader(@"C:\Users\mattt\RiderProjects\CasioScript\CasioScript\CasioScript\test.casio");
            if (args.Length > 0)
                file = new System.IO.StreamReader(args[0]);
            
            var fileContent = file.ReadToEnd();
            
            var inputStream = new AntlrInputStream(fileContent);
            var casioScriptLexer = new CasioScriptLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(casioScriptLexer);
            var casioScriptParser = new CasioScriptParser(commonTokenStream);
            var casioScriptContext = casioScriptParser.program();
            var casioScriptVisitor = new CasioScriptVisitor();  
            
            casioScriptVisitor.Visit(casioScriptContext);
        }
    }
}