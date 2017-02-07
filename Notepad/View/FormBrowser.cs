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
    public partial class FormBrowser : Form
    {
        private FormMain formMain;

        public FormBrowser()
        {
            InitializeComponent();
        }

        public FormBrowser(FormMain formMain)
        {
            InitializeComponent();
            this.formMain = formMain;
        }

        private void FormBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.formMain.browserNotShowed = true;
        }
    }
}
