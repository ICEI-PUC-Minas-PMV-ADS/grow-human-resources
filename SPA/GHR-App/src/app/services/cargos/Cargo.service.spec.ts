/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CargoService } from './Cargo.service';

describe('Service: Cargo', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CargoService]
    });
  });

  it('should ...', inject([CargoService], (service: CargoService) => {
    expect(service).toBeTruthy();
  }));
});
