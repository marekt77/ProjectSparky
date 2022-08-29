using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectSparky.Interfaces;
using ProjectSparky.Model.BingDailyImage;
using ProjectSparky.Model.Weather;
using ProjectSparky.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ProjectSparky.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IBingDailyImageService _bingDailyImageService;
        private readonly IWeatherService _weatherService;
        private readonly INavigationService _navigationService;
        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        private DispatcherTimer viewRefresh;

        public HomeViewModel(IBingDailyImageService tmpBingDailyImageService, IWeatherService tmpWeatherService, INavigationService tmpNavigationService)
        {
            App.AppClock.TimeArrived += new Action<DateTime>(clock_TimeArrived);
            _bingDailyImageService = tmpBingDailyImageService;
            _weatherService = tmpWeatherService;
            _navigationService = tmpNavigationService;
            viewRefresh = new DispatcherTimer();
            viewRefresh.Tick += viewRefresh_Tick;
            viewRefresh.Interval = new TimeSpan(0, 10, 0);
            UpdateHomeScreenBackground();
        }

        public void UpdateViewData()
        {
            _currentCity = App.CurrentAppData.City;
            
            UpdateHomeScreenBackground();
            UpdateWeather();
            viewRefresh.Start();
            LastUpdate = DateTime.Now.ToString("MMM dd hh:mm:ss");
            SetLocationChangeVisbility();
        }

        [Conditional("DEBUG")]
        private void SetLocationChangeVisbility()
        {
            IsDebug = Visibility.Visible;
        }

        public void ClearViewData()
        {
            viewRefresh.Stop();
        }

        private void viewRefresh_Tick(object sender, object e)
        {
            viewRefresh.Stop();
            UpdateViewData();
        }

        private async void UpdateHomeScreenBackground()
        {
            Object value = localSettings.Values["BackgroundImageSource"];
            //ImageSource tmpBackgroundImage = null;

            if (App.CurrentAppData.BackgoundImage == null || DateTime.Now.CompareTo(App.CurrentAppData.BackgoundImage.ImageDate.Date) < 0 )
            {
                if (value != null)
                {
                    string[] sourceType = value.ToString().Split(';');

                    switch (sourceType[0])
                    {
                        case "BING":
                            //tmpBackgroundImage = await GetTodayBingImage();
                            App.CurrentAppData.BackgoundImage = new Model.BackgroundImage(await GetTodayBingImage(), DateTime.Now);
                            break;

                        case "DEFAULT":
                            App.CurrentAppData.BackgoundImage = new Model.BackgroundImage(new BitmapImage(new Uri("ms-appx:///Assets/Backgrounds/" + sourceType[1])), DateTime.Now);
                            break;

                        case "ONEDRIVE":
                            break;

                        default:
                            App.CurrentAppData.BackgoundImage = new Model.BackgroundImage(new BitmapImage(new Uri("ms-appx:///Assets/Backgrounds/DevBackground.png")), DateTime.Now);
                            break;
                    }
                }
                else
                {
                    App.CurrentAppData.BackgoundImage = new Model.BackgroundImage(new BitmapImage(new Uri("ms-appx:///Assets/Backgrounds/DevBackground.png")), DateTime.Now);
                }
            }

            BackgroundImage = App.CurrentAppData.BackgoundImage.Image;
        }

        private async void UpdateWeather()
        {
            try
            {
                WeatherForecast _CurrentWeather = await _weatherService.GetCurrentForecast(App.CurrentAppData.owCityID, false);

                CurrentTemp = Convert.ToInt16(_CurrentWeather.CurrentTemp).ToString() + "F";
                CurrentConditionIcon = new BitmapImage(new Uri("ms-appx:///Assets/Weather/" + Common.WeatherTools.ConditionCode_to_WeatherIcon(_CurrentWeather.DayCondition) + "_104px.png"));
            }
            catch (Exception ex)
            {

            }
        }

        private void clock_TimeArrived(DateTime time)
        {
            if (App.CurrentAppData.UseMetric)
            {
                CurrentTime = time.ToString("HH:mm:ss");
            }
            else
            {
                CurrentTime = time.ToString("h:mm:ss tt");
            }

            CurrentDate = time.ToString("MMM dd yyyy");
        }

        private async Task<ImageSource> GetTodayBingImage()
        {
            DailyImage TodaysImage = new DailyImage();
            ImageSource tmpBackgroundImage;

            try
            {
                TodaysImage = await _bingDailyImageService.GetTodaysImage();
                tmpBackgroundImage = TodaysImage.Image;
            }
            catch (Exception ex)
            {
                //Log the error 
                //set background image to Dev.
                tmpBackgroundImage = new BitmapImage(new Uri("ms-appx:///Assets/Backgrounds/DevBackground.png"));
            }

            return tmpBackgroundImage;
        }

        private string _currentCity;
        public string CurrentCity
        {
            get
            {
                return _currentCity;
            }
            set
            {
                _currentCity = value;
                RaisePropertyChanged("CurrentCity");
            }
        }

        private string _CurrentTime;
        public string CurrentTime
        {
            get
            {
                return _CurrentTime;
            }
            set
            {
                _CurrentTime = value;
                RaisePropertyChanged("CurrentTime");
            }
        }

        private string _CurrentDate;
        public string CurrentDate
        {
            get
            {
                return _CurrentDate;
            }
            set
            {
                _CurrentDate = value;
                RaisePropertyChanged("CurrentDate");
            }
        }

        private ImageSource _BackgroundImage;
        public ImageSource BackgroundImage
        {
            get
            {
                return _BackgroundImage;
            }
            set
            {
                _BackgroundImage = value;
                RaisePropertyChanged("BackgroundImage");
            }
        }

        private ImageSource _currentConditionIcon;
        public ImageSource CurrentConditionIcon
        {
            get
            {
                return _currentConditionIcon;
            }
            set
            {
                _currentConditionIcon = value;
                RaisePropertyChanged("CurrentConditionIcon");
            }
        }

        private string _currentTemp;
        public string CurrentTemp
        {
            get
            {
                return _currentTemp;
            }
            set
            {
                _currentTemp = value;
                RaisePropertyChanged("CurrentTemp");
            }
        }

        private ICommand _settingsButtonCommand;
        public ICommand SettingsButtonCommand
        {
            get
            {
                return _settingsButtonCommand ??
                    new RelayCommand(() =>
                    {
                        _navigationService.Navigate(typeof(SettingsView));
                    });
            }
        }

        private string _lastUpdate;
        public string LastUpdate
        {
            get
            {
                return _lastUpdate;
            }
            set
            {
                _lastUpdate = value;
                RaisePropertyChanged("LastUpdate");
            }
        }

        private Visibility _IsDebug = Visibility.Collapsed;
        public Visibility IsDebug
        {
            get
            {
                return _IsDebug;
            }
            set
            {
                _IsDebug = value;
                RaisePropertyChanged("IsDebug");
            }
        }
    }
}
