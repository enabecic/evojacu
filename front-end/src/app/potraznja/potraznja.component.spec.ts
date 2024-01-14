import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PotraznjaComponent } from './potraznja.component';

describe('PotraznjaComponent', () => {
  let component: PotraznjaComponent;
  let fixture: ComponentFixture<PotraznjaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PotraznjaComponent]
    });
    fixture = TestBed.createComponent(PotraznjaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
