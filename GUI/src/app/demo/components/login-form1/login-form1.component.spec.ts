import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginForm1Component } from './login-form1.component';

describe('LoginForm1Component', () => {
  let component: LoginForm1Component;
  let fixture: ComponentFixture<LoginForm1Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LoginForm1Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LoginForm1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
