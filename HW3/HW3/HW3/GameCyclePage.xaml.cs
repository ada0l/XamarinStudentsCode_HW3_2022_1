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
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            _controller.NextStep();
            currentStepText.Text = _controller.CurrentStep()?.text;
        }
    }
}