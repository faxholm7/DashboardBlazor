using DataModels;
using Microsoft.AspNetCore.Components;
namespace Dashboard.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly HttpClient httpClient;
        public WeatherForecastService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<WeatherForecastModel> GetWeather()
        {
            return await httpClient.GetJsonAsync<WeatherForecastModel>("http://localhost:5155/api/Weather");
        }
        public async Task<WeatherForecastModel[]> GetWeatherForecast()
        {
            return await httpClient.GetJsonAsync<WeatherForecastModel[]>("http://localhost:5155/api/Weather/24hours");
        }
    }
}