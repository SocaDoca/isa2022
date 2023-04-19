import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminReservedAppointmentsComponent } from './admin-reserved-appointments.component';

describe('AdminReservedAppointmentsComponent', () => {
  let component: AdminReservedAppointmentsComponent;
  let fixture: ComponentFixture<AdminReservedAppointmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminReservedAppointmentsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminReservedAppointmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
