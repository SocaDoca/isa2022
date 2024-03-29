import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { ClinicLoadParameters } from '../model/ClinicLoadParameters';
import { ClinicSaveModel } from '../model/ClinicSaveModel';
import { DbAppointment } from '../model/DbAppointment';
import { DbClinic } from '../model/DbClinic';

@Component({
  selector: 'app-view-clinic-user',
  templateUrl: './view-clinic-user.component.html',
  styleUrls: ['./view-clinic-user.component.css']
})
export class ViewClinicUserComponent {
  @Input()
  id: any;
  res: ClinicLoadParameters;
  clinics: DbClinic[] = [];
  role: any;
  appointments: DbAppointment[] = [];
  appointment: DbAppointment;


  constructor(private route: ActivatedRoute, private clinicService: ClinicService) {
    this.res = new ClinicLoadParameters({
      searchCriteria: '',
      offset: 0,
      limit: 10,
      sortBy: '',
      orderAsc: true
    }),
      this.appointment = new DbAppointment({
        title: '',
        startDate: Date,
        startTime: '',
        isCanceled: false,
        isFinished:false,
      })

  }

  ngOnInit(): void {
    this.role = sessionStorage.getItem('role');
    if (this.role == 'Admin') {
      this.role = 1;
      console.log(this.role);
    }

    this.loadClinic();

  }

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    return `${hours}:${minutes}`;
  }

  loadClinic() {
    this.id = this.route.snapshot.params['id'];

    this.clinicService.getAll(this.res)
      .subscribe(res => {
        this.clinics = res;
        console.log(this.clinics);
        this.clinics.forEach(clinic => {
          clinic.worksFrom = this.formatDate(clinic.worksFrom!);
          clinic.worksTo = this.formatDate(clinic.worksTo!);
          console.log(clinic.worksTo);
        });
      })
  }
}
