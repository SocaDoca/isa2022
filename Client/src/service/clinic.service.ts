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
import { Complaint } from '../app/model/Complaint';

@Injectable({
  providedIn: 'root'
})
export class ClinicService {
  private access_token = null;
  url = "http://localhost:5017/Clinic/save-clinic";
  urlgetById = "http://localhost:5017/Clinic/get-clinic-by-id";
  urlgetAll = "http://localhost:5017/Clinic/load-all-clinics";
  urlgetAllClinics = "http://localhost:5017/Clinic/load-dropdown-clinics";
  urlgetAllTerms = "http://localhost:5017/Appointment/patient/appointmets";
  urlgetAllClinicIdTerms = "http://localhost:5017/Appointment/clinic/appointmets";
  urlgetOneTerm = "http://localhost:5017/Appointment/get-appointment";
  urlUpdate = "http://localhost:5017/Clinic/update-clinic";
  urlWorkingHours = "http://localhost:5017/WorkingHours/save-working-hours";
  urlTerm = "http://localhost:5017/Appointment/create-predefiend-appointment";
  urlAppoint = "http://localhost:5017/Appointment/reserve-predefiend-appointment";
  urlStartTerm = "http://localhost:5017/Appointment/start-predefiend-appointment";
  urlAppointOnClick = "http://localhost:5017/Appointment/save-onClick";
  urlgetAllReservedAppointments = "http://localhost:5017/Appointment/load-reserved-appointments"
  urlSaveComplaint = "http://localhost:5017/Complaints/save-complaints";
  urlAnswerComplaint = "http://localhost:5017/Complaints/answer-complaint";
  urlgetAllComplaints = "http://localhost:5017/Complaints/load-all-complaints";
  urlCancelAppointment = "http://localhost:5017/Appointment/cancel-appointment"

  constructor(private http: HttpClient, private route: ActivatedRoute) {

  }


  saveClinic(clinic: ClinicSaveModel) {
    return this.http.post<ClinicSaveModel>(`${this.url}`, clinic)

  }

  getClinicById(Id: string): Observable<ClinicSaveModel> {
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

  getAllClinics(clinic: ClinicLoadParameters): Observable<DbClinic[]> {
    return this.http.post<DbClinic[]>(`${this.urlgetAllClinics}`, clinic);
  }

  getAllTerms(id: string) {
    return this.http.post<DbClinic[]>(`${this.urlgetAllTerms}?patientId=${id}`,id);
  }

  getAllTermsByClinicId(clinicId: string) {
    return this.http.get<DbAppointment[]>(`${this.urlgetAllClinicIdTerms}?clinicId=${clinicId}`);
  }

  getTermById(Id: string) {
    return this.http.get<DbAppointment>(`${this.urlgetOneTerm}?id=${Id}`);
  }

  addPredefinedTerm(term: PredefinedTerm): Observable<DbAppointment[]> {
    return this.http.post<DbAppointment[]>(`${this.urlTerm}`, term);
  }

  startTerm(id: string){
    return this.http.post<boolean>(`${this.urlStartTerm}?appotinmentId=${id}`, id);
  }

  reserveAppointment(appointmentId: string, patientId: string) {
    const body = { appointmentId, patientId }; // Create the request body object
    const headers = { 'Content-Type': 'application/json' };

    return this.http.post<boolean>(`${this.urlAppoint}`, body, { headers });
  }



  getReservedAppointments() {
    return this.http.get<DbAppointment[]>(`${this.urlgetAllReservedAppointments}`,);
  }

  saveAppointmentOnClick(appointment: DbAppointment): Observable<DbAppointment> {
    return this.http.post<DbAppointment>(`${this.urlAppointOnClick}`, appointment);
  }

  saveComplaint(complaint: Complaint): Observable<Complaint> {
    return this.http.post<Complaint>(`${this.urlSaveComplaint}`, complaint);
  }

  cancelAppointment(Id: string) {
    return this.http.post<boolean>(`${this.urlCancelAppointment}?appointmenetId=${Id}`, Id);
  }

  getAllComplaints(complaint: Complaint) {
    return this.http.post<Complaint[]>(`${this.urlgetAllComplaints}`, null);
  }

  answerComplaint(complaintId: any, answer: string) {
    const body = { complaintId, answer }; // Create the request body object

    // Set the headers to specify the content type
    const headers = { 'Content-Type': 'application/json' };

    return this.http.post<boolean>(`${this.urlAnswerComplaint}`, body, { headers });
  }

}
