using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Diagnostics;
using FastColoredTextBoxNS;

namespace Notepad__
{
    
    public partial class Mode1 : Form
    {
        // Ссылки на сборки.
        private List<string> links = new List<string>();
        private string openFile = string.Empty;


        /// <summary>
        /// Конструктор.
        /// </summary>
        public Mode1()
        {
            InitializeComponent();
            links.Add("System.Core.dll");
            richTextBox1.Text = "System.Core.dll";
            openFileDialog1.Filter = "C# file(*.cs)|*.cs";
            saveFileDialog1.Filter = "C# file(*.cs)|*.cs"; 
        }


        /// <summary>
        /// Сохранение файлa 'как'.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = saveFileDialog1.FileName;
                File.WriteAllText(filename, fastColoredTextBox1.Text);
                MessageBox.Show("Файл сохранен.");
            }
            catch
            {
                MessageBox.Show("Файл не удалось сохранить.");
            }
        }


        /// <summary>
        /// Сохранение файлa.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(openFile, fastColoredTextBox1.Text);
            }
            catch
            {
                MessageBox.Show("Файла не существует.");
            }
        }


        /// <summary>
        /// Открытие файлa.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string fileName = openFileDialog1.FileName;
                fastColoredTextBox1.Text = File.ReadAllText(fileName);
                openFile = fileName;
                MessageBox.Show("Файл открыт.");
            }
            catch
            {
                MessageBox.Show("Файл не удается открыть.");
            }
        }


        /// <summary>
        /// Событие изменения текста.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void fastColoredTextBox1_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string text = fastColoredTextBox1.Text;

        }


        /// <summary>
        /// Метод отвечающий за появление менеджера ссылок.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void LinkManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }


        /// <summary>
        /// Метод отвечающий за закрытие менеджера ссылок.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }


        /// <summary>
        /// Метод отвечающий за добавление ссылок в менеджер ссылок.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog2.Filter = "Dll file (*.dll)|*.dll";
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                links.Add(openFileDialog2.FileName);
                richTextBox1.Text += "\n" + openFileDialog2.FileName;
            }
            catch
            {
                MessageBox.Show("Не удалось добавить ссылку.");
            }
        }


        /// <summary>
        /// Метод отвечающий за компиляцию кода.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        [Obsolete]
        private void CompileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //saveFileDialog2.Filter = "EXE file (*.exe)|*.exe";
            //if (saveFileDialog2.ShowDialog() == DialogResult.Cancel)
            //   return;
            // string Output = saveFileDialog2.FileName;
            try
            {
                string Output = "out.exe";
                CSharpCodeProvider codeProvider = new CSharpCodeProvider();
                ICodeCompiler icc = codeProvider.CreateCompiler();
                System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
                parameters.GenerateExecutable = true;
                parameters.OutputAssembly = Output;
                CompilerResults results = icc.CompileAssemblyFromSource(parameters, fastColoredTextBox1.Text);
                if (results.Errors.Count > 0)
                {
                    foreach (CompilerError CompErr in results.Errors)
                    {
                        MessageBox.Show("Line number " + CompErr.Line +
                            ", Error Number: " + CompErr.ErrorNumber +
                            ", '" + CompErr.ErrorText + ";" +
                            Environment.NewLine + Environment.NewLine);
                    }
                }
                else
                {
                    MessageBox.Show("Success!");
                    Process.Start(Output);
                }
            }
            catch
            {
                MessageBox.Show("Не удалось скомпилировать код  :(");
            }

        }
    }
}
