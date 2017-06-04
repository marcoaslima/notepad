using Notepad.Core;
using Notepad.Core.Global;
using Notepad.Core.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Notepad.View
{
    public partial class FormSettings : Form
    {
        private AppData MyAppData;
        private FormMain MyParent;

        public System.Globalization.CultureInfo[] languages { get; set; }

        public FormSettings(AppData Data, FormMain Parent, Language MyLanguage = null)
        {
            InitializeComponent();
            this.MyAppData = Data;
            this.MyParent = Parent;
            LoadContent();
        }

        private void LoadContent()
        {
            cbxThemes.Items.Clear();
            cbxLanguage.Items.Clear();
            cbxThemes.SelectedText = this.MyAppData.DefaultThemeName;

            foreach (Theme theme in MyAppData.Themes)
            {
                cbxThemes.Items.Add(theme.Name);
            }

            languages = Language.GetAvaliableLanguages(Constants.LANGUAGE_FOLDER);

            foreach (CultureInfo culture in languages)
            {
                cbxLanguage.Items.Add(culture.DisplayName);
            }


            chkAskBeforeExit.Checked = this.MyAppData.General.AskBeforeExit;
            chkExitF4.Checked = this.MyAppData.General.ExitAltF4;
            chkShowFullPathWindow.Checked = this.MyAppData.General.ShowCompletePath;
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbxThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MyParent.ApplyTheme(this.MyAppData.Themes[cbxThemes.SelectedIndex]);
            this.MyAppData.DefaultThemeName = this.MyAppData.Themes[cbxThemes.SelectedIndex].Name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.MyAppData.Save();
            this.MyParent.MyAppData = this.MyAppData;
            this.MyParent.InitializeSettings();
            //this.Dispose();
        }

        private void cbxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            String langCode = languages[cbxLanguage.SelectedIndex].Name;
            String LangPath = Program.LangCodeToFolder(langCode);
            MyAppData.General.DefaultLanguage = langCode;
            this.MyParent.MyLanguage = File.Exists(LangPath) ? Language.GetLanguage(LangPath) : null;            
            this.MyParent.InitializeLanguage();
        }

        private void chkExitF4_CheckedChanged(object sender, EventArgs e)
        {
            this.MyAppData.General.ExitAltF4 = chkExitF4.Checked;
        }

        private void chkAskBeforeExit_CheckedChanged(object sender, EventArgs e)
        {
            this.MyAppData.General.AskBeforeExit = chkAskBeforeExit.Checked;
        }

        private void chkShowFullPathWindow_CheckedChanged(object sender, EventArgs e)
        {
            this.MyAppData.General.ShowCompletePath = chkShowFullPathWindow.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        
    }
}
