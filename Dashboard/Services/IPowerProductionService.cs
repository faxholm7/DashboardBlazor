using DataModels;
namespace Dashboard.Services
{
    public interface IPowerProductionService
    {
        Task<PowerProductionModel> getPowerProduction();
        Task<PowerProductionModel[]> GetFullProduction();
    }
}
