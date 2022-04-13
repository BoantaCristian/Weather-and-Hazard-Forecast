import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  background: string = ''

  constructor() { }

  ngOnInit() {
    this.setBackground()
  }

  setBackground(){
    this.background = localStorage.getItem('background')
  }

}
