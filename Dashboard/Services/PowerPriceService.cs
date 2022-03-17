
using DataModels;
using Microsoft.AspNetCore.Components;

namespace Dashboard.Services
{
    public class PowerPriceService : IPowerPriceService
    {
        private readonly HttpClient httpClient;
        public PowerPriceService(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }
        public async Task<IEnumerable<PowerPriceModel>> getPowerPrice()
        {
            return await httpClient.GetJsonAsync<PowerPriceModel[]>("http://localhost:5020/api/Powerprice/getPowerPrice");
        }
    }
}
