import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewInspectionComponent } from './new-inspection.component';

describe('NewInspectionComponent', () => {
  let component: NewInspectionComponent;
  let fixture: ComponentFixture<NewInspectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewInspectionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NewInspectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
