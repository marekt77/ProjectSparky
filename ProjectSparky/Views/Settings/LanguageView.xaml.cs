using ProjectSparky.ViewModels;
using ProjectSparky.ViewModels.Settings;
using Windows.UI.Xaml.Controls;

namespace ProjectSparky.Views.Settings
{
    public sealed partial class LanguageView : Page
    {
        public LanguageViewModel myLanguageVM { get; set; }

        public LanguageView()
        {
            this.InitializeComponent();
            ViewModelLocator myVML = new ViewModelLocator();

            myLanguageVM = myVML.LanguageVM;
        }
    }
}
