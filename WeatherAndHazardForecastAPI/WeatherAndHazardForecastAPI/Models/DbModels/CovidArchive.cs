using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DbModels
{
    public class CovidArchive
    {
        [Key]
        public int Id { get; set; }
        public string Active_Cases_text { get; set; }
        public string Country_text { get; set; }
        public string Last_Update { get; set; }
        public string New_Cases_text { get; set; }
        public string New_Deaths_text { get; set; }
        public string Total_Cases_text { get; set; }
        public string Total_Deaths_text { get; set; }
        public string Total_Recovered_text { get; set; }
        public virtual User User { get; set; }
    }
}
