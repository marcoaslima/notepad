using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notepad.Core.Settings
{
    public class Autosave
    {
        public bool SaveAfter5Minutes { get; set; }
        public bool SaveInOriginalFile { get; set; }
        public bool AllowMultipleInstance { get; set; }
    }
}
