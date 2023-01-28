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

  id: any;
  appointment: DbAppointment;
  appointments: DbAppointment[] = [];

  constructor(private clinicService: ClinicService, private route: ActivatedRoute) {
    this.appointment = new DbAppointment({

      title: '',
      startDate: new Date,
      startTime: '',
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
    this.id = this.route.snapshot.params['clinicId'];
    console.log(this.appointment.clinic_RefID);
    this.appointment.patient_RefId = this.route.snapshot.params['id'];
    console.log(this.appointment.patient_RefId);
    /*this.clinicService.getTermById(this.id).subscribe(res => {
      this.appointment = res;
    });*/
   // console.log(JSON.stringify(this.form.value, null, 2));
  }

  onSubmit() {
    /*this.clinicService.getTermById(this.id).subscribe(res => {
      this.appointment = res;
    })*/
    /*this.clinicService.saveAppointment(this.appointment).subscribe(res => {
      this.appointment = res;
    })*/
  }
}
