/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ContaComponent } from './conta.component';

describe('ContaComponent', () => {
  let component: ContaComponent;
  let fixture: ComponentFixture<ContaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
