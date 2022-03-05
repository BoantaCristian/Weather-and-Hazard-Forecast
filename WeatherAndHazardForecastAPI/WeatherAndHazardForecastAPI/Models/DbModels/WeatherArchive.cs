using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DbModels
{
    public class WeatherArchive
    {
        public int Id { get; set; }
        public string WeatherType { get; set; }
        public double Temperature { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public int Humidity { get; set; }
        public int Wind { get; set; }
        public string UVIndex { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public virtual User User { get; set; }
        public virtual Location Location { get; set; }
    }
}
