using ProjectSparky.Tools;
using System;
using Windows.Storage;
using Windows.UI.Xaml;

namespace ProjectSparky.Model
{
    public class ClockModel
    {
        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        DispatcherTimer myTimer = new DispatcherTimer();
        
        private DateTime _Time;

        public event Action<DateTime> TimeArrived;

        public ClockModel()
        {
            myTimer.Tick += Timer_Tick;
            myTimer.Interval = new TimeSpan(0, 0, 1);
            myTimer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

            DateTime myUTC = DateTime.UtcNow;
            
            DateTime = TimeZoneInfo.ConvertTime(myUTC, localTimeZone);

            var enableDim = localSettings.Values["dimNightTime"];

            if (enableDim != null)
            {
                bool eD = false;

                bool.TryParse(enableDim.ToString(), out eD);

                if (eD)
                {
                    SetNightTime(DateTime);
                }
            }
           
        }

        private void SetNightTime(DateTime now)
        {
            var startNT = localSettings.Values["nightStart"];
            var endNT = localSettings.Values["nightEnd"];

            DateTime startNightTime = Convert.ToDateTime(startNT.ToString());
            DateTime endNightTime = Convert.ToDateTime(endNT.ToString());

            int start = DateTime.Compare(now, startNightTime);
            int end = DateTime.Compare(now, endNightTime);

            if(start == 0)
            {
                DeviceControls.SetBrightnessLevel(0.05);
            }

            if(end == 0)
            {
                var tmpScreenBrightness = localSettings.Values["ScreenBrightness"];
                double sb;
                if (double.TryParse(tmpScreenBrightness.ToString(), out sb))
                {
                    DeviceControls.SetBrightnessLevel(sb * .01);
                }
                else
                {
                    DeviceControls.SetBrightnessLevel(0.50);
                }
            }
        }

        public DateTime DateTime
        {
            get
            {
                return _Time;
            }
            set
            {
                this._Time = value;
                if (TimeArrived != null)
                {
                    TimeArrived(DateTime);
                }
            }
        }
    }
}
