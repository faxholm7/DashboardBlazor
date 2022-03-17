using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class PowerPriceModel
    {
        public double SpotPriceDKK { get; set; }

       // [Timestamp]
        public DateTime HourDK { get; set; }
        
    }
}
