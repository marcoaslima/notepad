namespace Notepad.View
{
    partial class FormChangeLanguage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChangeLanguage));
            this.cbxLanguage = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLanguageCode = new System.Windows.Forms.Label();
            this.btnDefine = new System.Windows.Forms.Button();
            this.chkExitOnSave = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxLanguage
            // 
            this.cbxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLanguage.FormattingEnabled = true;
            this.cbxLanguage.Location = new System.Drawing.Point(7, 31);
            this.cbxLanguage.Name = "cbxLanguage";
            this.cbxLanguage.Size = new System.Drawing.Size(232, 21);
            this.cbxLanguage.TabIndex = 0;
            this.cbxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblLanguageCode);
            this.groupBox1.Controls.Add(this.cbxLanguage);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 82);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Idioma";
            // 
            // lblLanguageCode
            // 
            this.lblLanguageCode.AutoSize = true;
            this.lblLanguageCode.Location = new System.Drawing.Point(201, 55);
            this.lblLanguageCode.Name = "lblLanguageCode";
            this.lblLanguageCode.Size = new System.Drawing.Size(38, 13);
            this.lblLanguageCode.TabIndex = 1;
            this.lblLanguageCode.Text = "XX-XX";
            // 
            // btnDefine
            // 
            this.btnDefine.Location = new System.Drawing.Point(190, 100);
            this.btnDefine.Name = "btnDefine";
            this.btnDefine.Size = new System.Drawing.Size(75, 23);
            this.btnDefine.TabIndex = 2;
            this.btnDefine.Text = "Definir";
            this.btnDefine.UseVisualStyleBackColor = true;
            this.btnDefine.Click += new System.EventHandler(this.btnDefine_Click);
            // 
            // chkExitOnSave
            // 
            this.chkExitOnSave.AutoSize = true;
            this.chkExitOnSave.Checked = true;
            this.chkExitOnSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExitOnSave.Location = new System.Drawing.Point(12, 101);
            this.chkExitOnSave.Name = "chkExitOnSave";
            this.chkExitOnSave.Size = new System.Drawing.Size(90, 17);
            this.chkExitOnSave.TabIndex = 3;
            this.chkExitOnSave.Text = "Sair ao salvar";
            this.chkExitOnSave.UseVisualStyleBackColor = true;
            // 
            // FormChangeLanguage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 134);
            this.Controls.Add(this.chkExitOnSave);
            this.Controls.Add(this.btnDefine);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChangeLanguage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bloco de notas";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxLanguage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDefine;
        private System.Windows.Forms.Label lblLanguageCode;
        private System.Windows.Forms.CheckBox chkExitOnSave;
    }
}