using System.Collections.Generic;

namespace VisualNowel
{
    public struct ProfileDialogs
    {
        public string active;
        public List<string> closed;
    }

    public struct Profile
    {
        public ProfileDialogs dialogs;
    }

    public class SaveSystem
    {
        private Profile _profile;

        public SaveSystem(Profile profile)
        {
            _profile = profile;
        }

        public void AddClosed(string dialogKey)
        {
            _profile.dialogs.closed.Add(dialogKey);
        }

        public void SetActie(string dialogKey)
        {
            _profile.dialogs.active = dialogKey;
        }

        public string GetActive()
        {
            return _profile.dialogs.active;
        }
    }
}
