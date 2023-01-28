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
import { PredefinedTerm } from '../app/model/PredefinedTerm';
import { DbAppointment } from '../app/model/DbAppointment';

@Injectable({
  providedIn: 'root'
})
export class ClinicService {
  private access_token = null;
  url = "http://localhost:5017/Clinic/save-clinic";
  urlgetById = "http://localhost:5017/Clinic/get-clinic-by-id";
  urlgetAll = "http://localhost:5017/Clinic/load-all-clinics";
  urlgetAllTerms = "http://localhost:5017/Appointment/patient/appointmets";
  urlgetOneTerm = "http://localhost:5017/Appointment/get-appointment";
  urlUpdate = "http://localhost:5017/Clinic/update-clinic";
  urlWorkingHours = "http://localhost:5017/WorkingHours/save-working-hours";
  urlTerm = "http://localhost:5017/Appointment/save-predefiend-appointment";
  urlAppoint = "http://localhost:5017/Appointment/save-appointment";

  constructor(private http: HttpClient, private route: ActivatedRoute) {

  }


  saveClinic(clinic: ClinicSaveModel) {
    return this.http.post<ClinicSaveModel>(`${this.url}`, clinic)

  }

  getClinicById(Id: string): Observable<ClinicSaveModel>{
    return this.http.post<ClinicSaveModel>(`${this.urlgetById}?id=${Id}`, Id);

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

  getAllTerms(patientId: string){
    return this.http.get<DbClinic[]>(`${this.urlgetAllTerms}?id=${patientId}`);
  }

  getTermById(Id: string) {
    return this.http.get<DbAppointment>(`${this.urlgetOneTerm}?id=${Id}`);
  }

  addPredefinedTerm(term: PredefinedTerm): Observable<DbAppointment[]> {
    return this.http.post<DbAppointment[]>(`${this.urlTerm}`, term);
  }

  saveAppointment(appointmentSave: DbAppointment): Observable<DbAppointment> {
    return this.http.post<DbAppointment>(`${this.urlAppoint}`, appointmentSave);
  }


  }



