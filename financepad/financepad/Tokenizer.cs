using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace financepad
{
    public class Token
    {
        public string type {  get; set; }
        public string lexeme { get; set; }
    }
    public class Tokenizer
    {
        //@"^([+-])\s*(\d+)\s*\((\w*)\)$"
        //@"^(\w*):$"

        private static readonly Regex OperatorPattern = new Regex(@"^([+-])\s*(\d+)?\s*\((\w*)\)$", RegexOptions.Compiled);
        private static readonly Regex LabelPattern = new Regex(@"^(\w+)(?:\s\((\w+)\))?:$", RegexOptions.Compiled);
        private static readonly Regex VariableDeclarationPattern = new Regex(@"^(\d+)?\s*\((\w*)\)", RegexOptions.Compiled);
        public List<Token> Tokenize(string text)
        {
            var lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var tokens = new List<Token>();
            foreach (var line in lines)
            {
                var labelMatch = LabelPattern.Match(line);
                if (labelMatch.Success)
                {
                    if (labelMatch.Groups[1].Success)
                        tokens.Add(new Token { type = "Label", lexeme = labelMatch.Groups[1].Value });
                    if (labelMatch.Groups[2].Success)
                        tokens.Add(new Token { type = "Modifier", lexeme = labelMatch.Groups[2].Value });
                    continue;
                }

                var operatorMatch = OperatorPattern.Match(line);
                if (operatorMatch.Success)
                {
                    if (operatorMatch.Groups[1].Success)
                        tokens.Add(new Token { type = "Operator", lexeme = operatorMatch.Groups[1].Value });
                    if (operatorMatch.Groups[2].Success)
                        tokens.Add(new Token { type = "Number", lexeme = operatorMatch.Groups[2].Value });
                    if (operatorMatch.Groups[3].Success)
                        tokens.Add(new Token { type = "Identifier", lexeme = operatorMatch.Groups[3].Value });
                    continue;
                }

                var VariableMatch = VariableDeclarationPattern.Match(line);
                if (VariableMatch.Success)
                {
                    Debug.WriteLine($"{VariableMatch.Groups[1].Value} {VariableMatch.Groups[2].Value}");
                    if (VariableMatch.Groups[1].Success)
                        tokens.Add(new Token { type = "Number", lexeme = VariableMatch.Groups[1].Value });
                    if (VariableMatch.Groups[2].Success)
                        tokens.Add(new Token { type = "Identifier", lexeme = VariableMatch.Groups[2].Value });
                    continue;
                }
            }
            return tokens;
        }
    }
}
