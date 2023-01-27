import { Injectable } from '@angular/core';
import { User } from './../app/model/User';
import { UserLoadModel } from './../app/model/UserLoadModel';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { UserRequest } from '../app/model/UserRequest';
import { ActivatedRoute } from '@angular/router';
import { map } from "rxjs/operators";
import { DbClinic } from '../app/model/DbClinic';
import { ClinicSaveModel } from '../app/model/ClinicSaveModel';
import { WorkingHours } from '../app/model/WorkingHours';
import { ClinicLoadParameters } from '../app/model/ClinicLoadParameters';

@Injectable({
  providedIn: 'root'
})
export class ClinicService {
  private access_token = null;
  url = "http://localhost:5017/Clinic/save-clinic";
  urlgetById = "http://localhost:5017/Clinic/get-clinic-by-id";
  urlgetAll = "http://localhost:5017/Clinic/load-all-clinics";
  urlUpdate = "http://localhost:5017/Clinic/update-clinic";
  urlWorkingHours = "http://localhost:5017/WorkingHours/save-working-hours";

  constructor(private http: HttpClient, private route: ActivatedRoute) {

  }


  saveClinic(clinic: ClinicSaveModel) {
    return this.http.post<ClinicSaveModel>(`${this.url}`, clinic)

  }

  getClinicById(id: string): Observable<ClinicSaveModel>{
    return this.http.get<ClinicSaveModel>(`${this.urlgetById}?id=${id}`);

  }

  updateClinic(clinic: ClinicSaveModel) {
    return this.http.post<boolean>(`${this.urlUpdate}`, clinic);

  }

  saveWorkingHours(hours: WorkingHours) {
    return this.http.post<WorkingHours>(`${this.url}`, hours)

  }



  getAll(clinic: ClinicLoadParameters): Observable<DbClinic[]> {
    return this.http.post<DbClinic[]>(`${this.urlgetAll}`, clinic);
  }





  }



