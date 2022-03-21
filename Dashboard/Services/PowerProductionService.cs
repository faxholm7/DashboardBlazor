using DataModels;
using Microsoft.AspNetCore.Components;
namespace Dashboard.Services
{
    public class PowerProductionService : IPowerProductionService
    {
        private readonly HttpClient httpClient;
        public PowerProductionService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<PowerProductionModel> getPowerProduction()
        {
            return await httpClient.GetJsonAsync<PowerProductionModel>("http://localhost:5118/api/Inverter");
        }

        public async Task<PowerProductionModel[]> GetFullProduction()
        {
            return await httpClient.GetJsonAsync<PowerProductionModel[]>("http://localhost:5118/api/Inverter/60min");
        }
    }
}
