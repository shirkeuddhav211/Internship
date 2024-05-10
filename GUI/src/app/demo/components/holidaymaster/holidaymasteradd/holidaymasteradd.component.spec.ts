import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HolidaymasteraddComponent } from './holidaymasteradd.component';

describe('HolidaymasteraddComponent', () => {
  let component: HolidaymasteraddComponent;
  let fixture: ComponentFixture<HolidaymasteraddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HolidaymasteraddComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HolidaymasteraddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
