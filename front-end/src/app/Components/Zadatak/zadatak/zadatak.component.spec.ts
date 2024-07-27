import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ZadatakComponent } from './zadatak.component';

describe('ZadatakComponent', () => {
  let component: ZadatakComponent;
  let fixture: ComponentFixture<ZadatakComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ZadatakComponent]
    });
    fixture = TestBed.createComponent(ZadatakComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
