import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FuncionarioContaComponent } from './funcionario-conta.component';

describe('FuncionariosContaComponent', () => {
  let component: FuncionarioContaComponent;
  let fixture: ComponentFixture<FuncionarioContaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FuncionarioContaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FuncionarioContaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
