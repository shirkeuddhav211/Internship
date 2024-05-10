import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResidentuserComponent } from './residentuser.component';

describe('ResidentuserComponent', () => {
  let component: ResidentuserComponent;
  let fixture: ComponentFixture<ResidentuserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ResidentuserComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ResidentuserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
