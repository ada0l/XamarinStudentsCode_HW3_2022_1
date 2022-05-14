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
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            _controller.NextStep();
            var currentStep = _controller.CurrentStep();
            
            if (currentStep != null)
            {
                currentStepText.Text = currentStep.Value.text;
            }
            else
            {
                var options = _controller.GetOptions();
                if(options != null)
                {
                    BindableLayout.SetItemsSource(optionSelector, options.Value.selectors);
                    currentStepText.Text = options.Value.text;
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
        }
    }
}