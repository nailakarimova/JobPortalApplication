import { TestBed } from '@angular/core/testing';

import { AzerbaijaniPhoneNumberService } from './azerbaijani-phone-number.service';

describe('AzerbaijaniPhoneNumberService', () => {
  let service: AzerbaijaniPhoneNumberService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AzerbaijaniPhoneNumberService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
