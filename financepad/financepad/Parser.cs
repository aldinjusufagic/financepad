using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace financepad
{
    public class Node
    {
        public string type { get; set; }
        public string value { get; set; }
        public List<Node> children { get; set; } = new List<Node>();
    }
    public class Parser
    {
        public static Node Parse(List<Token> tokens)
        {
            var root = new Node() { type = "Program" };
            int index = 0;
            while (index < tokens.Count)
            {

                if (tokens[index].type == "Label")
                {
                    var labelNode = new Node
                    {
                        type = "Label",
                        value = tokens[index].lexeme
                    };
                    root.children.Add(labelNode);
                    index++;

                    if (tokens[index].type == "Operator")
                    {
                        var operationNode = new Node
                        {
                            type = "Label",
                            value = tokens[index].lexeme
                        };
                        labelNode.children.Add(operationNode);
                        if (tokens[index].type == "Number")
                        {
                            var numberNode = new Node
                            {
                                type = "Number",
                                value = tokens[index].lexeme
                            };
                            operationNode.children.Add(numberNode);
                            index++;
                        }
                        if (tokens[index].type == "Identifier")
                        {
                            var identifierNode = new Node
                            {
                                type = "Identifier",
                                value = tokens[index].lexeme
                            };
                            operationNode.children.Add(identifierNode);
                            index++;
                        }
                    }
                }
                else
                    index++;
            }
            return root;
        }
    }
}
