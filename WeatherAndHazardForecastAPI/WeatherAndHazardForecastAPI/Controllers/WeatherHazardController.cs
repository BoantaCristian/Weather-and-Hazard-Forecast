using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeatherAndHazardForecastAPI.Models.ApplicationContext;
using WeatherAndHazardForecastAPI.Models.DbModels;

namespace WeatherAndHazardForecastAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherHazardController : ControllerBase
    {
        private ApplicationContext _context;
        private UserManager<User> _userManager;

        public WeatherHazardController(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
    }


}