import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { ClinicSaveModel } from '../model/ClinicSaveModel';
import { WorkingHours } from '../model/WorkingHours';

@Component({
  selector: 'app-update-clinic',
  templateUrl: './update-clinic.component.html',
  styleUrls: ['./update-clinic.component.css']
})
export class UpdateClinicComponent{
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
  workHours: WorkingHours;
  Id: any;

  constructor(private clinicService: ClinicService, private router: Router, private route: ActivatedRoute) {
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

        })]

    })
  }

  ngOnInit(): void {
    this.loadClinic();
  }

  loadClinic() {
    this.Id = this.route.snapshot.params['Id'];
    console.log(this.Id);
    this.clinicService.getClinicById(this.Id).subscribe(res => {
      this.clinic = res;
      this.clinic.workHours = res.workHours;
      console.log(this.clinic.workHours);
    })
  }

  onSubmit(): void {
    this.submitted = true;


    if (this.form.invalid) {
      return;
    } else {
      this.updateClinic();
      console.log(JSON.stringify(this.form.value, null, 2));
      this.router.navigate(['/profileEmployee/:id/viewClinic']);
    }
  }


  updateClinic() {

    this.clinicService.updateClinic(this.clinic).subscribe(res => {
      //this.clinic = res;
      console.log(res);
      //console.log(this.clinic.workHours![this.hours.dayOfWeek]);
      console.log(this.workHours.day);

    });
  }

 
}
