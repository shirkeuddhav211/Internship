import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InspectionMapComponent } from './inspection-map.component';

describe('InspectionMapComponent', () => {
  let component: InspectionMapComponent;
  let fixture: ComponentFixture<InspectionMapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InspectionMapComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InspectionMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
