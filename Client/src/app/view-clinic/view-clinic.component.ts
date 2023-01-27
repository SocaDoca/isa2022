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
    this.role = sessionStorage.getItem('role');
    if (this.role == 'Admin') {
      this.role = 1;
      console.log(this.role);
    }



  }

  loadClinic() {
    this.id = this.route.snapshot.params['id'];

    this.clinicService.getAll(this.res)
      .subscribe(res => {
        this.clinics = res;
        console.log(this.clinics);
      }

      )
  }
}
