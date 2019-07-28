using ProjectSparky.ViewModels;
using Windows.UI.Xaml.Controls;

namespace ProjectSparky.Views
{
    public sealed partial class SettingsView : Page
    {
        public SettingsViewModel mySettingVM { get; set; }

        public SettingsView()
        {
            this.InitializeComponent();
            ViewModelLocator myVML = new ViewModelLocator();

            mySettingVM = myVML.SettingsVM;
        }
    }
 }
