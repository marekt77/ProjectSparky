namespace ProjectSparky.Common
{
    public class WeatherTools
    {
        public static string ConditionCode_to_WeatherIcon(int ConditionCode)
        {
            string WeatherIconFilename = "Partly_Cloudy";

            if (ConditionCode >= 200 && ConditionCode <= 299)
            {
                //Group 2xx: Thunderstorm
                WeatherIconFilename = "Thunderstorm";
            }
            else if (ConditionCode >= 300 && ConditionCode <= 499)
            {
                //Group 3xx: Drizzle
                WeatherIconFilename = "Light_Rain";
            }
            else if (ConditionCode >= 500 && ConditionCode <= 599)
            {
                //Group 5xx: Rain

                if (ConditionCode == 500)
                {
                    WeatherIconFilename = "Light_Rain";
                }
                else if (ConditionCode == 501)
                {
                    WeatherIconFilename = "Rain";
                }
                else if (ConditionCode == 502 && ConditionCode == 503)
                {
                    //Heavy Rain
                    WeatherIconFilename = "Rain";
                }
                else if (ConditionCode == 504)
                {
                    //Torrential_Rain
                    WeatherIconFilename = "Rain";
                }
                else
                {
                    WeatherIconFilename = "Rain";
                }
            }
            else if (ConditionCode >= 600 && ConditionCode <= 699)
            {
                //Group 6xx: Snow

                if (ConditionCode == 600)
                {
                    //Light Snow
                    WeatherIconFilename = "Light_Snow";
                }
                else if (ConditionCode == 601)
                {
                    WeatherIconFilename = "Snow";
                }
                else if (ConditionCode == 602)
                {
                    WeatherIconFilename = "Snow_Storm";
                }
                else if (ConditionCode == 611 && ConditionCode == 612)
                {
                    WeatherIconFilename = "Sleet";
                }
                else
                {
                    WeatherIconFilename = "Snow";
                }
            }
            else if (ConditionCode >= 700 && ConditionCode <= 799)
            {
                //Group 7xx: Atmosphere
                if (ConditionCode == 701 && ConditionCode == 721 && ConditionCode == 741)
                {
                    WeatherIconFilename = "Haze";
                }
                else if (ConditionCode == 781)
                {
                    //Tornado
                    WeatherIconFilename = "Tornado";
                }
                else
                {
                    WeatherIconFilename = "Partly_Cloudy";
                }
            }
            else if (ConditionCode >= 800 && ConditionCode <= 899)
            {
                if (ConditionCode == 800)
                {
                    WeatherIconFilename = "Sunny";
                }
                else if (ConditionCode >= 801 && ConditionCode <= 803)
                {
                    WeatherIconFilename = "Partly_Cloudy";
                }
                else
                {
                    WeatherIconFilename = "Partly_Cloudy";
                }

            }
            else if (ConditionCode >= 900 && ConditionCode <= 999)
            {
                //Group 9xx: Extreme/Wind

                if (ConditionCode == 900)
                {
                    WeatherIconFilename = "Tornado";
                }
                else if (ConditionCode >= 901 && ConditionCode <= 904)
                {
                    WeatherIconFilename = "Partly_Cloudy";
                }
                else if (ConditionCode == 905)
                {
                    WeatherIconFilename = "Windy";
                }
                else if (ConditionCode == 906)
                {
                    WeatherIconFilename = "Hail";
                }
                else if (ConditionCode >= 951 && ConditionCode <= 959)
                {
                    WeatherIconFilename = "Windy";
                }
                else
                {
                    WeatherIconFilename = "Thunderstorm";
                }
            }
            return WeatherIconFilename;
        }
    }
}
