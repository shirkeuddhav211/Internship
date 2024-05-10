import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HolidaymasterComponent } from './holidaymaster.component';

describe('HolidaymasterComponent', () => {
  let component: HolidaymasterComponent;
  let fixture: ComponentFixture<HolidaymasterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HolidaymasterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HolidaymasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
