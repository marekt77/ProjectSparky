using System;

namespace ProjectSparky.Model.Weather
{
    public class WeatherForecast
    {
        public DateTime ForcastDate { get; set; }
        public double CurrentTemp { get; set; }
        public double DayHighTemp { get; set; }
        public double DayLowTemp { get; set; }
        public int DayCondition { get; set; }
        public double Pressure { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
    }
}
