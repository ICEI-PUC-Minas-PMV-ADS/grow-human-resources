/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FuncionarioMetaService } from './funcionarioMeta.service';

describe('Service: FuncionarioMeta', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FuncionarioMetaService]
    });
  });

  it('should ...', inject([FuncionarioMetaService], (service: FuncionarioMetaService) => {
    expect(service).toBeTruthy();
  }));
});
