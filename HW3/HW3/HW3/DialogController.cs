using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace VisualNowel
{

    public struct Step
    {
        public string text;
        public string npc;
    }

    public struct Transform
    {
        public double x { get; set; } 
        public double y { get; set; } 
        public double width { get; set; }
        public double height { get; set; }
    }

    public class NPC
    {
        public string name;
        public string image;
        public Transform transform;
    }


    public struct OptionSelector
    {
        public string text { get; set; }
        public string dialog_trigger { get; set; }
    }

    public struct Options
    {
        public string text;
        public List<OptionSelector> selectors;
        public string npc;
    }

    public struct Dialog
    {
        public string background;
        public List<Step> steps;
        public Options options;
    }

    public struct GameData
    {
        public SortedDictionary<string, NPC> characters;
        public SortedDictionary<string, Dialog> dialogs;
    }

    public class DialogController
    {

        private SortedDictionary<string, NPC> _characters;
        private SortedDictionary<string, Dialog> _dialogs;
        private Dialog? _currentDialog = null;
        private int _currentStepIndex = 0;

        public SortedDictionary<string, Dialog> Dialogs => _dialogs;

        public DialogController(GameData gameData, string startDialog)
        {
            _characters = gameData.characters;
            _dialogs = gameData.dialogs;
            SetNextDialog(startDialog);
        }

        public void NextStep()
        {
            _currentStepIndex++;
        }

        public Step? CurrentStep()
        {
            if(_currentStepIndex < _currentDialog?.steps.Count)
            {
                return _currentDialog?.steps[_currentStepIndex];
            }
            return null;
        }

        public Options? GetOptions()
        {
            return _currentDialog?.options;
        }

        public void SetNextDialog(string dialogName)
        {
            if (_dialogs.TryGetValue(dialogName, out var nextDialog))
            {
                _currentDialog = nextDialog;
                _currentStepIndex = 0;
            }
        }

        public string GetBackground()
        {
            return _currentDialog?.background;
        }

        public NPC GetNPC(string characterKey)
        {
            if (_characters.TryGetValue(characterKey ?? "", out var character))
            {
                return character;
            }
            return null;
        }

        public SortedSet<string> GetNpcKeysForCurrentDialog()
        {
            return  new SortedSet<string>(_currentDialog?.steps.Select(x => x.npc).Concat(
                new [] { _currentDialog?.options.npc } ));
        }
    }
}
