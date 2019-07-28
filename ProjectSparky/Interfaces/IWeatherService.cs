using ProjectSparky.Model.Weather;
using System.Threading.Tasks;

namespace ProjectSparky.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherForecast> GetCurrentForecast(int CityID, bool UseMetric);

        Task<FiveDayForecast> GetFiveDayForecast(int CityID, bool UseMetric);

        Task<bool> CheckStatus();
    }
}
