using ProjectSparky.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ProjectSparky.Views
{
    public sealed partial class HomeView : Page
    {
        public HomeViewModel myHomeVM { get; set; }

        public HomeView()
        {
            this.InitializeComponent();
            ViewModelLocator myVML = new ViewModelLocator();

            myHomeVM = myVML.HomeVM;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myHomeVM.UpdateViewData();
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            myHomeVM.UpdateViewData();
            base.OnNavigatedFrom(e);
        }
    }
}
