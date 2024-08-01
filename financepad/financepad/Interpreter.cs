using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace financepad
{
    public class Interpreter
    {
        public int result {  get; private set; }
        private readonly Dictionary<string, int> _variables = new Dictionary<string, int>();
        public void Execute(Node program)
        {
            foreach (var child in program.children)
            {
                if (child.type == "Label") 
                {
                    executeLabelNode(child);
                }
            }
        }
        private void executeLabelNode(Node labelNode)
        {
            int labelValue = 0;
            foreach (var child in labelNode.children)
            {
                labelValue = executeOperationNode(labelValue, child);
            }
            result = labelValue; 
        }
        private int executeOperationNode(int parentValue, Node operationNode)
        {
            if (operationNode.children.Count < 2)
                throw new InvalidOperationException("Operation must have two children");

            string operatorValue = operationNode.value;
            int numberValue = int.Parse(operationNode.children[0].value);
            string variableName = operationNode.children[1].value;

            _variables[variableName] = numberValue;

            if (operatorValue == "+")
            {
                parentValue += numberValue;
            }
            else if (operatorValue == "-")
            {
                parentValue -= numberValue;
            }
            else
                throw new InvalidOperationException($"Unknown operator: {operatorValue}");

            return parentValue;
        }
    }
}
