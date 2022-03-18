using ServiceReferenceWeather;

namespace Dashboard.Data
{
    public class WeatherForecastService
    {
        private readonly string _key = "Jeger1studerende";
        private readonly string _location = "Kolding";

        public async Task<WeatherForecast> GetWeatherAsync()
        {
            string key = _key;
            string location = _location;

            ForecastServiceClient client = new();
            Forecast response = client.GetForecastAsync(location, key).Result.Body.GetForecastResult;

            var result = response.location.values[1];

            return await Task.FromResult(new WeatherForecast
            {
                Cloudcover = (float)result.cloudcover,
                Temperature = (float)result.temp,
                Conditions = result.conditions,
                Day = DateTimeOffset.FromUnixTimeMilliseconds(result.datetime).DateTime,
                Location = location
            });


        }

        public async Task<WeatherForecast[]> GetWeatherMultiAsync(string location, string key)
        {

            ForecastServiceClient client = new();
            Forecast response = client.GetForecastAsync(location, key).Result.Body.GetForecastResult;

            var result = response.location.values;
            return await Task.FromResult(Enumerable.Range(0, 24).Select(index => new WeatherForecast
            {
                Cloudcover = (float)result[index].cloudcover,
                Temperature = (float)result[index].temp,
                Conditions = result[index].conditions,
                Day = DateTimeOffset.FromUnixTimeMilliseconds(result[index].datetime).DateTime,
                Location = location
            }).ToArray());



        }
    }
}