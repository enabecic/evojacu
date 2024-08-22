import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PomocComponent } from './pomoc.component';

describe('PomocComponent', () => {
  let component: PomocComponent;
  let fixture: ComponentFixture<PomocComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PomocComponent]
    });
    fixture = TestBed.createComponent(PomocComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
