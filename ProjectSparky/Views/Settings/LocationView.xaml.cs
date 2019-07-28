using ProjectSparky.ViewModels;
using ProjectSparky.ViewModels.Settings;
using Windows.UI.Xaml.Controls;

namespace ProjectSparky.Views.Settings
{
    public sealed partial class LocationView : Page
    {
        public LocationViewModel myLocationVM { get; set; }
        public LocationView()
        {
            this.InitializeComponent();
            ViewModelLocator myVML = new ViewModelLocator();

            myLocationVM = myVML.LocationVM;
        }
    }
}
