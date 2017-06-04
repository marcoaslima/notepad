using Notepad.Core.Global;
using Notepad.Core.IO;
using Notepad.Core.Settings;
using Notepad.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
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
                return menuBrowser.Enabled;
            }

            set
            {
                menuBrowser.Enabled = value;
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
        public Language MyLanguage { get; set; }
        public AppData MyAppData { get; set; }


        public FormMain(AppData MyAppData = null, Language MyLanguage = null)
        {
            this.MyAppData = MyAppData;
            this.MyLanguage = MyLanguage;

            InitializeComponent();
            InitializeLanguage();
            InitializeSettings();

            buffer = new List<String>();
            previousTitle = this.Text;
        }

        public void InitializeSettings()
        {
            this.ApplyTheme(this.MyAppData.SearchThemeByName(this.MyAppData.DefaultThemeName));
        }

        public void InitializeLanguage()
        {
            ApplyMenu(this.menuForm);
            this.lblLanguage.Text = MyLanguage.LangName;
            this.Text = MyLanguage.GetKey("FormMain.Text").Translation;
        }

        public void ApplyMenu(MenuStrip menuForm)
        {
            foreach (ToolStripMenuItem menu in menuForm.Items)
            {
                menu.Text = MyLanguage.SetComponentLanguage(this.Name, menu.Name);

                if (menu.HasDropDownItems)
                {
                    ApplyMenuItem(menu.DropDownItems, this);
                }
            }
        }

        private void ApplyMenuItem(ToolStripItemCollection toolStripItemCollection, Form form)
        {
            foreach (ToolStripItem item in toolStripItemCollection)
            {
                item.Text = MyLanguage.SetComponentLanguage(this.Name, item.Name);

                if (item.GetType() == typeof(ToolStripMenuItem))
                {
                    ApplyMenuItem(((ToolStripMenuItem)item).DropDownItems, form);
                }
            }
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
            if (fileIsSave || currentArchive == null)
            {
                OpenFile();
            }
            else
            {
                Notepad.Core.Global.Message message = MyLanguage.GetMessage("MessageSaveFileBeforeExit");
                
                DialogResult dr = MessageBox.Show(message.Text, message.Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
                Read(openFile.FileName);
            }
        }

        public void Read(String Path)
        {
            this.currentArchive = Archive.Read(Path);
            txtContent.AppendText(currentArchive.content);
            this.fileIsSave = true;
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
                PerformQuestionExit(e);
            }
        }

        private void PerformQuestionExit(FormClosingEventArgs e = null)
        {
            if (this.MyAppData.General.AskBeforeExit)
            {
                if (!fileIsSave)
                {
                    Notepad.Core.Global.Message message = MyLanguage.GetMessage("MessageSaveFileBeforeExit");

                    DialogResult dr = MessageBox.Show(message.Text, message.Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (!SaveFile().Equals(String.Empty))
                        {
                            Application.Exit();
                        }
                        else
                        {
                            if (e != null) { e.Cancel = true; }
                        }
                    }
                    else if (dr == System.Windows.Forms.DialogResult.No)
                    {
                        Application.Exit();
                    }
                    else if (dr == System.Windows.Forms.DialogResult.Cancel)
                    {
                        if (e != null) { e.Cancel = true; }
                    }

                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MyAppData.General.ExitAltF4)
            {
                PerformQuestionExit();
            }
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
            fileIsSave = true;
        }
        
        private void opcoesGeraisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings settings = new FormSettings(this.MyAppData, this);
            settings.ShowDialog();
        }

        private void configuraçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings settings = new FormSettings(this.MyAppData, this);
            settings.ShowDialog();
        }

        private void fonteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK & !String.IsNullOrEmpty(txtContent.Text))
            {
                txtContent.Font = fontDialog.Font;
            }          
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentArchive != null)
            {
                this.currentArchive.Save();
            }
            else
            {
                currentArchive = new Archive(SaveFile(), txtContent.Text);
                currentArchive.Save();
                fileIsSave = true;
            }
        }

        private void menuLanguage_Click(object sender, EventArgs e)
        {
            FormChangeLanguage changeLanguage = new FormChangeLanguage(this.MyLanguage, this, this.MyAppData);
            changeLanguage.ShowDialog();
        }

        private void menuRun_Click(object sender, EventArgs e)
        {
            if (!fileIsSave || currentArchive == null)
            {
                if(currentArchive == null){
                    currentArchive = new Archive(SaveFile(), txtContent.Text);
                }
                currentArchive.Save();
            }

            String FileExtension = Path.GetExtension(this.currentArchive.path);
            ExecuteAndCompile execute = this.MyAppData.Developer.GetExecuteAndCompile(FileExtension);
            if (execute != null)
            {
                execute.Run(this.currentArchive.path,false);
            }
        }

        private void txtContent_CursorChanged(object sender, EventArgs e)
        {
            int cursorPosition = txtContent.SelectionStart;

            lblLines.Text = String.Format("Ln: {0}/{1}", (txtContent.GetLineFromCharIndex(cursorPosition) + 1), txtContent.Lines.Count());
            lblColumns.Text = String.Format("Cl: {0}/{1}", txtContent.GetPositionFromCharIndex(cursorPosition), "");
            lblPosition.Text = String.Format("Pos: {0}/{1}", "", "");
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            FormAbout about = new FormAbout();
            about.ShowDialog();
        }

        private void menuStartPage_Click(object sender, EventArgs e)
        {
            WebSite.Open(String.Format("http://megari.com.br/software/notepad?apppack=br.com.megari.notepad&appversion={0}&lang={1}", AssemblyVersion, this.MyLanguage.LangCode));
        }

        private void menuNewWindow_Click(object sender, EventArgs e)
        {
            WebSite.Open("notepad.exe");
        }

        private void menuPaste_Click(object sender, EventArgs e)
        {
            Control ctrl = this.ActiveControl;
            if (ctrl != null)
            {
                if (ctrl is TextBox)
                {
                    TextBox tx = (TextBox)ctrl;
                    tx.Paste();
                }
                if (ctrl is RichTextBox)
                {
                    RichTextBox cb = (RichTextBox)ctrl;
                    cb.Paste();
                }
            }
        }

        private void menuCopy_Click(object sender, EventArgs e)
        {
            Control ctrl = this.ActiveControl;
            if (ctrl != null)
            {
                if (ctrl is TextBox)
                {
                    TextBox tx = (TextBox)ctrl;
                    tx.Copy();
                }

                if (ctrl is RichTextBox)
                {
                    RichTextBox tx = (RichTextBox)ctrl;
                    tx.Copy();
                }
            }
        }

        private void menuCrop_Click(object sender, EventArgs e)
        {
            Control ctrl = this.ActiveControl;
            if (ctrl != null)
            {
                if (ctrl is TextBox)
                {
                    TextBox tx = (TextBox)ctrl;
                    tx.Cut();
                }
                if (ctrl is RichTextBox)
                {
                    RichTextBox cb = (RichTextBox)ctrl;
                    cb.Cut();
                }
            }
        }

        private void menuRunAs_Click(object sender, EventArgs e)
        {

        }

        private void menuPrint_Click(object sender, EventArgs e)
        {
            PrintDocument documentToPrint = new PrintDocument();
            printMain.Document = documentToPrint;

            if (printMain.ShowDialog() == DialogResult.OK)
            {
                StringReader reader = new StringReader(txtContent.Text);
                documentToPrint.PrintPage += new PrintPageEventHandler(DocumentToPrint_PrintPage);
                documentToPrint.Print();
            }
        }

        private void DocumentToPrint_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringReader reader = new StringReader(txtContent.Text);
            float LinesPerPage = 0;
            float YPosition = 0;
            int Count = 0;
            float LeftMargin = e.MarginBounds.Left;
            float TopMargin = e.MarginBounds.Top;
            string Line = null;
            Font PrintFont = this.txtContent.Font;
            SolidBrush PrintBrush = new SolidBrush(Color.Black);

            LinesPerPage = e.MarginBounds.Height / PrintFont.GetHeight(e.Graphics);

            while (Count < LinesPerPage && ((Line = reader.ReadLine()) != null))
            {
                YPosition = TopMargin + (Count * PrintFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(Line, PrintFont, PrintBrush, LeftMargin, YPosition, new StringFormat());
                Count++;
            }

            if (Line != null)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
            PrintBrush.Dispose();
        }

        private void menuThemeDark_Click(object sender, EventArgs e)
        {

        }

        public void ApplyTheme(Theme theme)
        {
            this.menuForm.BackColor = System.Drawing.ColorTranslator.FromHtml(theme.MenuBackColor);
            this.txtContent.BackColor =  System.Drawing.ColorTranslator.FromHtml(theme.EditorBackColor);
            this.txtContent.ForeColor =  System.Drawing.ColorTranslator.FromHtml(theme.EditorFontColor);

            foreach (ToolStripMenuItem menu in menuForm.Items)
            {
                menu.ForeColor = System.Drawing.ColorTranslator.FromHtml(theme.MenuFontColor);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}
