import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { resOwnerGuard } from './res-owner.guard';

describe('resOwnerGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => resOwnerGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
