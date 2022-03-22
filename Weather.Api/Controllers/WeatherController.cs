using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ServiceReferenceWeather;

namespace Weather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
   
    public class WeatherController : ControllerBase
    {    

        private readonly string key = "Jeger1studerende";
        private readonly string location = "Kolding";

        [HttpGet]
        public async Task<WeatherServiceModel> GetWeather()
        {
            Console.WriteLine("Koden er kørt");
            ForecastServiceClient client = new();
            Forecast response = client.GetForecastAsync(location, key).Result.Body.GetForecastResult;
            var result =  response.location;

            return await Task.FromResult(new WeatherServiceModel
            {
                Cloudcover = (float)result.values[1].cloudcover,
                Temperature = (float)result.values[1].temp,
                Conditions = result.values[1].conditions,
                Day = result.values[1].datetimeStr,
                Sunrise = result.currentConditions.sunrise,
                Sunset = result.currentConditions.sunset,
                Location = location
            });

        }

        [HttpGet]
        [Route("24hours")]
        public async Task<WeatherServiceModel[]> GetWeatherForecast()
        {   
            ForecastServiceClient client = new();
            Forecast response = client.GetForecastAsync(location, key).Result.Body.GetForecastResult;

            var result = response.location;

            return await Task.FromResult(Enumerable.Range(0, 24).Select(index => new WeatherServiceModel
            {
                Cloudcover = (float)result.values[index].cloudcover,
                Temperature = (float)result.values[index].temp,
                Conditions = result.values[index].conditions,
                Day = result.values[index].datetimeStr,
                Sunrise = result.currentConditions.sunrise,
                Sunset = result.currentConditions.sunset,
                Location = location
            }).ToArray());

        }
      

    }
}



/*  public async Task<WeatherModel> GetWeather()
  {
     // string key = _key;
      //string location = _location;

      ForecastServiceClient client = new();
      Forecast response = client.GetForecastAsync(location, key).Result.Body.GetForecastResult;

      var result = response.location.values[1];          
      await Task.FromResult(new WeatherModel
      {
          Cloudcover = (float)result.cloudcover,
          Temperature = (float)result.temp,
          Conditions = result.conditions,
          Day = DateTimeOffset.FromUnixTimeMilliseconds(result.datetime).DateTime,
          Location = location
      });


  }*/




