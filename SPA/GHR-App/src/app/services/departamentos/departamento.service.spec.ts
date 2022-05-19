/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DepartamentoService } from './departamento.service';

describe('Service: Departamento', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DepartamentoService]
    });
  });

  it('should ...', inject([DepartamentoService], (service: DepartamentoService) => {
    expect(service).toBeTruthy();
  }));
});
