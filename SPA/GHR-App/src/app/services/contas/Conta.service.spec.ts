/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ContaService } from './Conta.service';

describe('Service: Conta', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ContaService]
    });
  });

  it('should ...', inject([ContaService], (service: ContaService) => {
    expect(service).toBeTruthy();
  }));
});
