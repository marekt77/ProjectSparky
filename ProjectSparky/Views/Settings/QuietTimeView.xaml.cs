using ProjectSparky.ViewModels;
using ProjectSparky.ViewModels.Settings;
using Windows.UI.Xaml.Controls;

namespace ProjectSparky.Views.Settings
{
    public sealed partial class QuietTimeView : Page
    {
        QuietTimeViewModel myQuietTimeVM { get; set; }

        public QuietTimeView()
        {
            this.InitializeComponent();
            ViewModelLocator myVML = new ViewModelLocator();

            myQuietTimeVM = myVML.QuietTimeVM;
        }
    }
}
