using System.Collections.Generic;

namespace VisualNowel
{

    public struct Step
    {
        public string text;
        public NPC npc;
    }

    public struct Transform
    {
        public float x { get; set; } 
        public float y { get; set; } 
        public float width { get; set; }
        public float height { get; set; }
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
        public string dialogTrigger { get; set; }
    }

    public struct Options
    {
        public string text;
        public List<OptionSelector> selectors;
        public NPC npc;
    }

    public struct Dialog
    {
        public string background;
        public List<Step> steps;
        public Options options;
    }

    public class DialogController
    {
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
                            npc = new NPC
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
                        new Step 
                        {
                            text = "Next Text",
                            npc = new NPC
                            {
                                name = "Босс",
                                image = "snake",
                                transform = new Transform
                                {
                                    x = 15,
                                    y = 400,
                                    width = 256,
                                    height = 256
                                }
                            }
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
                                dialogTrigger = "dialog2"
                            },
                            new OptionSelector
                            {
                                text = "Some Option Text",
                                dialogTrigger = "dialog3"
                            }
                        },
                        npc = new NPC
                            {
                                name = "Сэм",
                                image = "samuel",
                                transform = new Transform
                                {
                                    x = 45,
                                    y = 400,
                                    width = 256,
                                    height = 256
                                }
                            }

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
                                dialogTrigger = "dialog3"
                            },
                            new OptionSelector
                            {
                                text = "Good Option Text",
                                dialogTrigger = "dialog3"
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
    }
}
