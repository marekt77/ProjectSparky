﻿using System.Collections.Generic;

namespace ProjectSparky.Model.Weather.OpenWeatherData.MultDayForecast
{
    public class Forecast
    {
        public int dt { get; set; }
        public Temp temp { get; set; }
        public double pressure { get; set; }
        public int humidity { get; set; }
        public List<Weather> weather { get; set; }
        public double speed { get; set; }
        public int deg { get; set; }
        public int clouds { get; set; }
        public double rain { get; set; }
    }
}
