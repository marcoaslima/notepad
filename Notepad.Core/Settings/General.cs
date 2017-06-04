using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notepad.Core.Settings
{
    public class General
    {
        public bool ExitAltF4 { get; set; }
        public bool AskBeforeExit { get; set; }
        public bool ShowCompletePath { get; set; }
        public bool SearchForUpdates { get; set; }
        public bool DataPattern { get; set; }
        public bool SaveLogFile { get; set; }
        public bool SendErrorReport { get; set; }
        public string DefaultLanguage { get; set; }
    }
}
