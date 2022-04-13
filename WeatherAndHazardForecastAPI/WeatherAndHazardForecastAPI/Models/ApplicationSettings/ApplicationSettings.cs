using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.ApplicationSettings
{
    public class ApplicationSettings
    {
        public string JWT_Secret { get; set; }
        public string X_RapidAPI_Key { get; set; }
        public string X_RapidAPI_Host_Weather { get; set; }
        public string X_RapidAPI_Host_Geocoding { get; set; }
        public string X_RapidAPI_Host_Earthquake { get; set; }
    }
}
