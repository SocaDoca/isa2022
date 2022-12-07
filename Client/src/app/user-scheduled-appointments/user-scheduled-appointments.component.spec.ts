import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserScheduledAppointmentsComponent } from './user-scheduled-appointments.component';

describe('UserScheduledAppointmentsComponent', () => {
  let component: UserScheduledAppointmentsComponent;
  let fixture: ComponentFixture<UserScheduledAppointmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserScheduledAppointmentsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserScheduledAppointmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
