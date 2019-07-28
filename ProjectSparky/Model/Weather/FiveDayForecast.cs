using System.Collections.Generic;

namespace ProjectSparky.Model.Weather
{
    public class FiveDayForecast
    {
        public string City { get; set; }
        public List<WeatherForecast> FiveDayForcast { get; set; }
    }
}
