using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        public Node Parse(List<Token> tokens)
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

                    if (index < tokens.Count && tokens[index].type == "Modifier")
                    {
                        var modifierNode = new Node
                        {
                            type = "Modifier",
                            value = tokens[index].lexeme
                        };

                        labelNode.children.Add(modifierNode);
                        index++;
                    }

                    addChildOperatorNode(ref index, tokens, labelNode);
                }
                else if (tokens[index].type == "Number")
                {
                    var numberNode = new Node
                    {
                        type = "Number",
                        value = tokens[index].lexeme
                    };
                    root.children.Add(numberNode);
                    index++;
                    if (index < tokens.Count && tokens[index].type == "Variable")
                    {
                        var variableNode = new Node
                        {
                            type = "Variable",
                            value = tokens[index].lexeme
                        };
                        numberNode.children.Add(variableNode);
                        index++;
                    }
                }
                else if (tokens[index].type == "Keyword")
                {
                    if (tokens[index].lexeme == "Template")
                    {
                        var templateNode = new Node
                        {
                            type = "Keyword",
                            value = tokens[index].lexeme
                        };
                        root.children.Add(templateNode);
                        index++;
                        addChildOperatorNode(ref index, tokens, templateNode);
                    }
                }
                else
                    index++;
            }
            return root;
        }
        private void addChildOperatorNode(ref int index, List<Token> tokens, Node parentNode)
        {
            while (index < tokens.Count && tokens[index].type == "Operator")
            {
                var operationNode = new Node
                {
                    type = "Operator",
                    value = tokens[index].lexeme
                };
                index++;

                if (index < tokens.Count && tokens[index].type == "Number")
                {
                    var numberNode = new Node
                    {
                        type = "Number",
                        value = tokens[index].lexeme
                    };

                    operationNode.children.Add(numberNode);
                    index++;
                }

                if (index < tokens.Count && tokens[index].type == "Variable")
                {
                    var variableNode = new Node
                    {
                        type = "Variable",
                        value = tokens[index].lexeme
                    };

                    operationNode.children.Add(variableNode);
                    index++;
                }

                if (index < tokens.Count && tokens[index].type == "Multiply")
                {
                    var multiplyNode = new Node
                    {
                        type = "Multiply",
                        value = tokens[index].lexeme
                    };

                    operationNode.children.Add(multiplyNode);
                    index++;
                    if (index < tokens.Count && tokens[index].type == "Number")
                    {
                        var numberNode = new Node
                        {
                            type = "Number",
                            value = tokens[index].lexeme
                        };

                        multiplyNode.children.Add(numberNode);
                        index++;
                    }
                }
                parentNode.children.Add(operationNode);
            }
        }
    }
}
