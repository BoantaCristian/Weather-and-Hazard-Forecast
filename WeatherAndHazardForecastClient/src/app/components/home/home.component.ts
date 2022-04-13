import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { WeatherHazardService } from 'src/app/services/weather-hazard.service';
import { WeatherDetailComponent } from '../dialogs/weather-detail/weather-detail.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  
  weatherForecast: any = {city_name: "Craiova",
  country_code: "RO",
  data: [
    {
clouds: 7,
max_temp: 19.9,
min_temp: 3.8,
pop: 0,
precip: 0,
pres: 998.3,
rh: 49,
snow: 0,
snow_depth: 0,
sunrise_ts: 1649217365,
sunset_ts: 1649264332,
temp: 11.5,
uv: 6.7,
valid_date: "2022-04-06",
vis: 24.128,
weather: {icon: 'c02d', code: 801, description: 'Few clouds'},
wind_cdir: "SSE",
wind_cdir_full: "south-southeast",
wind_dir: 155,
wind_gust_spd: 2.4,
wind_spd: 0.8},
{
clouds: 91,
max_temp: 12.7,
min_temp: 9.6,
pop: 65,
precip: 3.35938,
pres: 989.4,
rh: 67,
snow: 0,
snow_depth: 0,
sunrise_ts: 1649303657,
sunset_ts: 1649350806,
temp: 11.3,
uv: 2.1,
valid_date: "2022-04-07",
vis: 21.857,
weather: {icon: 'r04d', code: 520, description: 'Light shower rain'},
wind_cdir: "SE",
wind_cdir_full: "southeast",
wind_dir: 134,
wind_gust_spd: 5.7,
wind_spd: 2.2},
{

clouds: 41,
max_temp: 21.2,
min_temp: 7.2,
pop: 0,
precip: 0,
pres: 991.7,
rh: 61,
snow: 0,
snow_depth: 0,
sunrise_ts: 1649389949,
sunset_ts: 1649437281,
temp: 14,
uv: 6.6,
valid_date: "2022-04-08",
vis: 24.128,
weather: {icon: 'c03d', code: 803, description: 'Broken clouds'},
wind_cdir: "W",
wind_cdir_full: "west",
wind_dir: 267,
wind_gust_spd: 6.8,
wind_spd: 2.7},
{
clouds: 16,
max_temp: 23.7,
min_temp: 8.5,
pop: 20,
precip: 0.00195312,
pres: 989.5,
rh: 54,
snow: 0,
snow_depth: 0,
sunrise_ts: 1649476242,
sunset_ts: 1649523755,
temp: 16.2,
uv: 6.7,
valid_date: "2022-04-09",
vis: 24.128,
weather: {icon: 'c02d', code: 801, description: 'Few clouds'},
wind_cdir: "S",
wind_cdir_full: "south",
wind_dir: 172,
wind_gust_spd: 3.6,
wind_spd: 2.2},
{
clouds: 100,
max_temp: 18,
min_temp: 7.2,
pop: 40,
precip: 1.4375,
pres: 986.4,
rh: 65,
snow: 0,
snow_depth: 0,
sunrise_ts: 1649562535,
sunset_ts: 1649610229,
temp: 10.8,
uv: 2.2,
valid_date: "2022-04-10",
vis: 23.676,
weather: {icon: 'c04d', code: 804, description: 'Overcast clouds'},
wind_cdir: "W",
wind_cdir_full: "west",
wind_dir: 281,
wind_gust_spd: 10.7,
wind_spd: 6.7},
{
clouds: 51,
max_temp: 12.6,
min_temp: 5.3,
pop: 0,
precip: 0,
pres: 995.9,
rh: 45,
snow: 0,
snow_depth: 0,
sunrise_ts: 1649648829,
sunset_ts: 1649696703,
temp: 8.7,
uv: 7.4,
valid_date: "2022-04-11",
vis: 24.128,
weather: {icon: 'c03d', code: 803, description: 'Broken clouds'},
wind_cdir: "WNW",
wind_cdir_full: "west-northwest",
wind_dir: 297,
wind_gust_spd: 11.2,
wind_spd: 7.8}
  ],
  lat: 44.32,
  lon: 23.8,
  state_code: "17",
  timezone: "Europe/Bucharest"};
  currentDay: any = {clouds: 100,
    max_temp: 20.1,
    min_temp: 10.6,
    pop: 60,
    precip: 4.48071,
    pres: 983,
    rh: 63,
    snow: 0,
    snow_depth: 0,
    sunrise_ts: new Date(1648785910 *1000),
    sunset_ts: new Date(1648831960 *1000),
    temp: 17.4,
    uv: 0,
    valid_date: "2022-04-01",
    vis: 23.619,
    weather: {icon: 'r01d', code: 500, description: 'Light rain'},
    wind_cdir: "SSE",
    wind_cdir_full: "south-southeast",
    wind_dir: 167,
    wind_gust_spd: 7.2,
    wind_spd: 2.2};

  loggedUser: any = {};
  timeUpdated = new Date(); //:Date
  background = ""
  locationsFromControl = new FormControl();
  filteredLocations: Observable<any[]>;
  cityToSearch: string
  userLocations: any;
  updateCount: any = 0;

  constructor(private router: Router, private service: WeatherHazardService, private toastr: ToastrService, private dialog: MatDialog) { }

  ngOnInit() {
    this.getUser()
    this.updateBackground(this.currentDay.weather.icon)
    setTimeout(()=> this.getUserLocations(), 300)
    // setTimeout(()=> this.getCurrentLocationWeather(), 200)
  }

  authenticate(){
    this.router.navigateByUrl('/authentication/login')
  }

  getUser(){
    if(localStorage.getItem('token') != null){
      this.service.getUser().subscribe(
        (res: any) => {
          this.loggedUser = res
        },
        err => {
          localStorage.removeItem('token')
          localStorage.removeItem('role')
          this.router.navigateByUrl('/authentication/login')
        }
      )
    }
    else
      this.loggedUser = null
  }

  getCurrentLocationWeather(){
    if(navigator.geolocation){
      navigator.geolocation.getCurrentPosition(position => {        
        const options = {
          method: 'GET',
          headers: {
            'X-RapidAPI-Host': 'forward-reverse-geocoding.p.rapidapi.com',
            'X-RapidAPI-Key': 'e176ecea79mshfbe0789d9e16db2p12f4d4jsn8d5070f53406'
          }
        };
        
        fetch(`https://forward-reverse-geocoding.p.rapidapi.com/v1/reverse?lat=${position.coords.latitude}&lon=${position.coords.longitude}&accept-language=en&polygon_threshold=0.0`, options)
          .then(response => response.json())
          .then(response => {

            let userName
            if(!this.loggedUser)
              userName = "noUser"
            else
              userName = this.loggedUser.userName
            
            this.service.getLocalWeatherForecast(response.address.city, userName).subscribe(
              (res: any) => {
                this.weatherForecast = res
                this.currentDay = res.data[0]
                this.timeUpdated = new Date()
                this.convertSunriseSunsetToDateTime(res.data)
                this.updateBackground(res.data[0].weather.icon)
              }
            )
          })
          .catch(err => console.error(err));
      })
    }
    else{
      console.log("Geolocation not supported!")
      this.toastr.error('Geolocation not supported!')
    }
  }

  getWeatherByLocation(city){
    let userName
    if(!this.loggedUser)
      userName = "noUser"
    else
      userName = this.loggedUser.userName
    this.service.getLocalWeatherForecast(city, userName).subscribe(
      (res: any) => {
        this.weatherForecast = res
        this.currentDay = res.data[0]
        this.timeUpdated = new Date()
        this.getUserLocations()
        setTimeout(()=> this.updateBackground(res.data[0].weather.icon), 300)
        setTimeout(()=> this.convertSunriseSunsetToDateTime(res.data), 300)
        
        this.locationsFromControl.reset()
        if(this.loggedUser)
          document.getElementById("searchLocation").blur()
      }
    )
  }

  getUserLocations(){
    if(localStorage.getItem('token') != null){
      this.service.getUserLocations(this.loggedUser.userName).subscribe(
        res => {
          this.userLocations = res
          setTimeout(() => { this.fetchLocationsForAutocomplete() }, 200);
        }
        )
      }
    }

  private _filter(value: any): any[] {
    const filterValue = (value || '').toLowerCase();
    return this.userLocations.filter(location => location.toLowerCase().includes(filterValue));
  }

  fetchLocationsForAutocomplete(){
    this.filteredLocations = this.locationsFromControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value)),
    );
  }

  stopPropagetion(event){
    event.stopPropagation()
  }
  
  update(event){
    event.stopPropagation()
    this.getWeatherByLocation(this.weatherForecast.city_name)
    this.updateCount++
    var rotate = 360 * this.updateCount
    document.getElementById("updateButton").style.transform = `rotate(${rotate}deg)`
  }

  openWeatherDetailsDialog(day){
    var weatherDetailsDialog = this.dialog.open(WeatherDetailComponent, { data: day})
    weatherDetailsDialog.afterClosed().subscribe(() => {})
  }

  convertSunriseSunsetToDateTime(data){
    data.forEach(day => {
      day.sunrise_ts = new Date(day.sunrise_ts *1000)
      day.sunset_ts = new Date(day.sunset_ts *1000)
    });
  }

  updateBackground(backgroundIconCode){
    switch (backgroundIconCode) {
      case 'r02d': case 'r03d': case 'f01d': case 'r05d': case 'r06d': case 'u00d':
        this.background = `../../../assets/backgrounds/rainDay.jpg`
        break;
      case 'r01n': case 'r02n': case 'r03n': case 'f01n': case 'r04n': case 'r05n': case 'r06n': case 'd01n': case 'd02n': case 'd03n': case 'u00n':
        this.background = `../../../assets/backgrounds/rainNight.jpg`
        break;
      case 's01d': case 's02d': case 's03d': case 's04d': case 's05d': case 's06n':
        this.background = `../../../assets/backgrounds/snowDay4.jpg`
        break;
      case 's01n': case 's02n': case 's03n': case 's04n': case 's05n': case 's06n':
        this.background = `../../../assets/backgrounds/snowNight.jpg`
        break;
      case 'a01d': case 'a02d': case 'a03d': case 'a04d': case 'a05d': case 'a06d':
        this.background = `../../../assets/backgrounds/mistDay.jpg`
        break;
      case 'a01n': case 'a02n': case 'a03n': case 'a04n': case 'a05n': case 'a06n':
        this.background = `../../../assets/backgrounds/mistNight.jpg`
        break;
      case 'c02d': case 'c03d': case 'c04d':
        this.background = `../../../assets/backgrounds/cloudsDay.jpg`
        break;
      case 'c02n': case 'c03n': case 'c04n':
        this.background = `../../../assets/backgrounds/cloudsNight.jpg`
        break;
      case 'd01d': case 'd02d': case 'd03d': case 'r01d': case 'r04d':
        this.background = `../../../assets/backgrounds/lightRainDay.jpg`
        break;
      case 't01d': case 't02d': case 't03d': case 't04d': case 't05d': case 't01n': case 't02n': case 't03n': case 't04n': case 't05n':
        this.background = `../../../assets/backgrounds/thunderstormNight.jpg`
        break;
      case 'c01n':
        this.background = `../../../assets/backgrounds/night.jpg`
        break;
      case 'c01d':
        this.background = `../../../assets/backgrounds/day4.jpg`
        break;
      default:
        break;
      }
      
      localStorage.setItem('background', this.background)
  }


}
