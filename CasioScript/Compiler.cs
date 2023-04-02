using System;
using System.Linq;
using System.Collections.Generic;

namespace CasioScript
{
    public class Compiler
    {
        public static string Compile(string inputCode)
        {
            List<string> CompiledCode = new List<string>();

            // Split the input code into lines
            string[] lines = inputCode.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();

            // Process each line
            foreach (string line in lines)
            {
#region Comments
                if (line.StartsWith("//"))
                {
                    continue;
                }
#endregion

#region Variables
                //Check if line is a variable declaration: X = 1; or X, Y, Z = 1; or X > 1; etc.
                if (line.Contains("=") && !line.Contains("+=") && !line.Contains("-=") && !line.Contains("*=") && !line.Contains("/="))
                {
                    // Compile the variable declaration
                    string[] compiledCode = VariableCompiler.CompileVariableDeclaration(line);
                    
                    // Add the compiled code to the list of instructions
                    CompiledCode.AddRange(compiledCode);
                }
                
                
                //Check if line is a variable modification: X += 1; or X -= 1; etc.
                else if (line.Contains("+=") || line.Contains("-=") || line.Contains("*=") || line.Contains("/="))
                {
                    // Compile the variable modification
                    string compiledCode = VariableCompiler.CompileVariableModification(line);
                    
                    // Add the compiled code to the list of instructions
                    CompiledCode.Add(compiledCode);
                }
#endregion

            }
            return string.Join(Environment.NewLine, CompiledCode);
        }
    }
}

