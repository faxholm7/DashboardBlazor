using DataModels;

namespace Dashboard.Services
{
    public interface IPowerPriceService
    {
        Task<PowerPriceModel> getPowerPrice();
    }
}
