using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DbModels
{
    public class SevereWeatherArchive
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Regions { get; set; }
        public string Severity { get; set; }
        public string Local_Start_Date { get; set; }
        public string Local_End_Date { get; set; }
        public virtual Location Location { get; set; }
        public virtual User User { get; set; }
    }
}
