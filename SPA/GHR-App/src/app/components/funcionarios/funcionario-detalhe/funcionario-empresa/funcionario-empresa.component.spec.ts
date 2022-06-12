import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FuncionarioEmpresaComponent } from './funcionario-empresa.component';

describe('FuncionarioEmpresaComponent', () => {
  let component: FuncionarioEmpresaComponent;
  let fixture: ComponentFixture<FuncionarioEmpresaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FuncionarioEmpresaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FuncionarioEmpresaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
