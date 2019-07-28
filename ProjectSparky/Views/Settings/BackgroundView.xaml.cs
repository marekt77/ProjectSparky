using ProjectSparky.ViewModels;
using ProjectSparky.ViewModels.Settings;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ProjectSparky.Views.Settings
{
    public sealed partial class BackgroundView : Page
    {
        public BackgroundViewModel myBackgroundVM { get; set; }

        public BackgroundView()
        {
            this.InitializeComponent();
            ViewModelLocator myVML = new ViewModelLocator();

            myBackgroundVM = myVML.BackgroundVM;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myBackgroundVM.ClearViewData();
            myBackgroundVM.LoadViewData();
        }
    }
}
