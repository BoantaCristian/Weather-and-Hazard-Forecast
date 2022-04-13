using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DbModels
{
    public class Location
    {
        public int Id { get; set; }
        public string City_name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Timezone { get; set; }
        public string Country_Name { get; set; }
        public string Country_code { get; set; }
        public string State_code { get; set; }
        public virtual ICollection<UserLocation> UserLocations { get; set; }
        public virtual ICollection<WeatherArchive> WeatherArchives { get; set; }
        public virtual ICollection<SevereWeatherArchive> SevereWeathers { get; set; }
    }
}
