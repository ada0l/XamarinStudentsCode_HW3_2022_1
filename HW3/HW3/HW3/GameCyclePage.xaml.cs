using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VisualNowel;

namespace HW3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameCyclePage : ContentPage
    {
        private DialogController _controller;
        
        public GameCyclePage()
        {
            InitializeComponent();
            
            _controller = new DialogController();
            currentStepText.Text = _controller.CurrentStep()?.text;
            background.Source = _controller.GetBackground();

            var startNPC = _controller.CurrentStep()?.npc;
            DrawNPC(startNPC);
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            _controller.NextStep();
            var currentStep = _controller.CurrentStep();
            
            if (currentStep != null)
            {
                var currentNPC = currentStep.Value.npc;
                currentStepText.Text = currentStep.Value.text;
                DrawNPC(currentNPC);
            }
            else
            {
                var options = _controller.GetOptions();
                var currentNPC = options.Value.npc;
                if (options != null)
                {
                    BindableLayout.SetItemsSource(optionSelector, options.Value.selectors);
                    currentStepText.Text = options.Value.text;
                    DrawNPC(currentNPC);
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

            var npc = _controller.CurrentStep()?.npc;
            DrawNPC(npc);
        }

        private void DrawNPC(string npcID)
        {
            var npc = _controller.GetNPC(npcID);
            if(npc == null)
            {
                CleanNPCView();
                return;
            }

            currentNpcName.Text = npc.Value.name;
            npcImage.Source = npc.Value.image;

            var transform = npc.Value.transform;

            RelativeLayout.SetXConstraint(npcImage, Constraint.Constant(transform.x));
            RelativeLayout.SetYConstraint(npcImage, Constraint.Constant(transform.y));
            RelativeLayout.SetWidthConstraint(npcImage, Constraint.Constant(transform.width));
            RelativeLayout.SetHeightConstraint(npcImage, Constraint.Constant(transform.height));
        }

        private void CleanNPCView()
        {
            npcImage.Source = "";
            currentNpcName.Text = "";
        }
    }
}