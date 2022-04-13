using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DataTransferObjects
{
    public class Covid
    {
        [JsonProperty("Active Cases_text")]
        public string Active_Cases_text { get; set; }

        [JsonProperty("Country_text")]
        public string Country_text { get; set; }

        [JsonProperty("Last Update")]
        public string Last_Update { get; set; }

        [JsonProperty("New Cases_text")]
        public string New_Cases_text { get; set; }

        [JsonProperty("New Deaths_text")]
        public string New_Deaths_text { get; set; }

        [JsonProperty("Total Cases_text")]
        public string Total_Cases_text { get; set; }

        [JsonProperty("Total Deaths_text")]
        public string Total_Deaths_text { get; set; }

        [JsonProperty("Total Recovered_text")]
        public string Total_Recovered_text { get; set; }
    }
}
