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

namespace Notepad__
{
    // Рекомендуется почитать read.me
    public partial class Form1 : Form
    {
        private readonly List<(TabPage,bool)> tabs = new List<(TabPage,bool)>();
        private readonly List<string> paths = new List<string>();
        private Timer timer = new Timer();
        private Color color = Color.White;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Text File(.txt)|*.txt|Text File(.rtf)|*.rtf";
            openFileDialog1.Filter = "Text File(.txt)|*.txt|Text File(.rtf)|*.rtf";
        }
      
        /// <summary>
        /// Сохранение файлов.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void SaveAsClick(object sender, EventArgs e)
        {
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                StreamWriter sw= new StreamWriter(saveFileDialog1.FileName);
                string fileName = saveFileDialog1.FileName;
                if (Path.GetExtension(fileName) == ".rtf")
                    sw.WriteLine(richTextBox1.Rtf);
                else
                    sw.WriteLine(richTextBox1.Text);
                sw.Close();
                MessageBox.Show("Файл сохранен.");
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить файл.");
            }
        }

        /// <summary>
        /// Открытие файлов.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void OpenFileClick(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string fileName = openFileDialog1.FileName;
            try
            {
                TabPage tabPage = new TabPage(fileName.Split("\\")[^1]);
                RichTextBox richTextBox = new RichTextBox();
                richTextBox.Dock = DockStyle.Fill;
                richTextBox.TextChanged += Form1_TextChanged;
                richTextBox.ContextMenuStrip = contextMenuStrip1;
                tabPage.Controls.Add(richTextBox);
                tabControl1.TabPages.Add(tabPage);
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[^1].Controls[0];
                richTextBox1.BackColor = color;
                if (Path.GetExtension(fileName) == ".rtf")
                    richTextBox.LoadFile(fileName);
                else
                    richTextBox.LoadFile(fileName, RichTextBoxStreamType.PlainText);
                paths.Add(fileName);
                tabs.Add((tabPage, true));
                MessageBox.Show("Файл открыт.");
            }
            catch
            {
                MessageBox.Show("Файл не удается открыть.");
            }
        }


        /// <summary>
        /// Mетод вставки текста из буфера обмена в файл.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void PasteMethodClick(object sender, EventArgs e)
        {
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex ].Controls[0];
                if (richTextBox1.TextLength > 0)
                {
                    richTextBox1.Paste();
                }
            }
            catch{ }
        }


        /// <summary>
        /// Mетод для вырезания фрагмента текста.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void CutMethodClick(object sender, EventArgs e)
        {
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex ].Controls[0];
                if (richTextBox1.TextLength > 0)
                {
                    richTextBox1.Cut();
                }
            }
            catch { }
        }


        /// <summary>
        /// Mетод для копирования фрагмента текста.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void CopyMethodClick(object sender, EventArgs e)
        {
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex ].Controls[0];
                if (richTextBox1.TextLength > 0)
                {
                    richTextBox1.Copy();
                }
            }
            catch { }
        }


        /// <summary>
        /// Mетод для изменения шрифта текста.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void FontClick(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                tabs[tabControl1.SelectedIndex] = (tabs[tabControl1.SelectedIndex].Item1, false);
                richTextBox1.Font = fontDialog1.Font;
            }
            catch
            {
                MessageBox.Show("Не удалось поменять шрифт");
            }
        }


        /// <summary>
        /// Mетод для изменения фона текста.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void BackColorClick(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex ].Controls[0];
            richTextBox1.BackColor = colorDialog1.Color;
            }
            catch
            {
                MessageBox.Show("Не удалось изменить фон");
            }
        }


        /// <summary>
        /// Mетод для выделения всего текста.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void SelectAll(object sender, EventArgs e)
        {
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                if (richTextBox1.TextLength > 0)
                {
                    richTextBox1.SelectAll();
                }
            }
            catch
            { }
            }


        /// <summary>
        /// Mетод для копирования текста.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                if (richTextBox1.TextLength > 0)
                {
                    richTextBox1.Copy();
                }
            }
            catch
            { }
        }


        /// <summary>
        /// Mетод вставки текста из буфера обмена в файл.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void PasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex ].Controls[0];
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Paste();
            }
            }
            catch
            { }
        }


        /// <summary>
        /// Mетод для вырезания фрагмента текста.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void CutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                if (richTextBox1.TextLength > 0)
                {
                    richTextBox1.Cut();
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Mетод для выделения всего текста.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void SelectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                if (richTextBox1.TextLength > 0)
                {
                    richTextBox1.SelectAll();
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Mетод для открытия вкладок компиляции.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void CSharpCompiler(object sender, EventArgs e)
        {
            Mode1 mode1 = new Mode1();
            mode1.Show();
        }


        /// <summary>
        /// Mетод для создания вкладки.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void CreateTab(object sender, EventArgs e)
        {
            try
            {
                string title = "Untitled " + (tabControl1.TabCount + 1).ToString();
                TabPage tabPage = new TabPage(title);
                RichTextBox richTextBox = new RichTextBox();
                tabs.Add((tabPage, true));
                richTextBox.Dock = DockStyle.Fill;
                richTextBox.TextChanged += Form1_TextChanged;
                richTextBox.BackColor = color;
                richTextBox.ContextMenuStrip = contextMenuStrip1;
                tabPage.Controls.Add(richTextBox);
                tabControl1.TabPages.Add(tabPage);
                paths.Add("");
            }
            catch
            {
                MessageBox.Show("Не удалось создать вкладку");
            }
        }


        /// <summary>
        /// Mетод для удаления вкладки.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void toolStripRemove_Click(object sender, EventArgs e)
        {
            try
            {
                paths.RemoveAt(tabControl1.SelectedIndex);
                tabs.RemoveAt(tabControl1.SelectedIndex);
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
            catch
            {
                MessageBox.Show("Не удалось удалить вкладку");
            }
        }


        /// <summary>
        /// Mетод для отмены действия.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                richTextBox1.Undo();
            }
            catch
            {
                MessageBox.Show("Не удалось отменить действие");
            }
        }


        /// <summary>
        /// Mетод для повтора действия.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
            richTextBox1.Redo();
            }
            catch
            {
                MessageBox.Show("Не удалось повторить действие");
            }
        }


        /// <summary>
        /// Mетод для сохранения файла.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                if (paths[tabControl1.SelectedIndex] == "")
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                        return;
                    string fileName = saveFileDialog1.FileName;
                    paths[tabControl1.SelectedIndex] = fileName;
                    tabControl1.TabPages[tabControl1.SelectedIndex].Text = fileName.Split("\\")[^1];
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                    if (Path.GetExtension(fileName) == ".rtf")
                        sw.WriteLine(richTextBox1.Rtf);
                    else
                        sw.WriteLine(richTextBox1.Text);
                    sw.Close();
                    MessageBox.Show("Файл сохранен.");
                }
                else
                {
                    string fileName = paths[tabControl1.SelectedIndex];
                    StreamWriter sw = new StreamWriter(fileName);
                    if (Path.GetExtension(fileName) == ".rtf")
                        sw.WriteLine(richTextBox1.Rtf);
                    else
                        sw.WriteLine(richTextBox1.Text);
                    sw.Close();
                }
                tabs[tabControl1.SelectedIndex] = (tabs[tabControl1.SelectedIndex].Item1, true);
            }
            catch
            {
                MessageBox.Show("Файл не удалось сохранить.");
            }
        }


        /// <summary>
        /// Mетод для отслеживания изменений в тексте.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void Form1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tabs[tabControl1.SelectedIndex] = (tabs[tabControl1.SelectedIndex].Item1, false);
            }
            catch { }
        }


        /// <summary>
        /// Mетод для выхода из приложения.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch { }
        }


        /// <summary>
        /// Mетод для загрузки заданных настроек и открытия файлов перед началом работы приложения.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string[] oldPaths = Properties.Settings.Default.Text.Split(";");
                for (int i = 0; i < oldPaths.Length; i++)
                {
                    if (oldPaths[i] != "")
                    {
                        string fileName = oldPaths[i];
                        paths.Add(fileName);
                        TabPage tabPage = new TabPage(fileName.Split("\\")[^1]);
                        RichTextBox richTextBox = new RichTextBox();
                        tabs.Add((tabPage, true)); 
                        richTextBox.Dock = DockStyle.Fill;
                        richTextBox.TextChanged += Form1_TextChanged;
                        richTextBox.ContextMenuStrip = contextMenuStrip1;
                        tabPage.Controls.Add(richTextBox);
                        tabControl1.TabPages.Add(tabPage);
                        var richTextBox1 = (RichTextBox)tabControl1.TabPages[^1].Controls[0];
                        if (Path.GetExtension(fileName) == ".rtf")
                            richTextBox.LoadFile(fileName);
                        else
                            richTextBox.LoadFile(fileName, RichTextBoxStreamType.PlainText);
                    }
                }
                Color oldColor = Properties.Settings.Default.Color;
                for (int i = 0; i < tabs.Count; i++)
                {
                    var richTextBox1 = (RichTextBox)tabControl1.TabPages[i].Controls[0];
                    richTextBox1.BackColor = oldColor;
                }
                color = oldColor;
            }
            catch{}
            Initialise_Timer();
        }


        /// <summary>
        /// Инициаализация таймера.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void Initialise_Timer()
        {
            
            int.TryParse(автосохранениеToolStripMenuItem1.SelectedItem.ToString(), out int time);
            if (time == 0)
            {
                time = 5;
            }
            timer.Interval =  time*100000;
            timer.Tick += new EventHandler(TimerTick);
            timer.Start();
        }


        /// <summary>
        /// Событие  возникающее при каждом тике таймера.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void TimerTick(object sender, EventArgs e)
        {
            for (int i = 0; i < paths.Count; i++)
            {
                try
                {
                    var richTextBox1 = (RichTextBox)tabControl1.TabPages[i].Controls[0];
                    if (paths[i] == "")
                    {
                        if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
                        {
                            string fileName = saveFileDialog1.FileName;
                            paths[i] = fileName;
                            tabControl1.TabPages[i].Text = fileName.Split("\\")[^1];
                            StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                            if (Path.GetExtension(fileName) == ".rtf")
                                sw.WriteLine(richTextBox1.Rtf);
                            else
                                sw.WriteLine(richTextBox1.Text);
                            sw.Close();
                            MessageBox.Show("Файл сохранен.");
                        }
                    }
                    else
                    {
                        string fileName = paths[i];
                        StreamWriter sw = new StreamWriter(fileName);
                        if (Path.GetExtension(fileName) == ".rtf")
                            sw.WriteLine(richTextBox1.Rtf);
                        else
                            sw.WriteLine(richTextBox1.Text);
                        sw.Close();
                    }
                    tabs[i] = (tabs[i].Item1, true);
                }
                catch
                {
                    MessageBox.Show("Файл не удалось сохранить.");
                }
            }
        }


        /// <summary>
        /// Вспомогательный метод для сохранения файла.
        /// </summary>
        /// <param name="i">индекс файла в списке</param>
        private void saveFile(int i)
        {
            try
            {
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[i].Controls[0];
                if (paths[i] == "")
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                        return;
                    string fileName = saveFileDialog1.FileName;
                    paths[i] = fileName;
                    tabControl1.TabPages[i].Text = fileName.Split("\\")[^1];
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                    if (Path.GetExtension(fileName) == ".rtf")
                        sw.WriteLine(richTextBox1.Rtf);
                    else
                        sw.WriteLine(richTextBox1.Text);
                    sw.Close();
                    MessageBox.Show("Файл сохранен.");
                }
                else
                {
                    string fileName = paths[i];
                    StreamWriter sw = new StreamWriter(fileName);
                    if (Path.GetExtension(fileName) == ".rtf")
                        sw.WriteLine(richTextBox1.Rtf);
                    else
                        sw.WriteLine(richTextBox1.Text);
                    sw.Close();
                }
                tabs[i] = (tabs[i].Item1, true);
            }
            catch
            {
                MessageBox.Show("Файл не удалось сохранить.");
            }
        }


        /// <summary>
        /// Метод отвечающий за закрытие приложения.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                for (int i = 0; i < tabs.Count; i++)
                {
                    if (tabs[i].Item2 == false)
                    {
                        var d = MessageBox.Show($"Файл {tabControl1.TabPages[i].Text} не сохранен. Хотите сохранить?", "Notepad++", MessageBoxButtons.YesNoCancel);
                        if (d == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                            return;
                        }
                        else if (d == DialogResult.Yes)
                        {
                            saveFile(i);
                        }
                    }
                }
                Properties.Settings.Default.Color = color;
                Properties.Settings.Default.Text = string.Join(";", paths);
                Properties.Settings.Default.Save();
            }
            catch
            {
                MessageBox.Show("Небольшие неполадки");
            }
        }


        /// <summary>
        /// Метод отвечающий за автосохранение.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void AutoSaveToolStripMenuItem1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer.Stop();
            Initialise_Timer();
        }



        /// <summary>
        /// Метод отвечающий за курсивный шрифт.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void ItalicFont(object sender, EventArgs e)
        {
            try
            {
                Font font;
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                font = richTextBox1.SelectionFont;
                var newFont = new Font(font, font.Style | FontStyle.Italic);
                richTextBox1.SelectionFont = newFont;
                richTextBox1.Focus();
            }
            catch { }
        }


        /// <summary>
        /// Метод отвечающий за жирный шрифт.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void BoldFont(object sender, EventArgs e)
        {
            try
            {
                Font font;
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                font = richTextBox1.SelectionFont;
                var newFont = new Font(font, font.Style | FontStyle.Bold);
                richTextBox1.SelectionFont = newFont;
                richTextBox1.Focus();
            }
            catch { }
        }


        /// <summary>
        /// Метод отвечающий за подчеркнутый шрифт.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void UnderLineFont(object sender, EventArgs e)
        {
            try
            {
                Font font;
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                font = richTextBox1.SelectionFont;
                var newFont = new Font(font, font.Style | FontStyle.Underline);
                richTextBox1.SelectionFont = newFont;
                richTextBox1.Focus();
            }
            catch { }
        }


        /// <summary>
        /// Метод отвечающий за зачеркнутый шрифт.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void StrikeoutFont(object sender, EventArgs e)
        {
            try
            {
                Font font;
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                font = richTextBox1.SelectionFont;
                var newFont = new Font(font, font.Style | FontStyle.Strikeout);
                richTextBox1.SelectionFont = newFont;
                richTextBox1.Focus();
            }
            catch { }
        }


        /// <summary>
        /// Метод отвечающий за обычный шрифт.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void RegularFont(object sender, EventArgs e)
        {
            try
            {
                Font font;
                var richTextBox1 = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                font = richTextBox1.SelectionFont;
                var newFont = new Font(font, FontStyle.Regular);
                richTextBox1.SelectionFont = newFont;
                richTextBox1.Focus();
            }
            catch { }
        }


        /// <summary>
        /// Метод отвечающий за шрифт.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void FontProgramm(object sender, EventArgs e)
        {
            try
            {
                colorDialog1.ShowDialog();
                color = colorDialog1.Color;
                for (int i = 0; i < tabs.Count; i++)
                {
                    var richTextBox1 = (RichTextBox)tabControl1.TabPages[i].Controls[0];
                    richTextBox1.BackColor = color;
                }
            }
            catch { }
        }



        /// <summary>
        /// Метод отвечающий за сохранение всех файлов.
        /// </summary>
        /// <param name="sender">Ссылка на обЪект</param>
        /// <param name="e">Событие</param>
        private void SaveAlllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TimerTick(sender, e);
            }
            catch { }
        }
    }
}
