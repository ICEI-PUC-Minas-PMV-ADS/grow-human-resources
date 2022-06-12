/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DadosPessoaisService } from './dadosPessoais.service';

describe('Service: DadosPessoais', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DadosPessoaisService]
    });
  });

  it('should ...', inject([DadosPessoaisService], (service: DadosPessoaisService) => {
    expect(service).toBeTruthy();
  }));
});
