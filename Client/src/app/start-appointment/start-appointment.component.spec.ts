import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StartAppointmentComponent } from './start-appointment.component';

describe('StartAppointmentComponent', () => {
  let component: StartAppointmentComponent;
  let fixture: ComponentFixture<StartAppointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StartAppointmentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StartAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
