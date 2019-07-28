using ProjectSparky.ViewModels;
using ProjectSparky.ViewModels.Settings;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ProjectSparky.Views.Settings
{
    public sealed partial class ScreenBrightness : Page
    {
        public ScreenBrightnessViewModel myScreenBrightnessVM { get; set; }

        public ScreenBrightness()
        {
            this.InitializeComponent();
            ViewModelLocator myVML = new ViewModelLocator();

            myScreenBrightnessVM = myVML.ScreenBrightnessVM;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myScreenBrightnessVM.LoadViewData();
        }
    }
}
