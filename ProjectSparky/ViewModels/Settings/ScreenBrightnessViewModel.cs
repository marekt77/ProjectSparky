using GalaSoft.MvvmLight;
using ProjectSparky.Tools;
using System;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace ProjectSparky.ViewModels.Settings
{
    public class ScreenBrightnessViewModel : ViewModelBase
    {
        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public ScreenBrightnessViewModel()
        {

        }

        public void LoadViewData()
        {
            var enableDim = localSettings.Values["dimNightTime"];
            var valNightStart = localSettings.Values["nightStart"];
            var valNigthEnd = localSettings.Values["nightEnd"];
            var tmpScreenBrightness = localSettings.Values["ScreenBrightness"];

            if(tmpScreenBrightness != null)
            {
                double sb;
                if(double.TryParse(tmpScreenBrightness.ToString(), out sb))
                {
                    ScreenBrightness = sb;
                }
            }

            if (valNightStart != null)
            {
                TimeSpan sNT;

                if(TimeSpan.TryParse(valNightStart.ToString(), out sNT))
                {
                    StartNightTime = sNT;
                }
                else
                {
                    StartNightTime = new TimeSpan(22, 00, 00);
                }
            }
            else
            {
                StartNightTime = new TimeSpan(22, 00, 00);
            }

            if (valNigthEnd != null)
            {
                TimeSpan eNT;

                if(TimeSpan.TryParse(valNigthEnd.ToString(), out eNT))
                {
                    EndNightTime = eNT;
                }
                else
                {
                    EndNightTime = new TimeSpan(07, 00, 00);
                }
            }
            else
            {
                EndNightTime = new TimeSpan(07, 00, 00);
            }

            if (enableDim != null)
            {
                ShowNightTimeSettings = bool.Parse(enableDim.ToString());   
            }
            else
            {
                ShowNightTimeSettings = false;
            }
            
            RaisePropertyChanged("ShowNightTimeSettings");
            RaisePropertyChanged("StartNightTime");
            RaisePropertyChanged("EndNightTime");
        }

        public void ScreenBrightness_Changed(object sender, RangeBaseValueChangedEventArgs e)
        {
            ScreenBrightness = e.NewValue;
        }

        public void ScreenBrightness_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            localSettings.Values["ScreenBrightness"] = ScreenBrightness;

            DeviceControls.SetBrightnessLevel(ScreenBrightness * .01);
        }

        public void StartNightTime_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            string time = e.NewTime.ToString();
            localSettings.Values["nightStart"] = e.NewTime.ToString();
        }

        public void EndNightTime_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            string time = e.NewTime.ToString();
            localSettings.Values["nightEnd"] = e.NewTime.ToString();
        }

        public void NightTimeEnabled_Toggle(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var tmpToggle = (ToggleSwitch)sender;
            
            //Upate this code
            if(tmpToggle.IsOn)
            {
                localSettings.Values["dimNightTime"] = true;
            }
            else
            {
                localSettings.Values["dimNightTime"] = false;
            }
        }

        private double _ScreenBrightness = 1;
        public double ScreenBrightness
        {
            get
            {
                return _ScreenBrightness;
            }
            set
            {
                _ScreenBrightness = value;
                RaisePropertyChanged("ScreenBrightness");
            }
        }

        private bool _ShowNightTimeSettings;
        public bool ShowNightTimeSettings
        {
            get
            {
                return _ShowNightTimeSettings;
            }
            set
            {
                _ShowNightTimeSettings = value;
                RaisePropertyChanged("ShowNightTimeSettings");
            }
        }

        private TimeSpan _StartNightTime;
        public TimeSpan StartNightTime
        {
            get
            {
                return _StartNightTime;
            }
            set
            {
                _StartNightTime = value;
            }
        }

        private TimeSpan _EndNightTime;
        public TimeSpan EndNightTime
        {
            get
            {
                return _EndNightTime;
            }
            set
            {
                _EndNightTime = value;
            }
        }
    }
}
