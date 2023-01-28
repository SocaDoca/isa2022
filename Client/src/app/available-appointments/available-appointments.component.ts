import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { AppReport } from '../model/AppReport';
import { DbAppointment } from '../model/DbAppointment';

@Component({
  selector: 'app-available-appointments',
  templateUrl: './available-appointments.component.html',
  styleUrls: ['./available-appointments.component.css']
})
export class AvailableAppointmentsComponent {

  appointment: DbAppointment;

  constructor(private clinicService: ClinicService, private route: ActivatedRoute) {
    this.appointment = new DbAppointment({

      title: '',
      startDate: new Date(),
      startTime: '10:00',
      patient_RefId: '',
      clinic_RefID: '',
      isCanceled: false,
      isFinished: false,
      report: new AppReport({
        description: '0',
        equipment: '0',
        isDeleted:false
      })
    })
  }

  ngOnInit(): void {
    this.appointment.clinic_RefID = this.route.snapshot.params['clinicId'];
    console.log(this.appointment.clinic_RefID);
    this.appointment.patient_RefId = this.route.snapshot.params['id'];
    console.log(this.appointment.patient_RefId);
  }

  addUserAppointment() {
    this.appointment.clinic_RefID = this.route.snapshot.params['clinicId'];
    console.log(this.appointment.clinic_RefID);
    this.appointment.patient_RefId = this.route.snapshot.params['id'];
    console.log(this.appointment.patient_RefId);
    this.clinicService.saveAppointment(this.appointment).subscribe(res => {
      this.appointment = res;
    })
  }
}
