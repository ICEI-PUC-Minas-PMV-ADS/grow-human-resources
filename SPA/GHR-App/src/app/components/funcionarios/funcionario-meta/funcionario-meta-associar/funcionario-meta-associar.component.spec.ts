/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FuncionarioMetaAssociarComponent } from './funcionario-meta-associar.component';

describe('FuncionarioMetaAssociarComponent', () => {
  let component: FuncionarioMetaAssociarComponent;
  let fixture: ComponentFixture<FuncionarioMetaAssociarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FuncionarioMetaAssociarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FuncionarioMetaAssociarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
