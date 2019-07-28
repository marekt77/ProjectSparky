using ProjectSparky.ViewModels;
using ProjectSparky.ViewModels.Settings;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ProjectSparky.Views.Settings
{
    public sealed partial class AlarmsView : Page
    {
        public AlarmsViewModel myAlarmVM { get; set; }

        public AlarmsView()
        {
            this.InitializeComponent();
            ViewModelLocator myVML = new ViewModelLocator();

            myAlarmVM = myVML.AlarmsVM;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myAlarmVM.ClearViewData();
            myAlarmVM.LoadViewData();
        }
    }
}
