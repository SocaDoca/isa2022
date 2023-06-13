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
      this.router.navigateByUrl(`/profileEmployee/${this.id}/viewClinic`).then(() => {
        window.location.reload();
      });
    }
  }


  updateClinic() {
    const currentTimezoneOffset = new Date().getTimezoneOffset();

    // Adjust the worksFrom and worksTo values based on the timezone offset
    const worksFromTime = this.adjustTimeByTimezone(this.clinic.worksFrom, currentTimezoneOffset);
    const worksToTime = this.adjustTimeByTimezone(this.clinic.worksTo, currentTimezoneOffset);

    // Get the current date
    const currentDate = new Date();
    const currentYear = currentDate.getFullYear();
    const currentMonth = currentDate.getMonth() + 1;
    const currentDay = currentDate.getDate();

    // Create the worksFrom and worksTo Date objects using the current date and adjusted time values
    const worksFromDateTime = new Date(`${currentYear}-${currentMonth}-${currentDay} ${worksFromTime}`);
    const worksToDateTime = new Date(`${currentYear}-${currentMonth}-${currentDay} ${worksToTime}`);

    // Assign the formatted date-time values to the clinic object
    this.clinic.worksFrom = worksFromDateTime.toISOString();
    this.clinic.worksTo = worksToDateTime.toISOString();

    console.log(this.clinic);
    this.clinicService.updateClinic(this.clinic).subscribe(res => {
      //this.clinic = res;
      console.log(res);
      //console.log(this.clinic.workHours![this.hours.dayOfWeek]);
      //console.log(this.workHours.day);

    });
  }

  adjustTimeByTimezone(time: string, timezoneOffset: number): string {
    const [hours, minutes] = time.split(':').map(Number);

    // Apply the timezone offset to adjust the time
    const adjustedHours = hours - timezoneOffset / 60;
    const adjustedTime = `${adjustedHours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}`;

    return adjustedTime;
  }
 
}
