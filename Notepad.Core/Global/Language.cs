using Notepad.Core.IO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Notepad.Core.Global
{
    public class Language
    {
        public string LangCode { get; set; }
        public string LangName { get; set; }
        public List<Key> Keys { get; set; }
        public List<Message> Messages { get; set; }

        public static String GetDefault()
        {
            CultureInfo ci = CultureInfo.InstalledUICulture;
            return ci.CompareInfo.Name;
        }

        public static CultureInfo[] GetAvaliableLanguages(String LanguageFolder)
        {
           String[] langs = Directory.GetFiles(LanguageFolder);
           List<CultureInfo> cultures = new List<CultureInfo>();

            foreach (String lang in langs)
            {
                String cultureName = Path.GetFileNameWithoutExtension(lang);
                try
                {
                    cultures.Add(new CultureInfo(cultureName));
                }
                catch (Exception ex)
                {

                }
            }
            return cultures.ToArray();
        }

        public static Language GetLanguage(String FilePath)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Language>(Archive.ReadFileToString(FilePath));
        }

        public Key GetKey(String Component)
        {
            return Keys.SingleOrDefault(item => item.Component == Component);
        }

        public Message GetMessage(String Id)
        {
            return Messages.SingleOrDefault(item => item.Id == Id);
        }

        public String SetComponentLanguage(String FormName, String ComponentName)
        {
            String KeyCode = String.Format("{0}.{1}", FormName, ComponentName);
            
            Key key = GetKey(KeyCode);
            if (key != null)
            {
                return key.Translation;
            }
            else
            {
                return ComponentName;
            }
        }


        
    }
}
