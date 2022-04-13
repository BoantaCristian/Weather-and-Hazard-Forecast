using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DataTransferObjects
{
    public class SignificantEarthquakes
    {
        public string time { get; set; }
        public string timezone { get; set; }
        public string time_ago { get; set; }
        public string location { get; set; }
        public string region { get; set; }
        public string nearest_city { get; set; }
        public string magnitude { get; set; }
        public string effects { get; set; }
    }
}
