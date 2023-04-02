using System.Linq;

namespace CasioScript
{
    public class VariableCompiler
    {
        public static string[] CompileVariableDeclaration(string input)
        {
            string[] parts = input.Split('=');

            string[] variableNames = parts[0].Split(',').Select(x => x.Trim().ToUpper()).ToArray();

            string[] compiledCode = new string[variableNames.Length];
            
            for (int i = 0; i < variableNames.Length; i++)
            {
                string variableName = variableNames[i];
                string value = parts[1].Trim();
                
                if (i == 0)
                {
                    compiledCode[i] = $"{value}->{variableName}";
                }
                else
                {
                    compiledCode[i] = $"{variableNames[i - 1]}->{variableName}";
                }
            }
            
            return compiledCode;
        }

        public static string CompileVariableModification(string input)
        {
            string[] parts = input.Split(' ');

            string variableName = parts[0].Trim().ToUpper();
            string operatorSymbol = parts[1].Trim();
            string value = parts[2].Trim();

            string compiledCode = $"{variableName}{GetOperatorSymbol(operatorSymbol)}{value}->{variableName}";

            return compiledCode;
        }

        private static string GetOperatorSymbol(string symbol)
        {
            switch (symbol)
            {
                case "+=":
                    return "+";
                case "-=":
                    return "-";
                case "*=":
                    return "*";
                case "/=":
                    return "/";
                default:
                    return "";
            }
        }
    }
}