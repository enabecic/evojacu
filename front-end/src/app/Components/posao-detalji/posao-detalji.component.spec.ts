import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PosaoDetaljiComponent } from './posao-detalji.component';

describe('PosaoDetaljiComponent', () => {
  let component: PosaoDetaljiComponent;
  let fixture: ComponentFixture<PosaoDetaljiComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PosaoDetaljiComponent]
    });
    fixture = TestBed.createComponent(PosaoDetaljiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
