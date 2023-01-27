import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { toJSDate } from '@ng-bootstrap/ng-bootstrap/datepicker/ngb-calendar';
import { subscribeOn } from 'rxjs-compat/operator/subscribeOn';
import { ClinicService } from '../../service/clinic.service';
import { ClinicSaveModel } from '../model/ClinicSaveModel';

import { DbClinic } from '../model/DbClinic';
import { WorkingHours } from '../model/WorkingHours';

@Component({
  selector: 'app-add-clinic',
  templateUrl: './add-clinic.component.html',
  styleUrls: ['./add-clinic.component.css']
})
export class AddClinicComponent {
  submitted: boolean = false;

  form: FormGroup = new FormGroup({

    name: new FormControl(''),
    description: new FormControl(''),
    rating: new FormControl(''),
    phone: new FormControl(''),
    workingHours: new FormControl(''),
    country: new FormControl(''),
    city: new FormControl(''),
    address: new FormControl(''),


  });

  clinic: ClinicSaveModel;
  workHours!: WorkingHours;

  constructor(private clinicService: ClinicService) {
    this.clinic = new ClinicSaveModel({
      name: '',
      address: '',
      city: '',
      country: '',
      description: '',
      phone: '',
      rating: 0,
      workHours: [
        this.workHours = new WorkingHours({
          start: '',
          end: '',
          day: ''

        })  ]    
        
    })
  }

  onSubmit(): void {
    this.submitted = true;


    if (this.form.invalid) {
      return;
    } else {
      this.saveClinic();
      console.log(JSON.stringify(this.form.value, null, 2));
    }
  }


  saveClinic() {
    console.log(this.clinic);
    this.clinicService.saveClinic(this.clinic).subscribe(res => {
      //this.clinic.workHours![this.hours.dayOfWeek] = res;
      //this.clinic = res;
      console.log(res);
      //console.log(this.clinic.workHours![this.hours.dayOfWeek]);
      //console.log(this.workHours.day);

    });
  }

  /*checkBoxValue1(event: any) {
    console.log(event.target.checked);
    if (event.target.checked == true) {
      this.hours.dayOfWeek = 1;
      this.clinic.workHours![this.hours.dayOfWeek] == 1;

    }
  }

  checkBoxValue2(event: any) {
    console.log(event.target.checked);
    if (event.target.checked) {
      this.hours.dayOfWeek = 2;
      this.clinic.workHours![this.hours.dayOfWeek] == 2;
    }
  }
  checkBoxValue3(event: any) {
    console.log(event.target.checked);
    if (event.target.checked == true) {
      this.hours.dayOfWeek = 3;
     // this.clinic.workHours![this.hours.dayOfWeek] = 3;
    }
  }
  checkBoxValue4(event: any) {
    console.log(event.target.checked);
    if (event.target.checked == true) {
      this.hours.dayOfWeek = 4;
      this.clinic.workHours![this.hours.dayOfWeek] == 4;
    }
  }
  checkBoxValue5(event: any) {
    console.log(event.target.checked);
    if (event.target.checked == true) {
      this.hours.dayOfWeek = 5;
      this.clinic.workHours![this.hours.dayOfWeek] == 5;
    }
  }
  checkBoxValue6(event: any) {
    console.log(event.target.checked);
    if (event.target.checked == true) {
      this.hours.dayOfWeek = 6;
      this.clinic.workHours![this.hours.dayOfWeek] == 6;
    }
  }
  checkBoxValue7(event: any) {
    console.log(event.target.checked);
    if (event.target.checked == true) {
      this.hours.dayOfWeek = 7;
      this.clinic.workHours![this.hours.dayOfWeek] == 7;
    }
  }*/

}
