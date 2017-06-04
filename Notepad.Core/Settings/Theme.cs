using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notepad.Core.Settings
{
    public class Theme
    {
        public string Name { get; set; }
        public string EditorBackColor { get; set; }
        public string EditorFontColor { get; set; }
        public string MenuBackColor { get; set; }
        public string MenuFontColor { get; set; }

        public static Theme Dark
        {
            get
            {
                Theme theme = new Theme();
                theme.Name = "Dark";
                theme.MenuBackColor = "#323232";
                theme.MenuFontColor = "#ffffff";
                theme.EditorFontColor = "#ffff";
                theme.EditorBackColor = "#323232";
                return theme;
            }
        }
    }
}
