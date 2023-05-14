import { Component } from '@angular/core';
import { type } from 'os';
import { ClinicService } from '../../service/clinic.service';
import { ClinicLoadParameters } from '../model/ClinicLoadParameters';
import { ClinicSaveModel } from '../model/ClinicSaveModel';
import { DbAppointment } from '../model/DbAppointment';
import { DbClinic } from '../model/DbClinic';
import { PredefinedTerm } from '../model/PredefinedTerm';

@Component({
  selector: 'app-admin-add-term',
  templateUrl: './admin-add-term.component.html',
  styleUrls: ['./admin-add-term.component.css']
})
export class AdminAddTermComponent {

  clinics: DbClinic[] = [];
  res: ClinicLoadParameters;
  clinicId: string = '';
  term: PredefinedTerm;
  terms: DbAppointment[] = [];
  clinic!: DbClinic;
  idList: any;


  constructor(private clinicService: ClinicService) {
    this.res = new ClinicLoadParameters({
      searchCriteria: '',
      offset: 0,
      limit: 20,
      sortBy: '',
      orderAsc: true
    }),
      this.term = new PredefinedTerm({
        time: '',
        date: '',
        duration: 20,
        numberOfAppointmentsInDay: 1,
        clinic_RefID: ''
      })
  }

  ngOnInit(): void {
    this.clinicService.getAll(this.res)
      .subscribe(res => {
        this.clinics = res;
      });
  }

  onChange(event: any) {
    const selectedClinicId = event.target.value;
    console.log(selectedClinicId); // this will log the selected clinic ID
    this.term.clinic_RefID = selectedClinicId;
  }

  addAppointment() {  
    console.log(this.term.clinic_RefID);
    this.clinicService.addPredefinedTerm(this.term).subscribe(res => {
      this.terms = res;
      const dateTime = new Date(`${this.term.date}T${this.term.time}:00.000Z`);

      this.term.startDate = dateTime.toISOString();
      console.log(this.term.startDate); // "2023-05-17T10:00:00.000Z"
      //this.term.startDate = this.formatDatetime(this.term.date, this.term.time);
      //console.log(this.terms);
    });
}

  private formatDatetime(dateString: string, timeString: string): string {
    const date = new Date(dateString);
    const [hours, minutes] = timeString.split(':');
    date.setHours(Number(hours));
    date.setMinutes(Number(minutes));
    date.setSeconds(0);
    date.setMilliseconds(0);

    return date.toISOString();
  }


}
