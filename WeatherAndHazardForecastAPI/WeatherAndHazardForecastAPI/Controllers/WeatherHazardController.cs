using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using WeatherAndHazardForecastAPI.Models.ApplicationContext;
using WeatherAndHazardForecastAPI.Models.ApplicationSettings;
using WeatherAndHazardForecastAPI.Models.DataTransferObjects;
using WeatherAndHazardForecastAPI.Models.DbModels;

namespace WeatherAndHazardForecastAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherHazardController : ControllerBase
    {
        private ApplicationContext _context;
        private UserManager<User> _userManager;
        private ApplicationSettings _appSettings;

        public WeatherHazardController(ApplicationContext context, UserManager<User> userManager, IOptions<ApplicationSettings> appSettings)
        {
            _context = context;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpGet("{address}/{userName}")]
        [Route("GetWeatherForecast/{address}/{userName}")]
        public async Task<IActionResult> GetWeatherForecast(string address, string userName)
        {
            Geocoding geocoding = await GetGeocoding(address);
            var user = await _userManager.FindByNameAsync(userName);

            if (geocoding is null)
                return BadRequest(new { message = "Incorrect address!" });

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://weatherbit-v1-mashape.p.rapidapi.com/forecast/daily?lat={geocoding.lat}&lon={geocoding.lon}&units=metric"),
                Headers =
                {
                    { "X-RapidAPI-Host", _appSettings.X_RapidAPI_Host_Weather },
                    { "X-RapidAPI-Key", _appSettings.X_RapidAPI_Key },
                },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    if (body != "{}")
                    {
                        var weatherForecastJSON = JsonConvert.DeserializeObject<JToken>(body);
                        var weatherForecast = weatherForecastJSON.ToObject<Weatherbit>();
                        weatherForecast.data = weatherForecast.data.GetRange(0, 6);

                        await SaveToWeatherArchive(weatherForecast);
                        await CheckLocation(user, weatherForecast.city_name);

                        return Ok(weatherForecast);
                    }
                    else
                        return BadRequest(new { message = "Incorrect latitude or longitude!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "To many requests!", exception = e });
                throw e;
            }
        }

        [HttpGet("{address}/{userName}")]
        [Authorize]
        [Route("GetWeatherAlert/{address}/{userName}")]
        public async Task<IActionResult> GetWeatherAlert(string address, string userName)
        {
            Geocoding geocoding = await GetGeocoding(address);
            var user = await _userManager.FindByNameAsync(userName);

            if (geocoding is null)
                return BadRequest(new { message = "Incorrect address!" });

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://weatherbit-v1-mashape.p.rapidapi.com/alerts?lat={geocoding.lat}&lon={geocoding.lon}&units=metric"),
                Headers =
                {
                    { "X-RapidAPI-Host", _appSettings.X_RapidAPI_Host_Weather },
                    { "X-RapidAPI-Key", _appSettings.X_RapidAPI_Key },
                },
            };
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    if (body != "{}")
                    {
                        var weatherAlertJSON = JsonConvert.DeserializeObject<JToken>(body);
                        var weatherAlert = weatherAlertJSON.ToObject<SevereWeather>();

                        await SaveToSevereWeatherArchive(weatherAlert, userName);
                        await CheckLocation(user, weatherAlert.city_name);

                        return Ok(weatherAlert);
                    }
                    else
                        return BadRequest(new { message = "Incorrect latitude or longitude!" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetSevereWeatherArchive")]
        public IActionResult GetSevereWeatherArchive()
        {
            var servereWeather = _context.SevereWeathers.Include(i => i.User).Include(i => i.Location).Select(s => new { s.Title, s.Description, s.Regions, s.Local_Start_Date, s.Local_End_Date, s.Location.City_name, s.Location.Timezone, s.Location.Country_Name, s.User.UserName });

            return Ok(servereWeather);
        }

        [HttpGet("{userName}")]
        [Authorize]
        [Route("GetUserSevereWeatherArchive/{userName}")]
        public IActionResult GetUserSevereWeatherArchive(string userName)
        {
            var servereWeather = _context.SevereWeathers.Include(i => i.User).Include(i => i.Location).Where(w => w.User.UserName == userName).Select(s => new { s.Title, s.Description, s.Regions, s.Local_Start_Date, s.Local_End_Date, s.Location.City_name, s.Location.Timezone, s.Location.Country_Name, s.User.UserName });

            return Ok(servereWeather);
        }

        public async Task<Geocoding> GetGeocoding(string address)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://forward-reverse-geocoding.p.rapidapi.com/v1/search?q={address}&accept-language=en&polygon_threshold=0.0"),
                Headers =
                {
                    { "X-RapidAPI-Host", _appSettings.X_RapidAPI_Host_Geocoding },
                    { "X-RapidAPI-Key", _appSettings.X_RapidAPI_Key },
                },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    if (body != "{}")
                    {
                        var geolocationJSON = JsonConvert.DeserializeObject<JToken>(body);
                        var geolocation = (geolocationJSON.ToObject<List<Geocoding>>()).First();

                        return geolocation;
                    }
                    else
                        return null;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public async Task CheckLocation(User user, string city_name)
        {
            var location = await _context.Locations.Where(w => w.City_name == city_name).FirstAsync();

            var locationExists = false;
            foreach (var userLocation in _context.UserLocations.Include(i => i.User).Include(i => i.Location))
            {
                if(userLocation.User == user && userLocation.Location == location)
                {
                    locationExists = true;
                    break;
                }
            }

            if(!locationExists && user != null && location != null)
            {
                var newlocationUser = new UserLocation
                {
                    User = user,
                    Location = location
                };

                await _context.UserLocations.AddAsync(newlocationUser);
                await _context.SaveChangesAsync();
            }
        }
        public async Task SaveToWeatherArchive(Weatherbit weatherForecast)
        {
            var currentLocation = new Location();

            var locationExists = false;
            foreach (var location in _context.Locations)
            {
                if(location.City_name == weatherForecast.city_name)
                {
                    locationExists = true;
                    currentLocation = location;
                    break;
                }
            }
            if (!locationExists)
            {
                currentLocation.City_name = weatherForecast.city_name;
                currentLocation.Longitude = weatherForecast.lon;
                currentLocation.Latitude = weatherForecast.lat;
                currentLocation.Timezone = weatherForecast.timezone;
                currentLocation.Country_Name = await GetCountry(weatherForecast.country_code);
                currentLocation.Country_code = weatherForecast.country_code;
                currentLocation.State_code = weatherForecast.state_code;

                await _context.Locations.AddAsync(currentLocation);
                await _context.SaveChangesAsync();
            }

            foreach (var day in weatherForecast.data)
            {
                var dayArchive = new WeatherArchive();
                var dayExists = false;
                foreach (var archive in _context.WeatherArchives.Include(i => i.Location))
                {
                    if (day.valid_date == archive.Valid_date && archive.Location.City_name == weatherForecast.city_name)
                    {
                        dayArchive = archive;
                        dayExists = true;
                        break;
                    }
                }
                if (!dayExists)
                {
                    var newWeatherArchive = new WeatherArchive
                    {
                        Description = day.weather.description,
                        Image = day.weather.icon,
                        Valid_date = day.valid_date,
                        Temperature = day.temp,
                        MinTemperature = day.min_temp,
                        MaxTemperature = day.max_temp,
                        Wind_Speed = day.wind_spd,
                        Wind_Gust_Speed = day.wind_gust_spd,
                        Wind_Direction = day.wind_cdir,
                        Wind_Direction_Full = day.wind_cdir_full,
                        Wind_Direction_Degrees = day.wind_dir,
                        Clouds = day.clouds,
                        Snow = day.snow,
                        Snow_Depth = day.snow_depth,
                        UV_Index = day.uv,
                        Relative_Humidity = day.rh,
                        Precipitations = day.precip,
                        Probability_Precipitation = day.pop,
                        Pressure = day.pres,
                        Visibility = day.vis,
                        Sunrise = day.sunrise_ts,
                        Sunset = day.sunset_ts,
                        Location = currentLocation

                    };

                    await _context.WeatherArchives.AddAsync(newWeatherArchive);
                }
                else
                {
                    dayArchive.Description = day.weather.description;
                    dayArchive.Image = day.weather.icon;
                    dayArchive.Valid_date = day.valid_date;
                    dayArchive.Temperature = day.temp;
                    dayArchive.MinTemperature = day.min_temp;
                    dayArchive.MaxTemperature = day.max_temp;
                    dayArchive.Wind_Speed = day.wind_spd;
                    dayArchive.Wind_Gust_Speed = day.wind_gust_spd;
                    dayArchive.Wind_Direction = day.wind_cdir;
                    dayArchive.Wind_Direction_Full = day.wind_cdir_full;
                    dayArchive.Wind_Direction_Degrees = day.wind_dir;
                    dayArchive.Clouds = day.clouds;
                    dayArchive.Snow = day.snow;
                    dayArchive.Snow_Depth = day.snow_depth;
                    dayArchive.UV_Index = day.uv;
                    dayArchive.Relative_Humidity = day.rh;
                    dayArchive.Precipitations = day.precip;
                    dayArchive.Probability_Precipitation = day.pop;
                    dayArchive.Pressure = day.pres;
                    dayArchive.Visibility = day.vis;
                    dayArchive.Sunrise = day.sunrise_ts;
                    dayArchive.Sunset = day.sunset_ts;
                    dayArchive.Location = currentLocation;

                    _context.Entry(dayArchive).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task SaveToSevereWeatherArchive(SevereWeather weatherAlert, string userName)
        {
            var currentLocation = new Location();
            var user = await _userManager.FindByNameAsync(userName);

            var locationExists = false;
            foreach (var location in _context.Locations)
            {
                if (location.City_name == weatherAlert.city_name)
                {
                    locationExists = true;
                    currentLocation = location;
                    break;
                }
            }
            if (!locationExists)
            {
                currentLocation.City_name = weatherAlert.city_name;
                currentLocation.Longitude = weatherAlert.lon;
                currentLocation.Latitude = weatherAlert.lat;
                currentLocation.Timezone = weatherAlert.timezone;
                currentLocation.Country_Name = await GetCountry(weatherAlert.country_code);
                currentLocation.Country_code = weatherAlert.country_code;
                currentLocation.State_code = weatherAlert.state_code;

                await _context.Locations.AddAsync(currentLocation);
                await _context.SaveChangesAsync();
            }

            foreach (var alert in weatherAlert.alerts)
            {
                var alertArchive = new SevereWeatherArchive();
                var alertExists = false;
                foreach (var archive in _context.SevereWeathers)
                {
                    if(archive.Description == alert.description)
                    {
                        alertArchive = archive;
                        alertExists = true;
                        break;
                    }
                }
                if (!alertExists)
                {
                    var newSevereWeatherArchive = new SevereWeatherArchive
                    {
                        Title = alert.title,
                        Description = alert.description,
                        Regions = string.Join(" ", alert.regions),
                        Severity = alert.severity,
                        Local_End_Date = alert.effective_local,
                        Local_Start_Date = alert.expires_local,
                        Location = currentLocation,
                        User = user
                    };

                    await _context.SevereWeathers.AddAsync(newSevereWeatherArchive);
                }
                if (alertExists)
                {
                    alertArchive.Title = alert.title;
                    alertArchive.Description = alert.description;
                    alertArchive.Regions = string.Join(" ", alert.regions);
                    alertArchive.Severity = alert.severity;
                    alertArchive.Local_End_Date = alert.effective_local;
                    alertArchive.Local_Start_Date = alert.expires_local;
                    alertArchive.Location = currentLocation;
                    alertArchive.User = user;

                    _context.Entry(alertArchive).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();
        }

        [HttpGet]
        [Authorize]
        [Route("GetEarthquakes")]
        public async Task<object> GetEarthquakes()
        {
            var recentEarthquakes = await GetRecentEarthquakes();
            var significantEarthquakes = await GetSignificantEarthquakes();

            if (recentEarthquakes.Count() == 0)
                return BadRequest(new { message = "Requests limit reached!" });

            await SaveToEarthquakeArchive(recentEarthquakes, significantEarthquakes);

            return Ok(new { recentEarthquakes, significantEarthquakes });
        }

        public async Task SaveToEarthquakeArchive(List<RecentEarthquakes> recentEarthquakes, List<SignificantEarthquakes> significantEarthquakes)
        {
            foreach (var recentEarthquake in recentEarthquakes)
            {
                var recentEarthquakeArchive = new Earthquake();
                var earthquakeExists = false;
                foreach (var earthquake in _context.Earthquakes)
                {
                    if(earthquake.Location == recentEarthquake.location && earthquake.Date == recentEarthquake.date)
                    {
                        recentEarthquakeArchive = earthquake;
                        earthquakeExists = true;
                        break;
                    }
                }
                if (!earthquakeExists)
                {
                    var newEarthquake = new Earthquake
                    {
                        Date = recentEarthquake.date,
                        Time = recentEarthquake.time,
                        Time_ago = recentEarthquake.time_ago,
                        Latitude = recentEarthquake.latitude,
                        Longitude = recentEarthquake.longitude,
                        Depth = recentEarthquake.depth,
                        Magnitude = recentEarthquake.magnitude,
                        Location = recentEarthquake.location,
                        Status = "Recent"
                    };

                    await _context.Earthquakes.AddAsync(newEarthquake);
                }
                else
                {
                    recentEarthquakeArchive.Date = recentEarthquake.date;
                    recentEarthquakeArchive.Time = recentEarthquake.time;
                    recentEarthquakeArchive.Time_ago = recentEarthquake.time_ago;
                    recentEarthquakeArchive.Latitude = recentEarthquake.latitude;
                    recentEarthquakeArchive.Longitude = recentEarthquake.longitude;
                    recentEarthquakeArchive.Depth = recentEarthquake.depth;
                    recentEarthquakeArchive.Magnitude = recentEarthquake.magnitude;
                    recentEarthquakeArchive.Location = recentEarthquake.location;

                    _context.Entry(recentEarthquakeArchive).State = EntityState.Modified;
                }
            }

            foreach (var significantEarthquake in significantEarthquakes)
            {
                var significantEarthquakeArchive = new Earthquake();
                var earthquakeExists = false;
                foreach (var earthquake in _context.Earthquakes)
                {
                    if (earthquake.Location == significantEarthquake.location && earthquake.Time == significantEarthquake.time)
                    {
                        significantEarthquakeArchive = earthquake;
                        earthquakeExists = true;
                        break;
                    }
                }
                if (!earthquakeExists)
                {
                    var newEarthquake = new Earthquake
                    {
                        Time = significantEarthquake.time,
                        Timezone = significantEarthquake.timezone,
                        Time_ago = significantEarthquake.time_ago,
                        Location = significantEarthquake.location,
                        Region = significantEarthquake.region,
                        Nearest_city = significantEarthquake.nearest_city,
                        Magnitude = significantEarthquake.magnitude,
                        Effects = significantEarthquake.effects,
                        Status = "Significant"
                    };

                    await _context.Earthquakes.AddAsync(newEarthquake);
                }
                else
                {
                    significantEarthquakeArchive.Time = significantEarthquake.time;
                    significantEarthquakeArchive.Timezone = significantEarthquake.timezone;
                    significantEarthquakeArchive.Time_ago = significantEarthquake.time_ago;
                    significantEarthquakeArchive.Location = significantEarthquake.location;
                    significantEarthquakeArchive.Region = significantEarthquake.region;
                    significantEarthquakeArchive.Nearest_city = significantEarthquake.nearest_city;
                    significantEarthquakeArchive.Magnitude = significantEarthquake.magnitude;
                    significantEarthquakeArchive.Effects = significantEarthquake.effects;

                    _context.Entry(significantEarthquake).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<RecentEarthquakes>> GetRecentEarthquakes()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://earthquake-monitor.p.rapidapi.com/recent"),
                Headers =
                {
                    { "X-RapidAPI-Host", _appSettings.X_RapidAPI_Host_Earthquake },
                    { "X-RapidAPI-Key", _appSettings.X_RapidAPI_Key },
                },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    var recentEarthquakesJSON = JsonConvert.DeserializeObject<JToken>(body);
                    var recentEarthquakes = recentEarthquakesJSON.ToObject<List<RecentEarthquakes>>();
                    recentEarthquakes = recentEarthquakes.GetRange(0, 10);

                    return recentEarthquakes;
                }
            }
            catch (Exception)
            {
                return new List<RecentEarthquakes>();
                
            }
        }
        
        public async Task<List<SignificantEarthquakes>> GetSignificantEarthquakes()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://earthquake-monitor.p.rapidapi.com/significant"),
                Headers =
                {
                    { "X-RapidAPI-Host", _appSettings.X_RapidAPI_Host_Earthquake },
                    { "X-RapidAPI-Key", _appSettings.X_RapidAPI_Key },
                },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    var significantEarthquakesJSON = JsonConvert.DeserializeObject<JToken>(body);
                    var significantEarthquakes = significantEarthquakesJSON.ToObject<List<SignificantEarthquakes>>();

                    return significantEarthquakes;
                }
            }
            catch (Exception)
            {
                return new List<SignificantEarthquakes>();
            }
        }

        [HttpGet("{country}/{userName}")]
        [Authorize]
        [Route("GetCovid/{country}/{userName}")]
        public async Task<IActionResult> GetCovid(string country, string userName)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://covid-19-tracking.p.rapidapi.com/v1/{country}"),
                Headers =
                {
                    { "X-RapidAPI-Host", "covid-19-tracking.p.rapidapi.com" },
                    { "X-RapidAPI-Key", _appSettings.X_RapidAPI_Key },
                },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    var covidJSON = JsonConvert.DeserializeObject<JToken>(body);
                    var covid = covidJSON.ToObject<Covid>();

                    await SaveToCovidArchive(covid, userName);
                    var updatedCovid = _context.CovidArchive.Where(w => w.Country_text == covid.Country_text).Select(s => new { s.Active_Cases_text, s.Country_text, s.Last_Update, s.New_Cases_text, s.New_Deaths_text, s.Total_Cases_text, s.Total_Deaths_text, s.Total_Recovered_text });
                    
                    return Ok(updatedCovid);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Not found!" });
                throw e;

            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetCovidArchive")]
        public IActionResult GetCovidArchive()
        {
            var covid = _context.CovidArchive.Include(i => i.User).Select(s => new { s.Active_Cases_text, s.Country_text, s.Last_Update, s.New_Cases_text, s.New_Deaths_text, s.Total_Cases_text, s.Total_Deaths_text, s.Total_Recovered_text, s.User.UserName });

            return Ok(covid);
        }
        
        [HttpGet("{userName}")]
        [Authorize]
        [Route("GetUserCovidArchive/{userName}")]
        public IActionResult GetUserCovidArchive(string userName)
        {
            var covid = _context.CovidArchive.Include(i => i.User).Where(w => w.User.UserName == userName).Select(s => new { s.Active_Cases_text, s.Country_text, s.Last_Update, s.New_Cases_text, s.New_Deaths_text, s.Total_Cases_text, s.Total_Deaths_text, s.Total_Recovered_text, s.User.UserName, s.Id });

            return Ok(covid);
        }

        public async Task SaveToCovidArchive(Covid covid, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var covidArchive = new CovidArchive();
            var countryExists = false;
            foreach (var cov in _context.CovidArchive)
            {
                if(cov.Country_text == covid.Country_text)
                {
                    covidArchive = cov;
                    countryExists = true;
                    break;
                }
            }

            if (!countryExists)
            {
                var newCovidCountry = new CovidArchive
                {
                    Active_Cases_text = covid.Active_Cases_text,
                    Country_text = covid.Country_text,
                    Last_Update = covid.Last_Update,
                    New_Cases_text = covid.New_Cases_text,
                    New_Deaths_text = covid.New_Deaths_text,
                    Total_Cases_text = covid.Total_Cases_text,
                    Total_Deaths_text = covid.Total_Deaths_text,
                    Total_Recovered_text = covid.Total_Recovered_text,
                    User = user
                };

                await _context.CovidArchive.AddAsync(newCovidCountry);
            }
            else
            {
                covidArchive.Active_Cases_text = covid.Active_Cases_text;
                covidArchive.Country_text = covid.Country_text;
                covidArchive.Last_Update = covid.Last_Update;
                covidArchive.New_Cases_text = covid.New_Cases_text;
                covidArchive.New_Deaths_text = covid.New_Deaths_text;
                covidArchive.Total_Cases_text = covid.Total_Cases_text;
                covidArchive.Total_Deaths_text = covid.Total_Deaths_text;
                covidArchive.Total_Recovered_text = covid.Total_Recovered_text;
                covidArchive.User = user;

                _context.Entry(covidArchive).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

        }

        public async Task<string> GetCountry(string country_code)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://wft-geo-db.p.rapidapi.com/v1/geo/countries/{country_code}"),
                Headers =
                {
                    { "X-RapidAPI-Host", "wft-geo-db.p.rapidapi.com" },
                    { "X-RapidAPI-Key", "e176ecea79mshfbe0789d9e16db2p12f4d4jsn8d5070f53406" },
                },
            };
           
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    var countryJSON = JsonConvert.DeserializeObject<JToken>(body);
                    var country = countryJSON.ToObject<Country>();

                    if (response.IsSuccessStatusCode)
                    {
                        return country.data.name;
                    }
                    else
                        return "";

                }
            }
            catch (Exception)
            {
                return "";

            }
        }

        [HttpGet("{userName}")]
        [Authorize]
        [Route("GetUserLocations/{userName}")]
        public async Task<IActionResult> GetLocationOfUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if(user != null)
            {
                var location = _context.UserLocations.Include(i => i.Location).Include(i => i.User).Where(w => w.User == user).Select(s => s.Location.City_name);

                return Ok(location);
            }

            return BadRequest(new { message = "User not found!" });
        }
    }

}