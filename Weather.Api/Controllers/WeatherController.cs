using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ServiceReferenceWeather;
using Weather.Api.Models;

namespace Weather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
   
    public class WeatherController : ControllerBase
    {    

        private readonly string key = "Jeger1studerende";
        private readonly string location = "Kolding";

        [HttpGet]
        public async Task<WeatherForecastModel> GetWeather()
        {


            ForecastServiceClient client = new();
            Forecast response = client.GetForecastAsync(location, key).Result.Body.GetForecastResult;

            var result = response.location.values[1];
            return await Task.FromResult(new WeatherForecastModel
            {
                Cloudcover = (float)result.cloudcover,
                Temperature = (float)result.temp,
                Conditions = result.conditions,
                Day = DateTimeOffset.FromUnixTimeMilliseconds(result.datetime).DateTime,
                Location = location
            });


        }
        [HttpGet]
        [Route("api/[controller]/24hours")]
        public async Task<WeatherForecastModel[]> GetWeatherForecast()
        {        

            ForecastServiceClient client = new();
            Forecast response = client.GetForecastAsync(location, key).Result.Body.GetForecastResult;

            var result = response.location.values;

            return await Task.FromResult(Enumerable.Range(0, 24).Select(index => new WeatherForecastModel
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




