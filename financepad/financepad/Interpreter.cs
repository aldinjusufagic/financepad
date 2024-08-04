using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace financepad
{
    public class Interpreter
    {
        public int result {  get; private set; }
        private readonly Dictionary<string, int> _variables = new Dictionary<string, int>();
        public readonly Dictionary<string, int> _labels = new Dictionary<string, int>();
        private int _templateValue = 0;
        private bool _hasTemplate = false;
        public void Execute(Node program)
        {
            foreach (var child in program.children)
            {
                if (child.type == "Label") 
                {
                    _labels[child.value] = 0;
                    executeLabelNode(child);
                }
                else if (child.type == "Number")
                {
                    executeNumberNode(child);
                }
                else if (child.type == "Keyword")
                {
                    getKeyword(child);
                }
            }
        }
        private void getKeyword(Node keyword)
        {
            if (keyword.value == "Template")
            {
                _hasTemplate = true;
                executeTemplateNode(keyword);
            }
        }
        private void executeTemplateNode(Node templateNode) 
        {
            int templateValue = 0;
            foreach (var child in templateNode.children)
            {
                if (child.type == "Operator")
                    templateValue = executeOperationNode(templateValue, child);
                _templateValue = templateValue;
            }
        }
        private void executeNumberNode(Node numberNode)
        {
            if (numberNode.children.Count == 1 && numberNode.children[0].type == "Variable")
            {
                string variableName = numberNode.children[0].value;
                _variables[variableName] = int.Parse(numberNode.value);
            }
            else
                throw new InvalidOperationException("Variable declaration is incorrect");
        }
        private void executeLabelNode(Node labelNode)
        {
            int labelValue;
            if (_hasTemplate)
                labelValue = _templateValue;
            else
                labelValue = 0;

            Debug.WriteLine(labelValue);
            foreach (var child in labelNode.children)
            {
                if (child.type == "Operator")
                    labelValue = executeOperationNode(labelValue, child);

                if (child.type == "Modifier")
                {
                    if (_labels.ContainsKey(child.value))
                    {
                        labelValue += _labels[child.value];
                    }
                    else
                        throw new ArgumentException($"Modifier {child.value} does not exist");
                }
            }
            _labels[labelNode.value] = labelValue;
        }
        private int executeOperationNode(int parentValue, Node operationNode)
        {
            string operatorValue = operationNode.value;

            if (operationNode.children.Count == 2)
            {
                if (operationNode.children[0].type == "Number" && operationNode.children[1].type == "Variable")
                {
                    int numberValue = int.Parse(operationNode.children[0].value);
                    string variableName = operationNode.children[1].value;

                    _variables[variableName] = numberValue;

                    parentValue = calculateOperation(operatorValue, parentValue, numberValue);
                }
                else
                {
                    throw new InvalidOperationException("Operation children types are incorrect.");
                }
            }
            else if (operationNode.children.Count == 1)
            {
                Node child = operationNode.children[0];
                if (child.type == "Variable")
                {
                    if (_variables.ContainsKey(child.value))
                    {
                        parentValue = calculateOperation(operatorValue, parentValue, _variables[child.value]);
                    }
                    else
                        throw new ArgumentException("Identifier does not exist");
                }
                else
                    throw new InvalidOperationException($"Argument must be of type: Identifier");
            }
            else
                throw new ArgumentNullException("Operation must have at least one Argument");

            return parentValue;
        }
        private int calculateOperation(string type, int labelValue, int childValue)
        {
            if (type == "+")
            {
                labelValue += childValue;
            }
            else if (type == "-")
            {
                labelValue -= childValue;
            }
            else
                throw new InvalidOperationException($"Unknown operator: {type}");

            return labelValue;
        }
    }
}
