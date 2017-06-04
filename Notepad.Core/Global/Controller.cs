using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Notepad.Core.Global
{
    public class Controller
    {
        
        public static void ApplyMenu(MenuStrip menuForm, Form form, Language lang)
        {
            foreach (ToolStripMenuItem menu in menuForm.Items)
            {
                SetLanguage(form, menu, lang);

                if (menu.HasDropDownItems)
                {
                    ApplyMenu(menu.DropDownItems, form, lang);
                }
            }
        }

        private static void ApplyMenu(ToolStripItemCollection toolStripItemCollection, Form form, Language lang)
        {
            foreach (ToolStripItem item in toolStripItemCollection)
            {
                SetLanguage(form, item, lang);
            }
        }

        private static IEnumerable<Control> GetAllTextBoxControls(Control container)
        {
            List<Control> controlList = new List<Control>();
            foreach (Control c in container.Controls)
            {
                controlList.Add(c);

                if (c.Controls != null)
                {
                   controlList.AddRange(GetAllTextBoxControls(c));
                }
            }
            return controlList;
        }

        public static void ApplyLanguage(Language lang, Form form)
        {
            var controls = GetAllTextBoxControls(form);

            foreach (Control item in controls)
            {
                if (typeof(MenuStrip) == item.GetType())
                {
                    ApplyMenu((MenuStrip)item, form, lang);
                }
            }
        }

        public static void SetLanguage(Form form, Control item, Language lang)
        {
            String KeyCode = String.Format("{0}.{1}", form.Name, item.Name);
            Key key = lang.GetKey(KeyCode);
            if (key != null)
            {
                item.Text = key.Translation;
            }
        }

        public static void SetLanguage(Form form, ToolStripMenuItem item, Language lang)
        {
            String KeyCode = String.Format("{0}.{1}", form.Name, item.Name);
            Key key = lang.GetKey(KeyCode);
            if (key != null)
            {
                item.Text = key.Translation;
            }
        }

        public static void SetLanguage(Form form, ToolStripItem item, Language lang)
        {
            String KeyCode = String.Format("{0}.{1}", form.Name, item.Name);
            Key key = lang.GetKey(KeyCode);
            if (key != null)
            {
                item.Text = key.Translation;
            }
        }

        public static IEnumerable<Control> a { get; set; }
    }
}
