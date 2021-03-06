using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class WeatherForecastModel
    {
        public float Temperature { get; set; }

        public string? Conditions { get; set; }
        public float Cloudcover { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
        public string? Location { get; set; }
        public DateTime Day { get; set; }
    }
}
