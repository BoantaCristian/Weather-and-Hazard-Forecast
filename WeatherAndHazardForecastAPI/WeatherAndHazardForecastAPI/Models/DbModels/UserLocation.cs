using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DbModels
{
    public class UserLocation
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Location Location { get; set; }
    }
}
