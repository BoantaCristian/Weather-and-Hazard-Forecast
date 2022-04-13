using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DataTransferObjects
{
    public class WeatherDescription
    {
        public string icon { get; set; }
        public int code { get; set; }
        public string description { get; set; }
    }
}
