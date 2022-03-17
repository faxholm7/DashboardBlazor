using DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PowerPrice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerpriceController : ControllerBase
    {

        [HttpGet]
        [Route("getPowerPrice")]

        public async Task<IActionResult> getPowerPrice()
        {         

            HttpClient client = new();

            string url = "https://api.energidataservice.dk/datastore_search_sql?sql=SELECT%20%22HourDK%22,%20%22SpotPriceDKK%22%20FROM%20%22elspotprices%22%20WHERE%20%22PriceArea%22=%27DK1%27%20ORDER%20BY%20%22HourDK%22%20DESC%20LIMIT%201";

            var stream = client.GetStreamAsync(url);
            var result = await JsonSerializer.DeserializeAsync<PowerPriceModel>(await stream);
            return Ok(result);
            //List<PowerPriceModel> _pricelist = new List<PowerPriceModel>();

            //PowerPriceModel? data;

            //data = new PowerPriceModel { HourDK = DateTime.Now, SpotPriceDKK = 1397.54 };
            //_pricelist.Add(data);
            //return Ok(_pricelist);
        }
    }
}

//https://api.energidataservice.dk/datastore_search_sql?sql=SELECT "HourDK", "SpotPriceDKK" from "elspotprices" WHERE "PriceArea"='DK1' ORDER BY "HourDK" DESC LIMIT 24
