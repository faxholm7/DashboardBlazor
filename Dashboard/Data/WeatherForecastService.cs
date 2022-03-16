using ServiceReferenceWeather;

namespace Dashboard.Data
{
    public class WeatherForecastService
    {

        public async Task<WeatherForecast> GetWeatherAsync(string location, string key)
        {

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

        public Task<WeatherForecast[]> GetWeatherMultiAsync(string location, string key)
        {

            ForecastServiceClient client = new();
            Forecast response = client.GetForecastAsync(location, key).Result.Body.GetForecastResult;

            var result = response.location.values;
            return Task.FromResult(Enumerable.Range(0, 24).Select(index => new WeatherForecast
            {
                Cloudcover = (float)result[index].cloudcover,
                Temperature = (float)result[index].temp,
                Conditions = result[index].conditions,
                Day = DateTimeOffset.FromUnixTimeMilliseconds(result[index].datetime).DateTime,
                Location = location
            }).ToArray());



        }
    }

   /* public class WeatherForecastMultiService
    {

        public Task<WeatherForecast[]> GetWeatherAsync(string location, string key)
        {

            ForecastServiceClient client = new();
            Forecast response = client.GetForecastAsync(location, key).Result.Body.GetForecastResult;

            var result = response.location.values;
            return Task.FromResult(Enumerable.Range(1,5).Select(index => new WeatherForecast
            {
                Cloudcover = (float)result[index].cloudcover,
                Temperature = (float)result[index].temp,
                Conditions = result[index].conditions,
                Hour = DateTime.Now.Hour + 1,
                Location = location
            }).ToArray());



        }
    }*/
}


//public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
//{
//    return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
//    {
//        Date = startDate.AddDays(index),
//        TemperatureC = Random.Shared.Next(-20, 55),
//        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//    }).ToArray());
//}