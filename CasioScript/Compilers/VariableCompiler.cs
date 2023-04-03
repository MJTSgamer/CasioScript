using System;
using System.Collections.Generic;
using System.Linq;

namespace CasioScript
{
    public class VariableCompiler
    {
        const string ASSIGNMENT_OPERATOR = "->";

        #region variable declaration

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
                    compiledCode[i] = $"{value}{ASSIGNMENT_OPERATOR}{variableName}";
                }
                else
                {
                    compiledCode[i] = $"{variableNames[i - 1]}{ASSIGNMENT_OPERATOR}{variableName}";
                }
            }

            return compiledCode;
        }

        #endregion

        #region Variable Modification

        public static string CompileVariableModification(string input)
        {
            string[] parts = input.Split(' ');

            string variableName = parts[0].Trim().ToUpper();
            string operatorSymbol = parts[1].Trim();
            string value = parts[2].Trim();

            string compiledCode =
                $"{variableName}{GetOperatorSymbol(operatorSymbol)}{value}{ASSIGNMENT_OPERATOR}{variableName}";

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

        #endregion
    }
}