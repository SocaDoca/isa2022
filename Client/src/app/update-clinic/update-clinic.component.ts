import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ClinicService } from '../../service/clinic.service';
import { ClinicSaveModel } from '../model/ClinicSaveModel';
import { WorkingHours } from '../model/WorkingHours';

@Component({
  selector: 'app-update-clinic',
  templateUrl: './update-clinic.component.html',
  styleUrls: ['./update-clinic.component.css']
})
export class UpdateClinicComponent {
  submitted: boolean = false;

  form: FormGroup = new FormGroup({

    name: new FormControl(''),
    description: new FormControl(''),
    rating: new FormControl(''),
    phone: new FormControl(''),
    workHours: new FormControl(''),
    country: new FormControl(''),
    city: new FormControl(''),
    address: new FormControl(''),


  });

  clinic: ClinicSaveModel;
  hours: WorkingHours;

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
        this.hours = new WorkingHours({
          start: new Date,
          end: new Date(),
          dayOfWeek: undefined

        })]

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
    this.checkBoxValue1(event);
    //this.checkBoxValue2(event);
    //this.checkBoxValue3(event);
    //this.checkBoxValue4(event);
    //this.checkBoxValue5(event);
    //this.checkBoxValue6(event);
    //this.checkBoxValue7(event);
    this.clinicService.saveClinic(this.clinic).subscribe(res => {
      this.clinic = res;
      console.log(res);
      //console.log(this.clinic.workHours![this.hours.dayOfWeek]);
      console.log(this.hours.dayOfWeek);

    });
  }

  checkBoxValue1(event: any) {
    console.log(event.target.checked);
    if (event.target.checked == true) {
      this.hours.dayOfWeek = 2;
      this.clinic.workHours![this.hours.dayOfWeek] == 2;

    }
  }

  checkBoxValue2(event: any) {
    console.log(event.target.checked);
    if (event.target.checked) {
      //this.hours.dayOfWeek == 1;
      //this.clinic.workHours![2].day == this.hours.day;
      //this.clinic.workHours![2] == 1;
    }
  }
  checkBoxValue3(event: any) {
    console.log(event.target.checked);
    if (event.target.checked == true) {
      // this.workHours.day == 2;
    }
  }
  checkBoxValue4(event: any) {
    console.log(event.target.checked);
    if (event.target.checked == true) {
      this.clinic.workHours![2] == 3;
    }
  }
  checkBoxValue5(event: any) {
    console.log(event.target.checked);
    if (event.target.checked == true) {
      this.clinic.workHours![2] == 4;
    }
  }
  checkBoxValue6(event: any) {
    console.log(event.target.checked);
    if (event.target.checked == true) {
      this.clinic.workHours![2] == 5;
    }
  }
  checkBoxValue7(event: any) {
    console.log(event.target.checked);
    if (event.target.checked == true) {
      this.clinic.workHours![2] == 6;
    }
  }
}
