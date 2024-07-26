import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KategorijaDodajComponent } from './kategorija-dodaj.component';

describe('KategorijaDodajComponent', () => {
  let component: KategorijaDodajComponent;
  let fixture: ComponentFixture<KategorijaDodajComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [KategorijaDodajComponent]
    });
    fixture = TestBed.createComponent(KategorijaDodajComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
