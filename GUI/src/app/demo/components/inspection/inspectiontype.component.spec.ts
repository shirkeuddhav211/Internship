import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InspectiontypeComponent } from './inspectiontype.component';

describe('InspectiontypeComponent', () => {
  let component: InspectiontypeComponent;
  let fixture: ComponentFixture<InspectiontypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InspectiontypeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InspectiontypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
