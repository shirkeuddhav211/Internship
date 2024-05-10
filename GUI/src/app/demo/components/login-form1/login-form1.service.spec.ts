import { TestBed } from '@angular/core/testing';

import { LoginForm1Service } from './login-form1.service';

describe('LoginForm1Service', () => {
  let service: LoginForm1Service;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoginForm1Service);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
