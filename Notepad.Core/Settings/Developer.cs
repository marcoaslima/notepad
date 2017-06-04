using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notepad.Core.Settings
{
    public class Developer
    {
        public bool PreviewHtmlWebBrowser { get; set; }
        public bool ShowWebBrowserAnotherWindow { get; set; }
        public bool RunHtmlOpenWebBrowser { get; set; }
        public List<ExecuteAndCompile> ExecuteAndCompile { get; set; }

        public ExecuteAndCompile GetExecuteAndCompile(String Extension)
        {
            return ExecuteAndCompile.SingleOrDefault(item => item.Extension == Extension);
        }
    }
}
