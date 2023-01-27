import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { ClinicLoadParameters } from '../model/ClinicLoadParameters';
import { ClinicSaveModel } from '../model/ClinicSaveModel';
import { WorkingHours } from '../model/WorkingHours';

@Component({
  selector: 'app-search-clinic',
  templateUrl: './search-clinic.component.html',
  styleUrls: ['./search-clinic.component.css']
})
export class SearchClinicComponent {
  @Input()
  id: any;
  res: ClinicLoadParameters;
  clinics: ClinicSaveModel[] = [];
  workHours!: WorkingHours;



  constructor(private route: ActivatedRoute, private clinicService: ClinicService) {
    this.res = new ClinicLoadParameters({
      searchCriteria: '',
      offset: 0,
      limit: 10,
      sortBy: '',
      orderAsc: true
    })
  }

  ngOnInit(): void {

  }

  loadClinic() {
    this.id = this.route.snapshot.params['id'];
    this.clinicService.getAll(this.res)
      .subscribe(res => {
        this.clinics = res;
        console.log(this.res);

      }

      )
  }
}

