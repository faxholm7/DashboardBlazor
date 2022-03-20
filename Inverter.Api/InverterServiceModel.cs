using CsvHelper.Configuration.Attributes;

namespace Inverter.Api
{
    public class InverterServiceModel
    {    
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? EnergyStart { get; set; }
        public string? EnergyEnd { get; set; }
    }
}