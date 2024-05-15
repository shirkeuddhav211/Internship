import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoticeaddeditComponent } from './noticeaddedit.component';

describe('NoticeaddeditComponent', () => {
  let component: NoticeaddeditComponent;
  let fixture: ComponentFixture<NoticeaddeditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NoticeaddeditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NoticeaddeditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
