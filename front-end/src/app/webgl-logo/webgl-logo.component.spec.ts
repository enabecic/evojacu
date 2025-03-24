import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WebglLogoComponent } from './webgl-logo.component';

describe('WebglLogoComponent', () => {
  let component: WebglLogoComponent;
  let fixture: ComponentFixture<WebglLogoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WebglLogoComponent]
    });
    fixture = TestBed.createComponent(WebglLogoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
