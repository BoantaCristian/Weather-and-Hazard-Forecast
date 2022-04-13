using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DbModels
{
    public class User : IdentityUser
    {
        public virtual ICollection<UserLocation> UserLocations { get; set; }
        public virtual ICollection<SevereWeatherArchive> SevereWeathers { get; set; }
        public virtual ICollection<CovidArchive> CovidArchives { get; set; }
    }
}
