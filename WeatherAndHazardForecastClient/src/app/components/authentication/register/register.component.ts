import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { WeatherHazardService } from 'src/app/services/weather-hazard.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private service: WeatherHazardService,  private toastr: ToastrService) { }

  registerForm = this.formBuilder.group({
    UserName: ['', Validators.required],
    Email: ['', [Validators.required, Validators.email]],
    Password: ['', [Validators.required, Validators.minLength(4)]],
    ConfirmPassword: ['', Validators.required]
  })

  mismatch: boolean = false
  hide: boolean = true;
  
  ngOnInit() {
  }
  
  comparePasswords(){
    if(this.registerForm.value.Password != this.registerForm.value.ConfirmPassword){
      this.mismatch = true
    }
    else{
      this.mismatch = false
    }
  }

  register(){
    var body: any = {
      UserName: this.registerForm.value.UserName,
      Email: this.registerForm.value.Email,
      Password: this.registerForm.value.Password,
      Role: 'Client'
    }
    
    this.service.register(body).subscribe(
      (res:any) => {
        if(res.succeeded){
          this.toastr.success(`User ${body.UserName} created`,'Success!')
          this.registerForm.reset()
        }
        else {
          res.errors.forEach(element => {
            if(element.code == 'DuplicateUserName')
              this.toastr.error('Username already taken', 'Register Failed!')
            else{
              this.toastr.error(`${element.description}`, 'Register Failed!')
            }
          });
        }
      },
      err => {
        this.toastr.error(`${err.error.message}`, 'Register Failed!')
        console.log(err)
      }
    )
  }

}
