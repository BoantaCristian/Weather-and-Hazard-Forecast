using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DbModels
{
    public class HazardArchive
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Location Location { get; set; }
        public virtual HazardType HazardType { get; set; }
    }
}
