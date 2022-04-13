using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DbModels
{
    public class WeatherArchive
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Valid_date { get; set; }
        public double Temperature { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double Wind_Speed { get; set; }
        public double Wind_Gust_Speed { get; set; }
        public string Wind_Direction { get; set; }
        public string Wind_Direction_Full { get; set; }
        public int Wind_Direction_Degrees { get; set; }
        public double Clouds { get; set; }
        public double Snow { get; set; }
        public double Snow_Depth { get; set; }
        public double UV_Index { get; set; }
        public int Relative_Humidity { get; set; }
        public double Precipitations { get; set; }
        public int Probability_Precipitation { get; set; }
        public double Pressure { get; set; }
        public double Visibility { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
        public virtual Location Location { get; set; }
    }
}
