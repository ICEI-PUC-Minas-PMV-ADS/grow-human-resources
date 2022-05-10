/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MetaServiceService } from './MetaService.service';

describe('Service: MetaService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MetaServiceService]
    });
  });

  it('should ...', inject([MetaServiceService], (service: MetaServiceService) => {
    expect(service).toBeTruthy();
  }));
});
