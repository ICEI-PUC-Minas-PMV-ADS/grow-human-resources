/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CargosComponent } from './cargos.component';

describe('CargosComponent', () => {
  let component: CargosComponent;
  let fixture: ComponentFixture<CargosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CargosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CargosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
