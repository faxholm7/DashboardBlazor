using ServiceReferenceWeather;

namespace Dashboard.Data
{
   /* public class WeatherForecastMultiService
    {

        public async Task<WeatherForecast[]> GetWeatherAsync(string location, string key)
        {

            ForecastServiceClient client = new();
            Forecast response = client.GetForecastAsync(location, key).Result.Body.GetForecastResult;
         
            var result = response.location.values;
            return await Task.FromResult(Enumerable.Range(0, 23).Select(index => new WeatherForecast
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