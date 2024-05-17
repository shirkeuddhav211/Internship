import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoticeAddEditComponent } from './notice-add-edit.component';

describe('NoticeAddEditComponent', () => {
  let component: NoticeAddEditComponent;
  let fixture: ComponentFixture<NoticeAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NoticeAddEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NoticeAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
