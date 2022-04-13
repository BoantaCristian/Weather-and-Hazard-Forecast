using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DataTransferObjects
{
    public class WeatherAlerts
    {
        public string[] regions { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string severity { get; set; }
        public string effective_local { get; set; } //start
        public string expires_local { get; set; } //end
    }
}
