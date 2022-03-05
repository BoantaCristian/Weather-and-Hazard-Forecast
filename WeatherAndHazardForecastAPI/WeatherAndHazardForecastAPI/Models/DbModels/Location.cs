using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DbModels
{
    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public virtual ICollection<UserLocation> UserLocations { get; set; }
        public virtual ICollection<WeatherArchive> WeatherArchives { get; set; }
        public virtual ICollection<HazardArchive> HazardArchives { get; set; }
    }
}
