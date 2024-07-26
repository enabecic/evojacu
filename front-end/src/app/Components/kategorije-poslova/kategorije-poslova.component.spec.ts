import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KategorijePoslovaComponent } from './kategorije-poslova.component';

describe('KategorijePoslovaComponent', () => {
  let component: KategorijePoslovaComponent;
  let fixture: ComponentFixture<KategorijePoslovaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [KategorijePoslovaComponent]
    });
    fixture = TestBed.createComponent(KategorijePoslovaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
