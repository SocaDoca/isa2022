import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminAvailableAppointmentsComponent } from './admin-available-appointments.component';

describe('AdminAvailableAppointmentsComponent', () => {
  let component: AdminAvailableAppointmentsComponent;
  let fixture: ComponentFixture<AdminAvailableAppointmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminAvailableAppointmentsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminAvailableAppointmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
