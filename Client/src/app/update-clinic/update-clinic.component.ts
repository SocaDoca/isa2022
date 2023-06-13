import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { ClinicSaveModel } from '../model/ClinicSaveModel';
import { DbClinic } from '../model/DbClinic';
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

  clinic: DbClinic;
  id: any;

  constructor(private clinicService: ClinicService, private router: Router, private route: ActivatedRoute) {
    this.clinic = new DbClinic({
      name: '',
      address: '',
      city: '',
      country: '',
      description: '',
      phone: '',
      rating: 0,

    })
  }

  ngOnInit(): void {
    this.loadClinic();
  }

  formatTime(time: Date): string {
    const hours = time.getHours().toString().padStart(2, '0');
    const minutes = time.getMinutes().toString().padStart(2, '0');
    const formattedTime = `${hours}:${minutes}`;
    return formattedTime;
  }

  loadClinic() {
    this.id = this.route.snapshot.params['id'];

    this.clinicService.getClinicById(this.id).subscribe(res => {
      const clinic = res;

      const worksFromDateTime = new Date(clinic.worksFrom); // Convert worksFrom to Date object
      const worksToDateTime = new Date(clinic.worksTo); // Convert worksTo to Date object

      clinic.worksFrom = this.formatTime(worksFromDateTime); // Format worksFromDateTime as time string
      clinic.worksTo = this.formatTime(worksToDateTime); // Format worksToDateTime as time string

      console.log(clinic.worksFrom);
      console.log(clinic.worksTo);

      this.clinic = clinic;
    });
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
      //console.log(this.workHours.day);

    });
  }

 
}
