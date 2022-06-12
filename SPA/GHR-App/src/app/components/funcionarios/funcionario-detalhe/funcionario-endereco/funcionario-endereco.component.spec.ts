import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FuncionarioEnderecoComponent } from './funcionario-endereco.component';

describe('FuncionarioEnderecoComponent', () => {
  let component: FuncionarioEnderecoComponent;
  let fixture: ComponentFixture<FuncionarioEnderecoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FuncionarioEnderecoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FuncionarioEnderecoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
