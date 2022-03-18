using DataModels;
namespace Dashboard.Services
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecastModel> GetWeatherAsync();
        Task<WeatherForecastModel[]> GetWeatherMultiAsync();
    }
}
