import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { AppReport } from '../model/AppReport';
import { DbAppointment } from '../model/DbAppointment';
import { DbClinic } from '../model/DbClinic';
import { User } from '../model/User';

@Component({
  selector: 'app-schedule-app',
  templateUrl: './schedule-app.component.html',
  styleUrls: ['./schedule-app.component.css']
})
export class ScheduleAppComponent {
  appointment: DbAppointment;
  appointments: DbAppointment[] = [];
  id: any;
  fullAddress: any;

  constructor(private clinicService: ClinicService, private route: ActivatedRoute, private router: Router) {
    this.appointment = new DbAppointment({

      title: '',
      startDate: '',
      startTime: '0',
      patient_RefId: '',
      clinic_RefID: '',
      isCanceled: false,
      isFinished: false,
      report: new AppReport({
        description: '0',
        equipment: '0',
        isDeleted: false
      }),
      patient: new User({
        firstName: '',
        lastName: '',
        email: ''
      }),
      clinic: new DbClinic({
        name: '',
        address: '',
        city: '',
        country:''
      })
    })
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['appointmentId'];
    console.log(this.id);

    this.appointment.patient_RefId = this.route.snapshot.params['id'];
   // console.log(this.appointment.clinic_RefID);
    //console.log(this.appointment.patient_RefId);
    this.clinicService.getTermById(this.id).subscribe(res => {
      this.appointment = res;
      this.appointment.patient_RefId = res.patient_RefId;
      this.fullAddress = this.appointment.clinic?.address! + ", " + this.appointment.clinic?.city + ", " + this.appointment.clinic?.country;
      console.log(this.fullAddress);
    });

  }

  addUserAppointment() {
    this.appointment.clinic_RefID = this.route.snapshot.params['clinicId'];
    this.appointment.patient_RefId = this.route.snapshot.params['id'];
    this.clinicService.saveAppointment(this.appointment).subscribe(res => {
      this.appointment.patient_RefId = res.patient_RefId;
      this.appointment.clinic_RefID = res.clinic_RefID;
      this.appointment = res;

    });
    this.router.navigate(['/profile', this.appointment.patient_RefId]);
  }
}
