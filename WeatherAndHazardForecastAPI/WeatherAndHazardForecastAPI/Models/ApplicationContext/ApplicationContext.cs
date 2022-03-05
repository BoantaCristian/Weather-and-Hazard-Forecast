using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAndHazardForecastAPI.Models.DbModels;

namespace WeatherAndHazardForecastAPI.Models.ApplicationContext
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<UserLocation> UserLocations { get; set; }
        public DbSet<WeatherArchive> WeatherArchives { get; set; }
        public DbSet<HazardArchive> HazardArchives { get; set; }
        public DbSet<HazardType> HazardTypes { get; set; }
    }
}
