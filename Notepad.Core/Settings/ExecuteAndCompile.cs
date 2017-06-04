using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Notepad.Core.Settings
{
    public class ExecuteAndCompile
    {
        public string Language { get; set; }
        public string Extension { get; set; }
        public string Compiler { get; set; }
        public string Arguments { get; set; }

        public void Run(String Path, Boolean Hidden = false)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = Constants.ToLocalPath(this.Compiler);
            startInfo.WindowStyle = Hidden? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal;
            startInfo.Arguments = this.Arguments;

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}