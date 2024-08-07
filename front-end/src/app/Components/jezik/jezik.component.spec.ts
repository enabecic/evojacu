import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JezikComponent } from './jezik.component';

describe('JezikComponent', () => {
  let component: JezikComponent;
  let fixture: ComponentFixture<JezikComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [JezikComponent]
    });
    fixture = TestBed.createComponent(JezikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
