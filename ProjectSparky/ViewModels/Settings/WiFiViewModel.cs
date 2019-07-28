using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.Devices.WiFi;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProjectSparky.ViewModels.Settings
{
    public class WiFiViewModel : ViewModelBase
    {
        private WiFiAdapter _wifiAdapter;
        private WiFiAvailableNetwork _availableNetwork;

        public WiFiViewModel()
        {
            
        }

        public void LoadViewData()
        {
            UpdateViewData();
        }

        public void ClearViewData()
        {
            AvailableNetworks = null;
        }

        private async void UpdateViewData()
        {
            var accessAllowed = await WiFiAdapter.RequestAccessAsync();

            if(accessAllowed == WiFiAccessStatus.Allowed)
            {
                var result = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());
                if(result.Count >= 1)
                {
                    _wifiAdapter = await WiFiAdapter.FromIdAsync(result[0].Id);
                }

                string blah = GetCurrentWifiNetwork();

                GetCurrentWifiNetwork();

                ScanAvailableNetworks();
            }
        }

        private async void ScanAvailableNetworks()
        {
            ShowProgressRing = true;
            try
            { 
                await _wifiAdapter.ScanAsync();

                List<WifiNetworkDetail> myAvailableNetworks = new List<WifiNetworkDetail>();

                foreach (WiFiAvailableNetwork network in _wifiAdapter.NetworkReport.AvailableNetworks)
                {
                    if (!string.IsNullOrEmpty(network.Ssid))
                    {
                        WifiNetworkDetail tmpWifiNetwork = new WifiNetworkDetail();
                        tmpWifiNetwork.SSID = network.Ssid;
                        tmpWifiNetwork.Adapter = _wifiAdapter;
                        tmpWifiNetwork.SecurityType = network.SecuritySettings.NetworkEncryptionType.ToString();
                        tmpWifiNetwork.NetworkAuthenticationType = network.SecuritySettings.NetworkAuthenticationType.ToString();
                        tmpWifiNetwork.Network = network;
                        tmpWifiNetwork.parent = this;

                        if(tmpWifiNetwork.NetworkAuthenticationType == "Open80211")
                        {
                            tmpWifiNetwork.isSecured = false;
                            tmpWifiNetwork.Secured = "Open";
                        }
                        else
                        {
                            tmpWifiNetwork.isSecured = true;
                            tmpWifiNetwork.Secured = "Secured";

                        }

                        switch (network.SignalBars)
                        {
                            case 0:
                                tmpWifiNetwork.SignalBars = Symbol.ZeroBars;
                                break;
                            case 1:
                                tmpWifiNetwork.SignalBars = Symbol.OneBar;
                                break;
                            case 2:
                                tmpWifiNetwork.SignalBars = Symbol.TwoBars;
                                break;
                            case 3:
                                tmpWifiNetwork.SignalBars = Symbol.ThreeBars;
                                break;
                            case 4:
                                tmpWifiNetwork.SignalBars = Symbol.FourBars;
                                break;
                            default:
                                tmpWifiNetwork.SignalBars = Symbol.OneBar;
                                break;
                        }

                        if (IsConnected(network))
                        {
                            tmpWifiNetwork.IsConnected = true;
                            tmpWifiNetwork.IPv4Address = GetLocalIp();
                            tmpWifiNetwork.SecurityType = network.SecuritySettings.NetworkEncryptionType.ToString();
                            tmpWifiNetwork.NetworkAuthenticationType = network.SecuritySettings.NetworkAuthenticationType.ToString();

                            CurrentNetworkConnection = tmpWifiNetwork;

                            ShowCurrentConnection = Visibility.Visible;
                        }
                        else
                        {
                            tmpWifiNetwork.IsConnected = false;
                        }

                        if (!myAvailableNetworks.Any(x => x.SSID == tmpWifiNetwork.SSID))
                        {
                            myAvailableNetworks.Add(tmpWifiNetwork);
                        }
                    }
                }

                AvailableNetworks = new ObservableCollection<WifiNetworkDetail>(myAvailableNetworks);
            }
            catch (Exception ex)
            {

            }
            
            ShowProgressRing = false;
        }

        private string GetCurrentWifiNetwork()
        {
            var connectionProfiles = NetworkInformation.GetConnectionProfiles();

            if (connectionProfiles.Count < 1)
            {
                return null;
            }

            var validProfiles = connectionProfiles.Where(profile =>
            {
                return (profile.IsWlanConnectionProfile && profile.GetNetworkConnectivityLevel() != NetworkConnectivityLevel.None);
            });

            if (validProfiles.Count() < 1)
            {
                return null;
            }

            ConnectionProfile firstProfile = validProfiles.First();
            
            return firstProfile.ProfileName;
        }

        private bool IsConnected(WiFiAvailableNetwork network)
        {
            if (network == null)
                return false;

            string profileName = GetCurrentWifiNetwork();
            if (!String.IsNullOrEmpty(network.Ssid) &&
                !String.IsNullOrEmpty(profileName) &&
                (network.Ssid == profileName))
            {
                return true;
            }

            return false;
        }

        public string GetLocalIp()
        {
            var icp = NetworkInformation.GetInternetConnectionProfile();
            
            if (icp?.NetworkAdapter == null) return null;
            var hostname =
                NetworkInformation.GetHostNames()
                    .SingleOrDefault(
                        hn =>
                            hn.IPInformation?.NetworkAdapter != null && hn.IPInformation.NetworkAdapter.NetworkAdapterId
                            == icp.NetworkAdapter.NetworkAdapterId);

            // the ip address
            return hostname?.CanonicalName;
        }

        public void WiFiNetworks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(AvailableNetworks != null)
            {
                if (((WifiNetworkDetail)e.AddedItems[0]).IsConnected)
                {
                    ((WifiNetworkDetail)e.AddedItems[0]).ShowDisconnectButton = Visibility.Visible;
                }
                else
                {
                    ((WifiNetworkDetail)e.AddedItems[0]).ShowConnectButton = Visibility.Visible;
                }

                if (e.RemovedItems.Count() >= 1)
                {
                    ((WifiNetworkDetail)e.RemovedItems[0]).ShowConnectButton = Visibility.Collapsed;
                    ((WifiNetworkDetail)e.RemovedItems[0]).ShowDisconnectButton = Visibility.Collapsed;
                    ((WifiNetworkDetail)e.RemovedItems[0]).ShowNetworkKeyBox = Visibility.Collapsed;
                    ((WifiNetworkDetail)e.RemovedItems[0]).ShowErrorMessage = Visibility.Collapsed;
                }
            }
        }

        private bool _ShowProgressRing = false;
        public bool ShowProgressRing
        {
            get
            {
                return _ShowProgressRing;
            }
            set
            {
                _ShowProgressRing = value;
                RaisePropertyChanged("ShowProgressRing");
            }
        }

        private ObservableCollection<WifiNetworkDetail> _AvailableNetworks;
        public ObservableCollection<WifiNetworkDetail> AvailableNetworks
        {
            get
            {
                return _AvailableNetworks;
            }
            set
            {
                _AvailableNetworks = value;
                RaisePropertyChanged("AvailableNetworks");
            }
        }

        private Visibility _ShowCurrentConnection = Visibility.Collapsed;
        public Visibility ShowCurrentConnection
        {
            get
            {
                return _ShowCurrentConnection;
            }
            set
            {
                _ShowCurrentConnection = value;
                RaisePropertyChanged("ShowCurrentConnection");
            }
        }

        private WifiNetworkDetail _CurrentNetworkConnection = null;
        public WifiNetworkDetail CurrentNetworkConnection
        {
            get
            {
                return _CurrentNetworkConnection;
            }
            set
            {
                _CurrentNetworkConnection = value;
                RaisePropertyChanged("CurrentNetworkConnection");
            }
        }
    }

    public class WifiNetworkDetail : ViewModelBase
    {
        public string SSID { get; set;}
        public Symbol SignalBars { get; set; }
        public bool IsConnected { get; set; }
        public string IPv4Address { get; set; }
        public string SecurityType { get; set; }
        public string NetworkAuthenticationType { get; set; }
        public bool isSecured { get; set; }
        public string Secured { get; set; }
        public WiFiAdapter Adapter { get; set; }
        public WiFiAvailableNetwork Network { get; set; }
        public WiFiViewModel parent { get; set; }

        private Visibility _ShowConnectButton = Visibility.Collapsed;
        public Visibility ShowConnectButton
        {
            get
            {
                return _ShowConnectButton;
            }
            set
            {
                _ShowConnectButton = value;
                RaisePropertyChanged("ShowConnectButton");
            }
        }

        private Visibility _ShowDisconnectButton = Visibility.Collapsed;
        public Visibility ShowDisconnectButton
        {
            get
            {
                return _ShowDisconnectButton;
            }
            set
            {
                _ShowDisconnectButton = value;
                RaisePropertyChanged("ShowDisconnectButton");
            }
        }

        private Visibility _ShowNetworkKeyBox = Visibility.Collapsed;
        public Visibility ShowNetworkKeyBox
        {
            get
            {
                return _ShowNetworkKeyBox;
            }
            set
            {
                _ShowNetworkKeyBox = value;
                RaisePropertyChanged("ShowNetworkKeyBox");
            }
        }

        private Visibility _ShowErrorMessage = Visibility.Collapsed;
        public Visibility ShowErrorMessage
        {
            get
            {
                return _ShowErrorMessage;
            }
            set
            {
                _ShowErrorMessage = value;
                RaisePropertyChanged("ShowErrorMessage");
            }
        }

        private ICommand _ConnectWiFiCommand;
        public ICommand ConnectWiFiCommand
        {
            get
            {
                return _ConnectWiFiCommand ??
                    new  RelayCommand(async () =>
                    {
                        if (this.isSecured)
                        {
                            ShowNetworkKeyBox = Visibility.Visible;
                            ShowConnectButton = Visibility.Collapsed;
                        }
                        else
                        {
                            var wifiConnectionResult = await Adapter.ConnectAsync(this.Network, WiFiReconnectionKind.Automatic);

                            if (wifiConnectionResult.ConnectionStatus.ToString() == "Success")
                            {
                                this.IPv4Address = parent.GetLocalIp();

                                parent.CurrentNetworkConnection = this;
                                parent.ShowCurrentConnection = Visibility.Visible;
                                ShowConnectButton = Visibility.Collapsed;
                                ShowDisconnectButton = Visibility.Visible;
                            }
                            else
                            {
                                //Show Error
                                ErrorMessage = "Could not connect, please try again.";
                                ShowErrorMessage = Visibility.Visible;
                            }
                        }
                    });
            }
        }

        private ICommand _DisconnectButtonCommand;
        public ICommand DisconnectButtonCommand
        {
            get
            {
                return _DisconnectButtonCommand ??
                    new RelayCommand(() =>
                    {
                        //Do disconnect stuff...
                        Adapter.Disconnect();
                        ShowConnectButton = Visibility.Visible;
                        ShowDisconnectButton = Visibility.Collapsed;

                        parent.ShowCurrentConnection = Visibility.Collapsed;
                    });
            }
        }

        private ICommand _CancelNetworkKeyButtonCommand;
        public ICommand CancelNetworkKeyButtonCommand
        {
            get
            {
                return _DisconnectButtonCommand ??
                    new RelayCommand(() =>
                    {
                        ShowNetworkKeyBox = Visibility.Collapsed;
                        ShowConnectButton = Visibility.Visible;
                        ShowErrorMessage = Visibility.Collapsed;
                    });
            }
        }

        private ICommand _SecureConnectionButton;
        public ICommand SecureConnectionButton
        {
            get
            {
                return _SecureConnectionButton ??
                    new RelayCommand(async () =>
                    {
                        if (!String.IsNullOrEmpty(NetworkKey))
                        {
                            var credential = new Windows.Security.Credentials.PasswordCredential();
                            credential.Password = NetworkKey;

                            var wifiConnectionResult = await Adapter.ConnectAsync(this.Network, WiFiReconnectionKind.Automatic, credential);

                            if (wifiConnectionResult.ConnectionStatus.ToString() == "Success")
                            {
                                //Show Connection Results.
                                this.IPv4Address = parent.GetLocalIp();

                                parent.CurrentNetworkConnection = this;
                                parent.ShowCurrentConnection = Visibility.Visible;

                                ShowNetworkKeyBox = Visibility.Collapsed;
                                ShowConnectButton = Visibility.Collapsed;
                                ShowDisconnectButton = Visibility.Visible;
                            }
                            else
                            {
                                //Show Error
                                ErrorMessage = "Could not connect, please check network key and try again.";
                                ShowErrorMessage = Visibility.Visible;
                            }
                        }
                        else
                        {
                            //Show Error...
                            ErrorMessage = "Please enter the network key.";
                            ShowErrorMessage = Visibility.Visible;
                        }
                    });
            }
        }

        private string _NetworkKey;
        public string NetworkKey
        {
            get
            {
                return _NetworkKey;
            }
            set
            {
                _NetworkKey = value;
                RaisePropertyChanged("NetworkKey");
            }
        }

        private string _ErrorMessage;
        public string ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }
            set
            {
                _ErrorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }
        
    }
}
