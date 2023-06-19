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
  appointmenetId?: any;
  constructor(private route: ActivatedRoute, private clinicService: ClinicService, private router: Router) { }


  startApp() {
    this.id = this.route.snapshot.paramMap.getAll('id');
    console.log(this.id);
    this.clinicService.startTerm(this.id)
      .subscribe(res => {
        this.id = res;
        
        console.log(this.id);
      });
    const ids = this.route.snapshot.paramMap.getAll('id');
    const lastId = ids[ids.length - 1];
    const urlSegments = this.route.snapshot.url;
    const firstId = urlSegments[1].path;
    this.router.navigate(['/profileEmployee', firstId, 'reservedAppointments', 'start-appointment', lastId, 'report']);


  }

  addPenalty() {
    this.appointmenetId = this.route.snapshot.params['id'];
    console.log(this.appointmenetId);
    this.clinicService.addPenalty(this.appointmenetId).subscribe(res => {
      
    });
    }
}
