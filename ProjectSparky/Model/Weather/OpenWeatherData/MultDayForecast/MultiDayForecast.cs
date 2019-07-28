using System.Collections.Generic;

namespace ProjectSparky.Model.Weather.OpenWeatherData.MultDayForecast
{
    public class MultiDayForecast
    {
        public City city { get; set; }
        public string cod { get; set; }
        public double message { get; set; }
        public int cnt { get; set; }
        public List<Forecast> list { get; set; }
    }
}
