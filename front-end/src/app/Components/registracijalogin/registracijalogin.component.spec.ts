import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistracijaloginComponent } from './registracijalogin.component';

describe('RegistracijaloginComponent', () => {
  let component: RegistracijaloginComponent;
  let fixture: ComponentFixture<RegistracijaloginComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegistracijaloginComponent]
    });
    fixture = TestBed.createComponent(RegistracijaloginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
