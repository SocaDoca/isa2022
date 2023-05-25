import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { ClinicLoadParameters } from '../model/ClinicLoadParameters';
import { ClinicSaveModel } from '../model/ClinicSaveModel';
import { DbClinic } from '../model/DbClinic';
import { WorkingHours } from '../model/WorkingHours';

@Component({
  selector: 'app-view-clinic',
  templateUrl: './view-clinic.component.html',
  styleUrls: ['./view-clinic.component.css']
})
export class ViewClinicComponent {
  @Input()
  id: any;
  res: ClinicLoadParameters;
  clinics: DbClinic[] = [];
  clinic!: DbClinic;
  role: any;
  workingHours!: WorkingHours;


  constructor(private route: ActivatedRoute, private clinicService: ClinicService) {
    this.res = new ClinicLoadParameters({
      searchCriteria: '',
      offset: 0,
      limit: 20,
      sortBy: '',
      orderAsc: true
    })
  }

  ngOnInit(): void {
    this.id = this.route.params.subscribe(
      params => {
        this.clinic.id = params['Id'];
      });

    this.loadClinic();
  }

  formatDate(timeString: string): string {
    const currentDate = new Date(); // Current date
    const year = currentDate.getFullYear();
    const month = (currentDate.getMonth() + 1).toString().padStart(2, '0');
    const day = currentDate.getDate().toString().padStart(2, '0');
    const timeParts = timeString.split(':');
    const hours = timeParts[0].padStart(2, '0');
    const minutes = timeParts[1].padStart(2, '0');
    const formattedDateTime = `${year}-${month}-${day}T${hours}:${minutes}:00.000Z`;
    return formattedDateTime;
  }

  formatTime(time: Date): string {
    const hours = time.getHours().toString().padStart(2, '0');
    const minutes = time.getMinutes().toString().padStart(2, '0');
    const formattedTime = `${hours}:${minutes}`;
    return formattedTime;
  }


  loadClinic() {
    this.id = this.route.snapshot.params['id'];

    this.clinicService.getAll(this.res)
      .subscribe(res => {
        this.clinics = res;
        console.log(this.clinics);
        this.clinics.forEach(clinic => {
          const worksFromDateTime = new Date(clinic.worksFrom); // Convert worksFrom to Date object
          const worksToDateTime = new Date(clinic.worksTo); // Convert worksTo to Date object

          clinic.worksFrom = this.formatTime(worksFromDateTime); // Format worksFromDateTime as time string
          clinic.worksTo = this.formatTime(worksToDateTime); // Format worksToDateTime as time string

          console.log(clinic.worksFrom);
          console.log(clinic.worksTo);
        });
      });
  }















}
