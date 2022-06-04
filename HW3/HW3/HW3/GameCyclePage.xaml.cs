using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VisualNowel;
using System.Linq;
using System.Collections.ObjectModel;

namespace HW3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameCyclePage : ContentPage
    {
        private class CharacterViewData
        {
            public NPC npc;
            public string image { get; set; }
            public Constraint x { get; set; }
            public Constraint y { get; set; }
            public Constraint width { get; set; }
            public Constraint height { get; set; }
            public double opacity { get; set; } 
        }

        private DialogController _controller;
        private ResourceLoader _resourceLoader;
        private SaveSystem _saveSystem;
        private ObservableCollection<CharacterViewData> _viewDatas;
        
        public GameCyclePage()
        {
            InitializeComponent();

            _resourceLoader = new ResourceLoader();
            _saveSystem = new SaveSystem(_resourceLoader.GetStartProfile());
            _controller = new DialogController(_resourceLoader.GetGameData(), _saveSystem.GetActive());
            
            currentStepText.Text = _controller.CurrentStep()?.text;
            background.Source = _controller.GetBackground();

            DrawNPC();
            SetActiveNPC(_controller.CurrentStep()?.npc);
            BindableLayout.SetItemsSource(charactersHolder, _viewDatas);
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            _controller.NextStep();
            var currentStep = _controller.CurrentStep();
            
            if (currentStep != null)
            {
                var currentNPC = currentStep.Value.npc;
                currentStepText.Text = currentStep.Value.text;
                SetActiveNPC(currentNPC);
            }
            else
            {
                var options = _controller.GetOptions();
                if (options != null)
                {
                    var currentNPC = options.Value.npc;
                    BindableLayout.SetItemsSource(optionSelector, options.Value.selectors);
                    currentStepText.Text = options.Value.text;
                    SetActiveNPC(currentNPC);
                }
            }
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            var dialogTrigger = (sender as Button)?.ClassId;
            _controller.SetNextDialog(dialogTrigger);

            currentStepText.Text = _controller.CurrentStep()?.text;
            BindableLayout.SetItemsSource(optionSelector, null);
            background.Source = _controller.GetBackground();

            DrawNPC();
            SetActiveNPC(_controller.CurrentStep()?.npc);
        }

        private void DrawNPC()
        {
            _viewDatas = new ObservableCollection<CharacterViewData>(_controller.GetNpcKeysForCurrentDialog().Select(x => _controller.GetNPC(x)).Where(x => x != null).Select(x => new CharacterViewData
            {
                npc = x,
                image = x.image,
                x = Constraint.Constant(x.transform.x),
                y = Constraint.Constant(x.transform.y),
                width = Constraint.Constant(x.transform.width),
                height = Constraint.Constant(x.transform.height),
                opacity = 0.6
            }).ToList()); ;

            // W/A
            BindableLayout.SetItemsSource(charactersHolder, _viewDatas);
        }

        private void SetActiveNPC(string npcKey)
        {
            currentNpcName.Text = "";
            var activeNPC = _controller.GetNPC(npcKey);
            if (activeNPC != null)
            {
                var activeNPCView = _viewDatas.First(x => x.npc == activeNPC);
                _viewDatas.Remove(activeNPCView);

                for (int i = 0; i < _viewDatas.Count; i++)
                {
                    _viewDatas[i].opacity = 0.6;
                }

                activeNPCView.opacity = 1;
                _viewDatas.Add(activeNPCView);

                currentNpcName.Text = activeNPC.name;

                // W/A
                _viewDatas = new ObservableCollection<CharacterViewData>(_viewDatas);
                BindableLayout.SetItemsSource(charactersHolder, _viewDatas);
            }
        }
    }
}