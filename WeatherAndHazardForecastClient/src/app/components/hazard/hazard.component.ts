import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { WeatherHazardService } from 'src/app/services/weather-hazard.service';

@Component({
  selector: 'app-hazard',
  templateUrl: './hazard.component.html',
  styleUrls: ['./hazard.component.css']
})
export class HazardComponent implements OnInit {

  background: string = ''
  loggedUser: any = {};

  weatherAlerts: MatTableDataSource<any>;
  covid: MatTableDataSource<any>;
  currentLocationCovid: MatTableDataSource<any>;
  weatherAlertsColumns: string[] = ["title", "city_name", "country_Name", "regions", "local_Start_Date", "local_End_Date", "timezone", "actions"]
  covidColumns: string[] = ['country_text', 'new_Cases_text', 'new_Deaths_text', 'total_Cases_text', 'total_Deaths_text', 'total_Recovered_text', 'last_Update'];
  currentPage: string = '';
  updateCount: number = 0;
  searchKey: string = '';
  
  covidFromControl = new FormControl();
  filteredCovidLocations: Observable<any[]>;
  covidLocationToSearch: string
  userLocations: any;

  constructor(private service: WeatherHazardService, private router: Router, private activatedRoute: ActivatedRoute, private toastr: ToastrService) {
    router.events.subscribe((val) => {
      this.checkPage()
     });
   }

  @ViewChild(MatSort, {static: false}) sort: MatSort
  @ViewChild('covidPaginator', {read: MatPaginator, static: false}) covidPaginator: MatPaginator;
  @ViewChild('weatherAlertsPaginator', {read: MatPaginator, static: false}) weatherAlertsPaginator: MatPaginator;

  ngOnInit() {
    this.getUser()
    this.checkPage()
    setTimeout(() => this.getCurrentLocation(), 300)
    setTimeout(() => this.getUserSevereWeatherAlerts(), 300)
    setTimeout(() => this.getUserCovid(), 300)
    setTimeout(() => this.getUserCovidLocations(), 300)
    this.setBackground()
  }

  getCurrentLocation(){
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
          .then((response: any) => {
            this.service.getCovid(response.address.country, this.loggedUser.userName).subscribe(
              (res : any) => {
                this.currentLocationCovid = new MatTableDataSource(res)
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

  getUserSevereWeatherAlerts(){
    this.service.getUserSevereWeatherArchive(this.loggedUser.userName).subscribe(
      (res: any) => {
        this.weatherAlerts = new MatTableDataSource(res)
        setTimeout(()=> this.fetchDataTable(), 200)
      }
    )
  }

  getUserCovid(){
    this.service.getUserCovidArchive(this.loggedUser.userName).subscribe(
      (res: any) => {
        this.covid = new MatTableDataSource(res)
        setTimeout(()=> this.fetchDataTable(), 200)
      }
    )
  }

  getUserCovidLocations(){
    this.service.getUserLocations(this.loggedUser.userName).subscribe(
      (res: any) => {
        this.userLocations = res
        setTimeout(() => { this.fetchLocationsForAutocomplete() }, 200);
      }
    )
  }

  selectCovidLocation(location){
    this.service.getCovid(location, this.loggedUser.userName).subscribe(
      (res : any) => {
        this.currentLocationCovid = new MatTableDataSource(res)
        setTimeout(() => {
          this.fetchDataTable()
          this.fetchLocationsForAutocomplete()
        }, 200);
        this.covidFromControl.reset()
        document.getElementById("selectCovidLocation").blur()
      },
      err => {
        console.log(err)
      }
    )
  }

  updateCovid(covid){
    this.updateCount++
    var rotate = 360 * this.updateCount
    document.getElementById(`${covid.id}`).style.transform = `rotate(${rotate}deg)`
    console.log(covid.country_text, this.loggedUser.userName)
    this.service.getCovid(covid.country_text, this.loggedUser.userName).subscribe(
      (res: any) => {
        this.covid = new MatTableDataSource(res)
        setTimeout(() => { this.fetchDataTable() }, 200);
      }
    )
  }

  fetchDataTable(){
    this.covid.sort = this.sort
    this.covid.paginator = this.covidPaginator
  }

  onSearchClear(){
    this.searchKey = ""
    this.search('covid')
  }

  search(option){
      switch (option) {
        case 'covid':
          this.covid.filter = this.searchKey.trim().toLocaleLowerCase()
          break;
        default:
          break;
      }
  }

  private _filter(value: any): any[] {
    const filterValue = (value || '').toLowerCase();
    return this.userLocations.filter(location => location.toLowerCase().includes(filterValue));
  }

  fetchLocationsForAutocomplete(){
    this.filteredCovidLocations = this.covidFromControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value)),
    );
  }

  checkPage(){
    this.activatedRoute.params.subscribe(parameter => {
      this.currentPage = parameter.hazardType
    })
  }

  setBackground(){
    this.background = localStorage.getItem('background')
  }

}
