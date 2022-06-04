using System.Collections.Generic;
using Xamarin.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;

namespace VisualNowel
{

    public class ResourceLoader
    {
        const string dialogsPath = "HW3.Assets.Dialogs";
        const string profilePath = "HW3.Assets.startProfile.json";

        public GameData GetGameData()
        {
            return new GameData()
            {
                characters = LoadCharacters(),
                dialogs = LoadDialogs()
            };
        }

        public Profile GetStartProfile()
        {
            var profileJSON = LoadFile(profilePath);
            var profile = JsonConvert.DeserializeObject<Profile>(profileJSON);
            return profile;
        }

        private SortedDictionary<string, Dialog> LoadDialogs()
        {
            var dialogs = Application.Current.GetType().Assembly.GetManifestResourceNames().Where(x => x.Contains(dialogsPath));
            var dialogDictionary = new SortedDictionary<string, Dialog>();

            foreach (var dialogPath in dialogs)
            {
                var dialogKey = Regex.Match(dialogPath, $"{dialogsPath}\\.(.*)\\.json").Groups[1].ToString();
                var dialogJSON = LoadFile(dialogPath);
                var dialog = JsonConvert.DeserializeObject<Dialog>(dialogJSON);
                dialogDictionary[dialogKey] = dialog;
            }

            return dialogDictionary;
        }

        private SortedDictionary<string, NPC> LoadCharacters()
        {
            return JsonConvert.DeserializeObject<SortedDictionary<string, NPC>>(
                LoadFile("HW3.Assets.npc.json"));
        }

        private string LoadFile(string filename)
        {
            var stream = Application.Current.GetType().Assembly.GetManifestResourceStream(filename);
            var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            return json;
        }
    }
}
