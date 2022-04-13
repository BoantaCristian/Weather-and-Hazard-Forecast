import { TestBed } from '@angular/core/testing';

import { WeatherHazardService } from './weather-hazard.service';

describe('WeatherHazardService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WeatherHazardService = TestBed.get(WeatherHazardService);
    expect(service).toBeTruthy();
  });
});
