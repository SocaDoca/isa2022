import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { AppReport } from '../model/AppReport';
import { DbAppointment } from '../model/DbAppointment';

@Component({
  selector: 'app-schedule-app',
  templateUrl: './schedule-app.component.html',
  styleUrls: ['./schedule-app.component.css']
})
export class ScheduleAppComponent {
  appointment: DbAppointment;

  constructor(private clinicService: ClinicService, private route: ActivatedRoute, private router: Router) {
    this.appointment = new DbAppointment({

      title: 'Blood appointment',
      startDate: new Date,
      startTime: '',
      patient_RefId: '',
      clinic_RefID: '',
      isCanceled: false,
      isFinished: false,
      report: new AppReport({
        description: '0',
        equipment: '0',
        isDeleted: false
      })
    })
  }

  ngOnInit(): void {
    this.appointment.clinic_RefID = this.route.snapshot.params['clinicId'];
    console.log(this.appointment.clinic_RefID);
    this.appointment.patient_RefId = this.route.snapshot.params['id'];
    console.log(this.appointment.patient_RefId);
    // console.log(JSON.stringify(this.form.value, null, 2));
  }

  addUserAppointment() {

    this.clinicService.saveAppointment(this.appointment).subscribe(res => {
      this.appointment = res;

    });
    this.router.navigate(['/profile', this.appointment.patient_RefId]);
  }
}
