import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FuncionarioDadosPessoaisComponent } from './funcionario-dados-pessoais.component';

describe('FuncionarioDadosPessoaisComponent', () => {
  let component: FuncionarioDadosPessoaisComponent;
  let fixture: ComponentFixture<FuncionarioDadosPessoaisComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FuncionarioDadosPessoaisComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FuncionarioDadosPessoaisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
