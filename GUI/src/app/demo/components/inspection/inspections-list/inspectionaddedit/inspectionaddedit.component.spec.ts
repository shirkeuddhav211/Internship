import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InspectionaddeditComponent } from './inspectionaddedit.component';

describe('InspectionaddeditComponent', () => {
  let component: InspectionaddeditComponent;
  let fixture: ComponentFixture<InspectionaddeditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InspectionaddeditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InspectionaddeditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
