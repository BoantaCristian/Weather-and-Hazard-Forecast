using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DbModels
{
    public class Earthquake
    {
        [Key]
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Time_ago { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Depth { get; set; }
        public string Magnitude { get; set; }
        public string Location { get; set; }
        public string Timezone { get; set; }
        public string Region { get; set; }
        public string Nearest_city { get; set; }
        public string Effects { get; set; }
        public string Status { get; set; } //recent/significant
    }
}
