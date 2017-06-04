using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Notepad.Core.Utils
{
    public class WebSite
    {
        public static Boolean Open(String Path)
        {
            try
            {
                Process.Start(Path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
