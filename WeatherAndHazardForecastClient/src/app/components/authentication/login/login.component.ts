import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { WeatherHazardService } from 'src/app/services/weather-hazard.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private form: FormBuilder, private service: WeatherHazardService, private router: Router, private toastr: ToastrService) { }

  loginForm = this.form.group({
    UserName: ['', Validators.required],
    Password: ['', Validators.required]
  })
  
  hide: boolean = true;

  ngOnInit() {
  }

  login(){
    this.service.login(this.loginForm.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token)
        this.getUser()
        setTimeout(() => this.router.navigateByUrl(''), 100);
        setTimeout(() => this.toastr.success(`Login Successful!`), 200);
      },
      err => {
        if(err.status == 400){
          this.toastr.error(`Incorrect email or password`, 'Falied!')
        }
      }
    )
  }

  getUser(){
    if(localStorage.getItem('token') != null){
      this.service.getUser().subscribe(
        (res: any) => {
          localStorage.setItem('role', res.role)
        },
        err => {
          localStorage.removeItem('token')
          localStorage.removeItem('role')
          this.router.navigateByUrl('/authentication/login')
        }
      )
    }
  }

}
