using GalaSoft.MvvmLight;
using ProjectSparky.Interfaces;
using ProjectSparky.Model.BingDailyImage;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ProjectSparky.ViewModels.Settings
{
    public class BackgroundViewModel : ViewModelBase
    {
        private ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
        private readonly IBingDailyImageService _bingDailyImageService;

        public BackgroundViewModel(IBingDailyImageService tmpBingDailyImageService)
        {
            _bingDailyImageService = tmpBingDailyImageService;
        }

        public void LoadViewData()
        {
            GetDefaultImages();
            PopulateBackgroundSources();
            ReadCurrentBackgroundImageSetting();
        }

        public void ClearViewData()
        {
            DefaultAppImages = null;
            ShowLocalImageGallery = Visibility.Collapsed;
        }

        private void ReadCurrentBackgroundImageSetting()
        {
            Object value = _localSettings.Values["BackgroundImageSource"];

            if(value != null)
            {
                int tmpSelected = 0;
                string[] sourceType = value.ToString().Split(';');

                switch(sourceType[0])
                {
                    case "BING":
                        tmpSelected = BackgroundImageSources.IndexOf(new Tuple<string, string>("Bing Daily Images", "BING"));
                        GetBingPhoto();
                        break;
                    case "DEFAULT":
                        tmpSelected = BackgroundImageSources.IndexOf(new Tuple<string, string>("Choose a Photo", "DEFAULT"));
                        CurrentBackgroundImage = new BitmapImage(new Uri("ms-appx:///Assets/Backgrounds/" + sourceType[1]));
                        break;
                    case "ONEDRIVE":
                        tmpSelected = BackgroundImageSources.IndexOf(new Tuple<string, string>("OneDrive Photos", "ONEDRIVE"));
                        break;
                }
                SelectedSourceIndex = tmpSelected;
            }
        }

        private void PopulateBackgroundSources()
        {
            BackgroundImageSources = new ObservableCollection<Tuple<string, string>>();

            //Add Bing Daily Image
            BackgroundImageSources.Add(new Tuple<string, string>("Bing Daily Images", "BING"));

            //Add Default Images
            BackgroundImageSources.Add(new Tuple<string, string>("Choose a Photo", "DEFAULT"));

            //Add OneDrive Photos
            BackgroundImageSources.Add(new Tuple<string, string>("OneDrive Photos", "ONEDRIVE"));
        }

        private async void GetDefaultImages()
        {
            DefaultAppImages = new ObservableCollection<DefaultImage>();

            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder Assets = await appInstalledFolder.GetFolderAsync("Assets");
            StorageFolder Backgrounds = await Assets.GetFolderAsync("Backgrounds");
            var files = await Backgrounds.GetFilesAsync();

            foreach(StorageFile tmpFile in files)
            {
                DefaultAppImages.Add(new DefaultImage
                {
                    Source = new BitmapImage(new Uri("ms-appx:///Assets/Backgrounds/" + tmpFile.Name)),
                    ImageName = tmpFile.Name
                });
            }
        }

        private Visibility _ShowLocalImageGallery = Visibility.Collapsed;
        public Visibility ShowLocalImageGallery
        {
            get
            {
                return _ShowLocalImageGallery;
            }
            set
            {
                _ShowLocalImageGallery = value;
                RaisePropertyChanged("ShowLocalImageGallery");
            }
        }

        private int _selectedSourceIndex;
        public int SelectedSourceIndex
        {
            get
            {
                return _selectedSourceIndex;
            }
            set
            {
                _selectedSourceIndex = value;
                RaisePropertyChanged("SelectedSourceIndex");
            }
        }

        private ImageSource _CurrentBackgroundImage;
        public ImageSource CurrentBackgroundImage
        {
            get
            {
                return _CurrentBackgroundImage;
            }
            set
            {
                _CurrentBackgroundImage = value;
                RaisePropertyChanged("CurrentBackgroundImage");
            }
        }

        private ObservableCollection<DefaultImage> _DefaultAppImages;
        public ObservableCollection<DefaultImage> DefaultAppImages
        {
            get
            {
                return _DefaultAppImages;
            }
            set
            {
                _DefaultAppImages = value;
                RaisePropertyChanged("DefaultAppImages");
            }
        }

        private ObservableCollection<Tuple<string, string>> _BackgroundImageSources;
        public ObservableCollection<Tuple<string, string>> BackgroundImageSources
        {
            get
            {
                return _BackgroundImageSources;
            }
            set
            {
                _BackgroundImageSources = value;
                RaisePropertyChanged("BackgroundImageSources");
            }
        }

        public void SelectBackgroundSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearSelections();
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (BackgroundImageSources != null)
            {
                switch(((Tuple<string, string>)e.AddedItems[0]).Item2)
                {
                    case "BING":
                        localSettings.Values["BackgroundImageSource"] = "BING;";
                        GetBingPhoto();
                        break;
                    case "DEFAULT":
                        ShowLocalImageGallery = Visibility.Visible;
                        break;
                    case "ONEDRIVE":
                        localSettings.Values["BackgroundImageSource"] = "ONEDRIVE;";
                        break;
                }
            }
        }

        public void DefaultPictures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DefaultImage selectedImage = (DefaultImage)e.AddedItems[0];

                _localSettings.Values["BackgroundImageSource"] = "DEFAULT;" + selectedImage.ImageName;
                App.CurrentAppData.BackgoundImage = new Model.BackgroundImage(new BitmapImage(new Uri("ms-appx:///Assets/Backgrounds/" + selectedImage.ImageName)), DateTime.Now);
                CurrentBackgroundImage = new BitmapImage(new Uri("ms-appx:///Assets/Backgrounds/" + selectedImage.ImageName));
            }
        }

        private void clearSelections()
        {
            ShowLocalImageGallery = Visibility.Collapsed;
        }

        private async void GetBingPhoto()
        {
            DailyImage TodaysImage = new DailyImage();
            try
            {
                TodaysImage = await _bingDailyImageService.GetTodaysImage();
                CurrentBackgroundImage = TodaysImage.Image;
                App.CurrentAppData.BackgoundImage.Image = TodaysImage.Image;
                App.CurrentAppData.BackgoundImage.ImageDate = DateTime.Now;
            }
            catch(Exception ex)
            {

            }
        }
    }

    public class DefaultImage
    {
        public BitmapImage Source { get; set; }
        public string ImageName { get; set; }
    }
}
