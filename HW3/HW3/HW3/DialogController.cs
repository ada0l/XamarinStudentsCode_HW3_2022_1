using System.Collections.Generic;

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

    public struct NPC
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

    public class DialogController
    {
        private SortedDictionary<string, NPC> _characters = new SortedDictionary<string, NPC>()
        {
            {
                "samuel_left",
                new NPC
                {
                    name = "Сэм",
                    image = "samuel",
                    transform = new Transform
                    {
                        x = 90,
                        y = 400,
                        width = 256,
                        height = 256
                    }
                }
            },
            {
                "snake_left",
                new NPC
                {
                    name = "Босс",
                    image = "snake",
                    transform = new Transform
                    {
                        x = 90,
                        y = 400,
                        width = 256,
                        height = 256
                    }
                }
            },
        };

        private SortedDictionary<string, Dialog> _dialogs = new SortedDictionary<string, Dialog>()
        {
            {
                "dialog1",
                new Dialog {
                    steps = new List<Step>
                    {
                        new Step
                        {
                            text = "Some Text",
                            npc = "samuel_left"
                        },
                        new Step 
                        {
                            text = "Next Text",
                            npc = "snake_left"
                        }
                    },
                    options = new Options
                    {
                        text = "Some Options",
                        selectors = new List<OptionSelector>
                        {
                            new OptionSelector
                            {
                                text = "Some Option Text",
                                dialog_trigger = "dialog2"
                            },
                            new OptionSelector
                            {
                                text = "Some Option Text",
                                dialog_trigger = "dialog3"
                            }
                        },
                        npc = "samuel_left"

                    },
                    background = "road"
                }
            },
            {
                "dialog2",
                new Dialog {
                    steps = new List<Step>
                    {
                        new Step { text = "Good Text" },
                        new Step { text = "Bad Text" }
                    },
                    options = new Options
                    {
                        text = "Good Options",
                        selectors = new List<OptionSelector>
                        {
                            new OptionSelector
                            {
                                text = "Good Option Text",
                                dialog_trigger = "dialog3"
                            },
                            new OptionSelector
                            {
                                text = "Good Option Text",
                                dialog_trigger = "dialog3"
                            }
                        }
                    },
                    background = "gate"
                }
            },
            {
                "dialog3",
                new Dialog {
                    steps = new List<Step>
                    {
                        new Step { text = "Good Text" },
                        new Step { text = "Bad Text" }
                    },
                    background = "square"
                }
            }
        };
        private Dialog? _currentDialog = null;
        private int _currentStepIndex = 0;

        public SortedDictionary<string, Dialog> Dialogs => _dialogs;

        public DialogController()
        {
            _currentDialog = _dialogs["dialog1"];
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

        public NPC? GetNPC(string characterKey)
        {
            if (_characters.TryGetValue(characterKey ?? "", out var character))
            {
                return character;
            }
            return null;
        }
    }
}
