using Newtonsoft.Json;
using Notepad.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notepad.Core.Settings
{
    public class AppData
    {
        public General General { get; set; }
        public Search Search { get; set; }
        public List<Favourite> Favoutires { get; set; }
        public List<Theme> Themes { get; set; }
        public SaveAndLoad SaveAndLoad { get; set; }
        public Developer Developer { get; set; }
        public String Path { get; set; }
        public String DefaultThemeName { get; set; }

        public Theme SearchThemeByName(String Name)
        {
            return Themes.SingleOrDefault(item => item.Name == Name);
        }

        public static AppData Get(String Path)
        {
            AppData temp = Newtonsoft.Json.JsonConvert.DeserializeObject<AppData>(Archive.ReadFileToString(Path));
            temp.Path = Path;
            return temp;
        }

        public static void SaveDefault()
        {

        }

        public void Save()
        {
           String json = JsonConvert.SerializeObject(this);
           Archive archive = new Archive(this.Path, json);
           Archive.Save(archive);
        }
    }
}
