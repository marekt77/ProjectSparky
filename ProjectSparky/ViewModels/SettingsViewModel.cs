using GalaSoft.MvvmLight;
using ProjectSparky.Interfaces;
using ProjectSparky.Views.Settings;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace ProjectSparky.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public SettingsViewModel(INavigationService tmpNavigationService)
        {
            _navigationService = tmpNavigationService;
        }

        public void nvSettings_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
            NavView_Navigate(item as NavigationViewItem, (Frame)sender.Content);
        }

        public void BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            _navigationService.GoBack();
        }

        private void NavView_Navigate(NavigationViewItem item, Frame targetFrame)
        {
            switch (item.Tag)
            {
                case "wifi":
                    _navigationService.Navigate(targetFrame, typeof(WiFiView));
                    break;

                case "background":
                    _navigationService.Navigate(targetFrame, typeof(BackgroundView));
                    break;

                case "alarms":
                    _navigationService.Navigate(targetFrame, typeof(AlarmsView));
                    break;

                case "quiettime":
                    _navigationService.Navigate(targetFrame, typeof(QuietTimeView));
                    break;

                case "screenbrightness":
                    _navigationService.Navigate(targetFrame, typeof(ScreenBrightness));
                    break;

                case "language":
                    _navigationService.Navigate(targetFrame, typeof(LanguageView));
                    break;

                case "location":
                    _navigationService.Navigate(targetFrame, typeof(LocationView));
                    break;
            }
        }
    }
}
