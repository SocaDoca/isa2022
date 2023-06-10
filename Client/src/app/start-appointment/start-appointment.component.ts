import { Component } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { DbAppointment } from '../model/DbAppointment';

@Component({
  selector: 'app-start-appointment',
  templateUrl: './start-appointment.component.html',
  styleUrls: ['./start-appointment.component.css']
})
export class StartAppointmentComponent {
  id?: any;
  appointment?: DbAppointment;
  constructor(private route: ActivatedRoute, private clinicService: ClinicService, private router: Router) { }


  startApp() {
    this.id = this.route.snapshot.params['id'];
    this.clinicService.startTerm(this.id)
      .subscribe(res => {
        this.id = res;
        
        console.log(this.id);
      });
    this.router.navigate(['/profileEmployee', this.id, 'reservedAppointments', 'start-appointment', this.id, 'report']);

  }
}
