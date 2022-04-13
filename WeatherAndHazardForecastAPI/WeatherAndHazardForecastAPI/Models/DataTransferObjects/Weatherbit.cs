using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DataTransferObjects
{
    public class Weatherbit
    {
        public string city_name { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public string timezone { get; set; }
        public string country_code { get; set; }
        public string state_code { get; set; }
        public List<DayWeather> data { get; set; }

    }
}
