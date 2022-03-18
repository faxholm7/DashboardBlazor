using DataModels;
namespace Dashboard.Services
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecastModel> GetWeather();
        Task<WeatherForecastModel[]> GetWeatherForecast();
    }
}
