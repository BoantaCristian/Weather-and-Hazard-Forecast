using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAndHazardForecastAPI.Models.DataTransferObjects
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
