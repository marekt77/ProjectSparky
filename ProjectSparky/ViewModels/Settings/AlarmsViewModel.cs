using GalaSoft.MvvmLight;
using ProjectSparky.Interfaces;
using ProjectSparky.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace ProjectSparky.ViewModels.Settings
{
    public class AlarmsViewModel : ViewModelBase
    {
        private ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
        private readonly IAlarmService _alarmService;

        public AlarmsViewModel(IAlarmService tmpAlarmService)
        {
            _alarmService = tmpAlarmService;
        }

        public void LoadViewData()
        {
            LoadAlarms();
        }

        public void ClearViewData()
        {

        }

        private async void LoadAlarms()
        {
            List<Alarm> tmpAlarms = await _alarmService.GetAlarms();
            Alarms = new ObservableCollection<Alarm>(tmpAlarms); 
        }

        private ObservableCollection<Alarm> _Alarms;
        public ObservableCollection<Alarm> Alarms
        {
            get
            {
                return _Alarms;
            }
            set
            {
                _Alarms = value;
                RaisePropertyChanged("Alarms");
            }
        }

        private Visibility _showDeleteButton = Visibility.Collapsed;
        public Visibility ShowDeleteButton
        {
            get
            {
                return _showDeleteButton;
            }
            set
            {
                _showDeleteButton = value;
                RaisePropertyChanged("ShowDeleteButton");
            }
        }

    }
}
