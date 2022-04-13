import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WeatherHazardService } from 'src/app/services/weather-hazard.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent implements OnInit {
  
  loggedUser: any = {}
  
  constructor(private service: WeatherHazardService, private router: Router) { }

  ngOnInit() {
    this.getUser()
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

  logout(){
    localStorage.removeItem('token')
    localStorage.removeItem('role')
    this.router.navigateByUrl('/authentication/login')
  }

  navigate(option){
    if(option == 'admin')
      this.router.navigateByUrl('admin')
    if(option != 'admin')
      this.router.navigateByUrl(`/hazard/${option}`)
    if(option == '')
      this.router.navigateByUrl('')

    localStorage.setItem('page', option)
  }

}
