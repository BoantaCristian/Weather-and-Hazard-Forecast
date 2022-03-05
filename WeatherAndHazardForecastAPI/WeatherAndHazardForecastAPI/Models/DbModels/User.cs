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
        public virtual ICollection<WeatherArchive> WeatherArchives { get; set; }
        public virtual ICollection<HazardArchive> HazardArchives { get; set; }
    }
}
