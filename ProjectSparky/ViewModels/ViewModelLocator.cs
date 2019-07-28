using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using ProjectSparky.Interfaces;
using ProjectSparky.Services;
using ProjectSparky.ViewModels.Settings;

namespace ProjectSparky.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IWeatherService, WeatherService>();
            SimpleIoc.Default.Register<IBingDailyImageService, BingDailyImageService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            
            SimpleIoc.Default.Register<IAlarmService, AlarmService>();
            SimpleIoc.Default.Register<INoteService, NoteService>();
            SimpleIoc.Default.Register<IUserAccountService, UserAccountService>();

            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();

            //Setting ViewModels
            SimpleIoc.Default.Register<AlarmsViewModel>();
            SimpleIoc.Default.Register<BackgroundViewModel>();
            SimpleIoc.Default.Register<LanguageViewModel>();
            SimpleIoc.Default.Register<LocationViewModel>();
            SimpleIoc.Default.Register<QuietTimeViewModel>();
            SimpleIoc.Default.Register<WiFiViewModel>();
            SimpleIoc.Default.Register<ScreenBrightnessViewModel>();
        }
        
        public HomeViewModel HomeVM => ServiceLocator.Current.GetInstance<HomeViewModel>();
        public SettingsViewModel SettingsVM => ServiceLocator.Current.GetInstance<SettingsViewModel>();

        //Setting ViewModels
        public AlarmsViewModel AlarmsVM => ServiceLocator.Current.GetInstance<AlarmsViewModel>();
        public BackgroundViewModel BackgroundVM => ServiceLocator.Current.GetInstance<BackgroundViewModel>();
        public LanguageViewModel LanguageVM => ServiceLocator.Current.GetInstance<LanguageViewModel>();
        public LocationViewModel LocationVM => ServiceLocator.Current.GetInstance<LocationViewModel>();
        public QuietTimeViewModel QuietTimeVM => ServiceLocator.Current.GetInstance<QuietTimeViewModel>();
        public WiFiViewModel WiFiVM => ServiceLocator.Current.GetInstance<WiFiViewModel>();
        public ScreenBrightnessViewModel ScreenBrightnessVM => ServiceLocator.Current.GetInstance<ScreenBrightnessViewModel>();
    }
}
