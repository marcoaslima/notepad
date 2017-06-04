using Notepad.Core;
using Notepad.Core.Global;
using Notepad.Core.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Notepad.View
{
    public partial class FormChangeLanguage : Form
    {
        public System.Globalization.CultureInfo[] languages { get; set; }

        Language MyLanguage;
        FormMain MyMain;
        AppData MyAppData;

        public FormChangeLanguage(Language MyLanguage, FormMain MyMain, AppData MyAppData)
        {
            InitializeComponent();
            this.MyLanguage = MyLanguage;
            this.MyMain = MyMain;
            this.MyAppData = MyAppData;

            languages = Language.GetAvaliableLanguages(Constants.LANGUAGE_FOLDER);

            foreach (var language in languages)
            {
                cbxLanguage.Items.Add(language.NativeName);
            }

            cbxLanguage.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 index = cbxLanguage.SelectedIndex;
            lblLanguageCode.Text = languages[index].Name;
        }

        private void btnDefine_Click(object sender, EventArgs e)
        {
            String LangCode = languages[cbxLanguage.SelectedIndex].Name;
            String LangPath = Program.LangCodeToFolder(LangCode);
            this.MyMain.MyLanguage = File.Exists(LangPath) ? Language.GetLanguage(LangPath) : null;
            this.MyMain.InitializeLanguage();
            this.MyAppData.General.DefaultLanguage = LangCode;
            this.MyAppData.Save();

            if (chkExitOnSave.Checked)
            {
                this.Dispose();
            }
        }

        
    }
}
