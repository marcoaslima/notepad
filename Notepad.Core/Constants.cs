using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Notepad.Core
{
    public class Constants
    {
        public static String APP_DATA
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
        }

        public static String DATA_JSON
        {
            get
            {
                return ToLocalPath(@"\data\appdata.json");
            }
        }

        public static String LANGUAGE_FOLDER
        {
            get
            {
                return ToLocalPath(@"\locales\");
            }
        }

        public static String LOCAL_PATH
        {
            get { return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location); }
        }

        public static String ToAppData(String folder = "")
        {
            return String.Concat(APP_DATA, @"\", folder);
        }

        public static String ToLocalPath(String folder = "")
        {
            return String.Concat(LOCAL_PATH, @"\", folder);
        }

        public static string LOG_PATH
        {
            get
            {
                return ToLocalPath(@"\logs");
            }
        }
    }
}
