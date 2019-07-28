using ProjectSparky.Interfaces;
using ProjectSparky.Model.Weather;
using ProjectSparky.Model.Weather.OpenWeatherData.CurrentWeather;
using ProjectSparky.Model.Weather.OpenWeatherData.MultDayForecast;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace ProjectSparky.Services
{
    public class WeatherService : IWeatherService
    {
        private string baseURL = "https://api.openweathermap.org/data/2.5/";
        private string OW_AppID = "f81bd94aafa4e585ca60ec10807c9a6a";

        public async Task<WeatherForecast> GetCurrentForecast(int CityID, bool UseMetric)
        {
            WeatherForecast myCurrentForecast = new WeatherForecast();
            myCurrentForecast.CurrentTemp = 9999;

            Uri myURI;

            if (UseMetric)
            {
                myURI = new Uri(baseURL + "weather?id=" + CityID + "&units=metric" + "&appid=" + OW_AppID);
            }
            else
            {
                myURI = new Uri(baseURL + "weather?id=" + CityID + "&units=imperial" + "&appid=" + OW_AppID);
            }

            HttpClient myClient = new HttpClient();
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                httpResponse = await myClient.GetAsync(myURI);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var rawCurrentWeather = JsonConvert.DeserializeObject<CurrentWeather>(httpResponse.Content.ToString());
                    myCurrentForecast.CurrentTemp = rawCurrentWeather.Main.temp;
                    myCurrentForecast.ForcastDate = DateTime.Now;
                    myCurrentForecast.DayHighTemp = rawCurrentWeather.Main.temp_max;
                    myCurrentForecast.DayLowTemp = rawCurrentWeather.Main.temp_min;
                    myCurrentForecast.DayCondition = rawCurrentWeather.Weather.FirstOrDefault().id;
                    myCurrentForecast.Humidity = rawCurrentWeather.Main.humidity;
                    myCurrentForecast.Pressure = rawCurrentWeather.Main.pressure;
                    myCurrentForecast.WindSpeed = rawCurrentWeather.Wind.speed;
                }
            }
            catch (Exception ex)
            {
                myClient.Dispose();
                throw new Exception("ERROR! Cannot retrive Weather Data.", ex);
            }

            return myCurrentForecast;
        }

        public async Task<FiveDayForecast> GetFiveDayForecast(int CityID, bool UseMetric)
        {
            FiveDayForecast myFiveDayForcast = new FiveDayForecast();
            myFiveDayForcast.FiveDayForcast = new List<WeatherForecast>();
            Uri myURI;

            if (UseMetric)
            {
                myURI = new Uri(baseURL + "forecast/daily?id=" + CityID + "&cnt=6" + "&units=metric" + "&appid=" + OW_AppID);
            }
            else
            {
                myURI = new Uri(baseURL + "forecast/daily?id=" + CityID + "&cnt=6" + "&units=imperial" + "&appid=" + OW_AppID);
            }

            HttpClient myClient = new HttpClient();
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                httpResponse = await myClient.GetAsync(myURI);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var rawMultiDayForecast = JsonConvert.DeserializeObject<MultiDayForecast>(httpResponse.Content.ToString());

                    myFiveDayForcast.City = App.CurrentAppData.City;

                    bool isFirst = true;

                    foreach (Forecast tmpForecast in rawMultiDayForecast.list)
                    {
                        if (!isFirst)
                        {
                            WeatherForecast addWF = new WeatherForecast();

                            addWF.ForcastDate = TimeZoneInfo.ConvertTime(DateTimeOffset.FromUnixTimeSeconds(tmpForecast.dt), TimeZoneInfo.Utc).DateTime;
                            addWF.CurrentTemp = tmpForecast.temp.day;
                            addWF.DayCondition = tmpForecast.weather.FirstOrDefault().id;
                            addWF.DayHighTemp = tmpForecast.temp.max;
                            addWF.DayLowTemp = tmpForecast.temp.min;
                            addWF.Humidity = tmpForecast.humidity;
                            addWF.Pressure = tmpForecast.pressure;
                            addWF.WindSpeed = tmpForecast.speed;

                            myFiveDayForcast.FiveDayForcast.Add(addWF);
                        }
                        else
                        {
                            isFirst = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                myClient.Dispose();
                throw new Exception("ERROR! Cannot retrive Weather Data.", ex);
            }

            return myFiveDayForcast;
        }

        public async Task<bool> CheckStatus()
        {
            bool isConnected = false;

            Uri myURI = new Uri(baseURL + "weather?q=chicago&appid=" + OW_AppID);

            HttpClient myClient = new HttpClient();
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                httpResponse = await myClient.GetAsync(myURI);

                if (httpResponse.IsSuccessStatusCode)
                {
                    isConnected = true;
                }
            }
            catch (Exception ex)
            {
                myClient.Dispose();
                throw new Exception("ERROR! Cannot retrive Weather Data.", ex);
            }

            myClient.Dispose();

            return isConnected;
        }
    }
}
