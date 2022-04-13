using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DataTransferObjects
{
    public class RecentEarthquakes
    {
        public string date { get; set; }
        public string time { get; set; }
        public string time_ago { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string depth { get; set; }
        public string magnitude { get; set; }
        public string location { get; set; }
    }
}
