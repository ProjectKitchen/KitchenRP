import { TestBed } from '@angular/core/testing';

import { RestrictionService } from './restriction.service';

describe('RestrictionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RestrictionService = TestBed.get(RestrictionService);
    expect(service).toBeTruthy();
  });
});
