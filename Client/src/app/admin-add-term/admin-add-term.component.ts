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
        time: '08:00',
        date: new Date(),
        duration: 20,
        numberOfAppointmentsInDay: 1,
        clinic_RefID: ''
      })
  }

  ngOnInit(): void {
    this.clinicService.getAll(this.res)
      .subscribe(res => {
        this.clinics = res;

        let projectNames = this.clinics.map(item => {
          return item.id;


        });


        var n = projectNames.length;
        console.log(n);
        var splitted = projectNames.pop();
        console.log(splitted);
        this.term.clinic_RefID = splitted;
        console.log(this.term.clinic_RefID);




        //let project1 = projectNames.split(",");
        //console.log(typeof( projectNames)); //vraca listu id ["id1", "id2"]
        //console.log(this.clinics.find("id"));
       // console.log(this.clinics);
      });
  }

  onChange() {
    console.log(this.clinics);
    this.clinicService.getClinicById(this.term.clinic_RefID!).subscribe(res => {
      this.term.clinic_RefID = res.id;
      console.log(this.term.clinic_RefID);
    })
  }

  addAppointment() {

    console.log(this.term.clinic_RefID);
    this.clinicService.addPredefinedTerm(this.term).subscribe(res => {
      this.terms = res;
      
      console.log(this.terms);
    });
}




}
