import { TestBed, async, inject } from '@angular/core/testing';

import { HazardGuard } from './hazard.guard';

describe('HazardGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HazardGuard]
    });
  });

  it('should ...', inject([HazardGuard], (guard: HazardGuard) => {
    expect(guard).toBeTruthy();
  }));
});
