using System.Collections.Generic;

namespace VisualNowel
{

    public struct Step
    {
        public string text;
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
    }

    public struct Dialog
    {
        public List<Step> steps;
        public Options options;
        public string background;
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
                        new Step { text = "Some Text" },
                        new Step { text = "Next Text" }
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
