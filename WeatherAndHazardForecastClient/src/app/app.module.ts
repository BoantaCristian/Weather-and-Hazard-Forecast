import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { AuthenticationComponent } from './components/authentication/authentication.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { RegisterComponent } from './components/authentication/register/register.component';
import { AdminComponent } from './components/admin/admin.component';

import { HttpClientModule } from '@angular/common/http';
import { WeatherHazardService } from "./services/weather-hazard.service";
import { MatAutocompleteModule, MatButtonModule, MatCardModule, MatDialogModule, MatIconModule, MatInputModule, MatListModule, MatPaginatorModule, MatSelectModule, MatSortModule, MatTableModule, MatTooltipModule } from '@angular/material';
import { ToastrModule } from 'ngx-toastr';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { WeatherDetailComponent } from './components/dialogs/weather-detail/weather-detail.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { HazardComponent } from './components/hazard/hazard.component';
import { CovidComponent } from './components/hazard/covid/covid.component';
import { EarthquakeComponent } from './components/hazard/earthquake/earthquake.component';
import { WeatherComponent } from './components/hazard/weather/weather.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AuthenticationComponent,
    LoginComponent,
    RegisterComponent,
    AdminComponent,
    AuthenticationComponent,
    WeatherDetailComponent,
    ToolbarComponent,
    HazardComponent,
    CovidComponent,
    EarthquakeComponent,
    WeatherComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AngularFontAwesomeModule,
    FormsModule, ReactiveFormsModule,
    ToastrModule.forRoot(),
    MatCardModule, MatButtonModule, MatInputModule, MatSelectModule, MatAutocompleteModule, MatIconModule, MatPaginatorModule, MatTableModule, MatSortModule, MatTooltipModule, MatDialogModule, MatListModule, MatDialogModule
  ],
  entryComponents: [WeatherDetailComponent],
  providers: [WeatherHazardService],
  bootstrap: [AppComponent]
})
export class AppModule { }
