using ProjectSparky.ViewModels;
using ProjectSparky.ViewModels.Settings;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ProjectSparky.Views.Settings
{
    public sealed partial class WiFiView : Page
    {
        public WiFiViewModel myWiFiVM { get; set; }

        public WiFiView()
        {
            this.InitializeComponent();
            ViewModelLocator myVML = new ViewModelLocator();

            myWiFiVM = myVML.WiFiVM;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myWiFiVM.ClearViewData();
            myWiFiVM.LoadViewData();
        }
    }
}
