import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {
  
  background: string = ''

  constructor() { }

  ngOnInit() {
    this.setBackground()
  }

  setBackground(){
    this.background = localStorage.getItem('background')
  }

}
