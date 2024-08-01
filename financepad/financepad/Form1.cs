using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace financepad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //processText(richTextBox1.Text);
        }
        private void processText(string text)
        {
            var tokenizer = new Tokenizer();

            var tokens = tokenizer.Tokenize(text);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("button clicked");
            runProgram(richTextBox1.Text);
        }
        private void runProgram(string text)
        {
            Debug.WriteLine("running program");

            Debug.WriteLine("getting tokens");
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize(text);

            Debug.Write("parsing");
            var parser = new Parser();
            var syntaxTree = parser.Parse(tokens);

            Debug.WriteLine("interperating");
            var interpreter = new Interpreter();
            interpreter.Execute(syntaxTree);

            Debug.WriteLine($"program result: {interpreter.result}");
            textBox1.Text = interpreter.result.ToString();
        }
    }
}