import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class WeatherHazardService {

  URL = 'http://localhost:59506/api'

  constructor(private http: HttpClient) { }
  
  register(body){
    return this.http.post(`${this.URL}/User/Register`, body)
  }
  login(body){
    return this.http.post(`${this.URL}/User/Login`, body)
  }
  getUser(){
    var token = new HttpHeaders({'Authorization': 'Bearer '+ localStorage.getItem('token')})
    return this.http.get(`${this.URL}/User/GetUser`, {headers: token})
  }
  getUsers(){
    var token = new HttpHeaders({'Authorization': 'Bearer '+ localStorage.getItem('token')})
    return this.http.get(`${this.URL}/User/GetUsers`, {headers: token})
  }
  getUserWithLocations(){
    var token = new HttpHeaders({'Authorization': 'Bearer '+ localStorage.getItem('token')})
    return this.http.get(`${this.URL}/User/GetUserWithLocations`, {headers: token})
  }
  getUserLocations(userName){
    var token = new HttpHeaders({'Authorization': 'Bearer '+ localStorage.getItem('token')})
    return this.http.get(`${this.URL}/WeatherHazard/GetUserLocations/${userName}`, {headers: token})
  }
  getLocalWeatherForecast(address, userName){
    return this.http.get(`${this.URL}/WeatherHazard/GetWeatherForecast/${address}/${userName}`)
  }
  getWeatherAlert(address, userName){
    var token = new HttpHeaders({'Authorization': 'Bearer '+ localStorage.getItem('token')})
    return this.http.get(`${this.URL}/WeatherHazard/GetWeatherAlert/${address}/${userName}`, {headers: token})
  }
  getSevereWeatherArchive(){
    var token = new HttpHeaders({'Authorization': 'Bearer '+ localStorage.getItem('token')})
    return this.http.get(`${this.URL}/WeatherHazard/GetSevereWeatherArchive`, {headers: token})
  }
  getUserSevereWeatherArchive(userName){
    var token = new HttpHeaders({'Authorization': 'Bearer '+ localStorage.getItem('token')})
    return this.http.get(`${this.URL}/WeatherHazard/GetUserSevereWeatherArchive/${userName}`, {headers: token})
  }
  getCovid(country, userName){
    var token = new HttpHeaders({'Authorization': 'Bearer '+ localStorage.getItem('token')})
    return this.http.get(`${this.URL}/WeatherHazard/GetCovid/${country}/${userName}`, {headers: token})
  }
  getCovidArchive(){
    var token = new HttpHeaders({'Authorization': 'Bearer '+ localStorage.getItem('token')})
    return this.http.get(`${this.URL}/WeatherHazard/GetCovidArchive`, {headers: token})
  }
  getUserCovidArchive(userName){
    var token = new HttpHeaders({'Authorization': 'Bearer '+ localStorage.getItem('token')})
    return this.http.get(`${this.URL}/WeatherHazard/GetUserCovidArchive/${userName}`, {headers: token})
  }
}
