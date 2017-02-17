using Notepad.Core;
using Notepad.Core.Global;
using Notepad.Core.Settings;
using Notepad.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Notepad
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!File.Exists(Constants.DATA_JSON))
            {
                AppData.SaveDefault();
            }

            AppData MyAppData = AppData.Get(Constants.DATA_JSON);
            if (MyAppData.General.DefaultLanguage.Equals(""))
            {
                MyAppData.General.DefaultLanguage = Language.GetDefault();
            }

            String LangPath = LangCodeToFolder(MyAppData.General.DefaultLanguage);
            Language MyLanguage = File.Exists(LangPath)? Language.GetLanguage(LangPath) : null;

            FormMain main = new FormMain(MyAppData, MyLanguage);

            if (args.Count() > 0)
            {
                if (File.Exists(args[0]))
                {
                    main.Read(args[0]);
                }
            }
            Application.Run(main);
        }

        public static String LangCodeToFolder(String LangCode)
        {
           return String.Format(@"{0}\{1}.json", Constants.LANGUAGE_FOLDER, LangCode);
        }
    }
}
