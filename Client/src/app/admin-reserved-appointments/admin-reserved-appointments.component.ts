import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { DbAppointment } from '../model/DbAppointment';

@Component({
  selector: 'app-admin-reserved-appointments',
  templateUrl: './admin-reserved-appointments.component.html',
  styleUrls: ['./admin-reserved-appointments.component.css']
})
export class AdminReservedAppointmentsComponent {
  @Input()
  id: any;
  apps: DbAppointment[] = [];
  app!: DbAppointment;


  constructor(private route: ActivatedRoute, private clinicService: ClinicService) {
    this.app = new DbAppointment({
      title: 'Blood appointment',
      startDate: ''
    })
  }

  ngOnInit(): void {
    this.clinicService.getReservedAppointments()
      .subscribe(res => {
        this.apps = res
        console.log(this.apps)
      })
  }
}
