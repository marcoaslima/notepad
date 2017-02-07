using Notepad.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Notepad.View
{
    public partial class FormMain : Form
    {
        private List<String> buffer;
        private Boolean textChangedByUser { get; set; }

        private FormBrowser formBrowser { get; set; }
        public Boolean browserNotShowed
        {
            get
            {
                return hTMLBrowserToolStripMenuItem.Enabled;
            }

            set
            {
                hTMLBrowserToolStripMenuItem.Enabled = value;
            }
        }

        public String previousTitle { get; set; }

        public Archive currentArchive
        {
            get
            {
                return _currentArchive;
            }
            set
            {
                this._currentArchive = value;
                this.Text = String.Format("{0} - {1}", this.previousTitle, this.currentArchive.path);
            }
        }
        private Archive _currentArchive { get; set; }

        private Boolean fileIsSave = true;

        public FormMain()
        {
            InitializeComponent();
            buffer = new List<String>();
            previousTitle = this.Text;
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            if (textChangedByUser)
            {
                buffer.Add(this.txtContent.Text);
            }

            if (!browserNotShowed)
            {
                this.formBrowser.webBrowser1.DocumentText = this.txtContent.Text;
            }

            fileIsSave = false;
        }

        private void txtContent_KeyDown(object sender, KeyEventArgs e)
        {
            
            
        }

        private void desfazerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (buffer.Count >= 0)
            {
                this.textChangedByUser = false;
                this.txtContent.Text = buffer.LastOrDefault();
                buffer.Remove(buffer.LastOrDefault());
                this.textChangedByUser = true;
            }
        }

        private void fazerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (buffer.Count > 0)
            {
                this.txtContent.Text = buffer.LastOrDefault();
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileIsSave)
            {
                OpenFile();
            }
            else
            {
                DialogResult dr = MessageBox.Show("Arquivo não foi salvo, salvar antes de sair?", "Abrir", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (currentArchive == null)
                    {
                        this.currentArchive = new Archive(SaveFile());
                    }

                    this.currentArchive.content = txtContent.Text;
                    this.currentArchive.Save(txtContent.Text);

                    OpenFile();
                }
                else if (dr == DialogResult.No)
                {
                    OpenFile();
                }
            }
        }

        private String SaveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Salvar Arquivo Texto";
            saveFileDialog.Filter = "Text File|.txt |"+ "All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0 ;
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.RestoreDirectory = true;

            DialogResult resultado = saveFileDialog.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            return String.Empty;
        }

        public void OpenFile()
        {
            this.txtContent.Clear();
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = false;
            openFile.Title = "Selecionar Arquivo";
            openFile.Filter = "Text File (*.txt)|*.txt|" + "All files (*.*)|*.*";
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;
            openFile.ReadOnlyChecked = true;
            openFile.ShowReadOnly = true;

            DialogResult dr = openFile.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                this.currentArchive = Archive.Read(openFile.FileName);
                txtContent.AppendText(currentArchive.content);
                this.fileIsSave = true;
            }
        }

        private void hTMLBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formBrowser = new FormBrowser(this);
            this.formBrowser.Show();
            this.browserNotShowed = false;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                PerformQuestionExit();
            }
        }

        private void PerformQuestionExit()
        {
            if (!fileIsSave)
            {
                if (MessageBox.Show("Arquivo não salvo, salvar antes de sair?", "SAIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveFile();
                }
                Application.Exit();
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformQuestionExit();
        }

        private void salvarESairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentArchive != null)
            {
                this.currentArchive.Save();
            }
            else
            {
                Archive current = new Archive(SaveFile(), txtContent.Text);
                current.Save();
            }
        }

        private void salvarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentArchive = new Archive(SaveFile(), txtContent.Text);
            currentArchive.Save();
        }
        
        private void opcoesGeraisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings settings = new FormSettings();
            settings.ShowDialog();
        }
    }
}
