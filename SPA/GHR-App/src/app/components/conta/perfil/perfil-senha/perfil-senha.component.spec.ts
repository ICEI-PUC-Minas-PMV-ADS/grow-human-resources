import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PerfilSenhaComponent } from './perfil-senha.component';

describe('PerfilSenhaComponent', () => {
  let component: PerfilSenhaComponent;
  let fixture: ComponentFixture<PerfilSenhaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PerfilSenhaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PerfilSenhaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
