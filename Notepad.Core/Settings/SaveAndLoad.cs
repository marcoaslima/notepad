using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notepad.Core.Settings
{
    public class SaveAndLoad
    {
        public int AlertWhenFileGet { get; set; }
        public bool AllowOnlyOneInstancePerFile { get; set; }
        public bool AlertWhenFileChanged { get; set; }
        public bool CreateBackup { get; set; }
        public bool AllowMultipleBackup { get; set; }
        public Autosave Autosave { get; set; }
    }
}
