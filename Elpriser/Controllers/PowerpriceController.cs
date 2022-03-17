using DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PowerPrice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerpriceController : ControllerBase
    {
        private string date = "2022-03-17T23:00:00";

        [HttpGet]
        [Route("getPowerPrice")]

        public IActionResult getPowerPrice()
        {
            List<PowerPriceModel> _pricelist = new List<PowerPriceModel>();

            PowerPriceModel? data;

            data = new PowerPriceModel { HourDK = DateTime.Now, SpotPriceDKK = 1397.54 };
            _pricelist.Add(data);
            return Ok(_pricelist);
        }
    }
}
