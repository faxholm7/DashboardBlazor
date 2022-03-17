using DataModels;

namespace Dashboard.Services
{
    public interface IPowerPriceService
    {
        Task<IEnumerable<PowerPriceModel>> getPowerPrice();
    }
}
