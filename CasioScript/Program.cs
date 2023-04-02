using System;
using System.Collections.Generic;
using System.Linq;

namespace CasioScript
{
    class Program
    {
        static void Main(string[] args)
        {
            var compiledCode = Compiler.Compile(@"
                // variable declaration
                X, Y, Z = 1
                
                // variable modification
                X += Y

                // variable modification
                Z += 1
            ");

            Console.WriteLine(compiledCode);
        }
    }
}