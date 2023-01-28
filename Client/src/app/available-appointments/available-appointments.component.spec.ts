import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvailableAppointmentsComponent } from './available-appointments.component';

describe('AvailableAppointmentsComponent', () => {
  let component: AvailableAppointmentsComponent;
  let fixture: ComponentFixture<AvailableAppointmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AvailableAppointmentsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AvailableAppointmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
