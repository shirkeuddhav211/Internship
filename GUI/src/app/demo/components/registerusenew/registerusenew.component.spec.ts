import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterusenewComponent } from './registerusenew.component';

describe('RegisterusenewComponent', () => {
  let component: RegisterusenewComponent;
  let fixture: ComponentFixture<RegisterusenewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RegisterusenewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RegisterusenewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
