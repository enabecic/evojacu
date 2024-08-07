import { TestBed } from '@angular/core/testing';

import { JezikService } from './jezik.service';

describe('JezikService', () => {
  let service: JezikService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JezikService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
