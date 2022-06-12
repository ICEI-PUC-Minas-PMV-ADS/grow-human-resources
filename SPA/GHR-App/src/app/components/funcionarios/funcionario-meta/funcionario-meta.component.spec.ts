import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FuncionarioMetaComponent } from './funcionario-meta.component';

describe('FuncionarioMetaComponent', () => {
  let component: FuncionarioMetaComponent;
  let fixture: ComponentFixture<FuncionarioMetaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FuncionarioMetaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FuncionarioMetaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
