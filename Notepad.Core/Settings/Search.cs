using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notepad.Core.Settings
{
    public class Search
    {
        public bool DifferentiateCase { get; set; }
        public bool MarcOnlyEntireWords { get; set; }
        public bool AllowRegex { get; set; }
        public bool HideSearchBarAfterSearch { get; set; }
        public bool HideSearchBarOnlySuccedSearch { get; set; }
        public bool SaveSearchHistory { get; set; }
        public bool MultThreadingEnabled { get; set; }
    }
}
