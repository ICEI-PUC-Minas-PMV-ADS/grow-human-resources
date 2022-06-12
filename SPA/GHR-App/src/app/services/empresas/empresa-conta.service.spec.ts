import { TestBed } from '@angular/core/testing';

import { EmpresaContaService } from './empresa-conta.service';

describe('EmpresaContaService', () => {
  let service: EmpresaContaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpresaContaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
