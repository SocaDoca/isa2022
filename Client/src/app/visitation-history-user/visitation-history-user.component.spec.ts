import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VisitationHistoryUserComponent } from './visitation-history-user.component';

describe('VisitationHistoryUserComponent', () => {
  let component: VisitationHistoryUserComponent;
  let fixture: ComponentFixture<VisitationHistoryUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VisitationHistoryUserComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VisitationHistoryUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
