using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DbModels
{
    public class HazardType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }
        public virtual ICollection<HazardArchive> HazardArchives { get; set; }
    }
}
    