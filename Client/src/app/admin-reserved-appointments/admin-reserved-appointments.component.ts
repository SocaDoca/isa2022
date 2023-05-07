import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { UserService } from '../../service/user.service';
import { DbAppointment } from '../model/DbAppointment';
import { Questionnaire } from '../model/Questionnaire';

@Component({
  selector: 'app-admin-reserved-appointments',
  templateUrl: './admin-reserved-appointments.component.html',
  styleUrls: ['./admin-reserved-appointments.component.css']
})
export class AdminReservedAppointmentsComponent {
  @Input()
  apps: DbAppointment[] = [];
  app!: DbAppointment;
  questionnaires: Questionnaire[] = [];
  questionnaire!: Questionnaire;



  constructor(private route: ActivatedRoute, private clinicService: ClinicService, private router: Router, private userService: UserService) {
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
        this.hideCanceled();
        this.apps.forEach(app => {
          app.startDate = this.formatDate(app.startDate);
        });
      })

  }

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const seconds = date.getSeconds().toString().padStart(2, '0');
    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
  }


  cancelApp(id:any) {
    this.clinicService.cancelAppointment(id).subscribe(res =>{
      window.location.href = '?appointmenetId=' + id;
      location.reload();
    })

  }

  startApp() {
    this.router.navigate(['/start-appointment']);
  }

  getQuestionnaireResult(patientId: number): string {
    const questionnaire = this.questionnaires.find(q => q.id === patientId);
    return questionnaire ? questionnaire.isValid : '';
  }

  hideCanceled(): void {
    this.apps = this.apps.filter(app => app.isCanceled !== undefined && app.isCanceled !== true);

  }

}
