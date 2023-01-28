import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { DbAppointment } from '../model/DbAppointment';

@Component({
  selector: 'app-user-scheduled-appointments',
  templateUrl: './user-scheduled-appointments.component.html',
  styleUrls: ['./user-scheduled-appointments.component.css']
})
export class UserScheduledAppointmentsComponent {

  appointments: DbAppointment[] = [];
  appointment: DbAppointment;
  id: any;

  constructor(private clinicService: ClinicService, private route: ActivatedRoute) {
    this.appointment = new DbAppointment({
      title: '',
      startDate: new Date(),
      startTime: '',
      patient_RefId:''
    })
  }

  ngOnInit(): void {
    let patientId = this.route.snapshot.params['id'];
    console.log(patientId);
    this.clinicService.getAllTerms(patientId).subscribe(res => {
      this.appointments = res;
    })
  }

  cancelAppointment() {

  }
}
