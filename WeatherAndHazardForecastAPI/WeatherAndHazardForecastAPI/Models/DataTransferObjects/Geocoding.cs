using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DataTransferObjects
{
    public class Geocoding
    {
        public string display_name { get; set; }
        public string lon { get; set; }
        public string lat { get; set; }
        public string type { get; set; }
    }
}
