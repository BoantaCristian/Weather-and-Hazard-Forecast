using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DataTransferObjects
{
    public class DayWeather
    {
        public WeatherDescription weather { get; set; }
        public string valid_date { get; set; }
        public double temp { get; set; }
        public double max_temp { get; set; }
        public double min_temp { get; set; }
        public double wind_spd { get; set; }
        public double wind_gust_spd { get; set; }
        public string wind_cdir { get; set; }
        public string wind_cdir_full { get; set; }
        public int wind_dir { get; set; }
        public double clouds { get; set; }
        public double snow { get; set; }
        public double snow_depth { get; set; }
        public double uv { get; set; }
        public int rh { get; set; }
        public double precip { get; set; }
        public int pop { get; set; }
        public double pres { get; set; }
        public double vis { get; set; }
        public long sunrise_ts { get; set; }
        public long sunset_ts { get; set; }
    }
}
