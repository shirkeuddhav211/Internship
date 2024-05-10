import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangepswdComponent } from './changepswd.component';

describe('ChangepswdComponent', () => {
  let component: ChangepswdComponent;
  let fixture: ComponentFixture<ChangepswdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChangepswdComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ChangepswdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
