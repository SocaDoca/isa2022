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

@Injectable({
  providedIn: 'root'
})
export class ClinicService {
  private access_token = null;
  url = "http://localhost:5017/Clinic/save-clinic";
  urlWorkingHours = "http://localhost:5017/WorkingHours/save-working-hours";

  constructor(private http: HttpClient, private route: ActivatedRoute) {

  }


  saveClinic(clinic: ClinicSaveModel) {
    return this.http.post<ClinicSaveModel>(`${this.url}`, clinic)
      /*.pipe(
      map((userData) => {
        sessionStorage.setItem("currentUser", JSON.stringify(userData))
        let tokenStr = "Bearer " + userData.token.accessToken;
        this.access_token = userData.token.accessToken;
        sessionStorage.setItem("token", tokenStr);
        sessionStorage.setItem("id", userData.id);
        return userData;
      });*/
    //);
  }

  saveWorkingHours(hours: WorkingHours) {
    return this.http.post<WorkingHours>(`${this.url}`, hours)

  }





  }



