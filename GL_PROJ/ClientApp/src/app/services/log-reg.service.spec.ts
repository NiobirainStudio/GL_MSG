import { TestBed } from '@angular/core/testing';

import { LogRegService } from './log-reg.service';

describe('LogRegService', () => {
  let service: LogRegService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LogRegService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
