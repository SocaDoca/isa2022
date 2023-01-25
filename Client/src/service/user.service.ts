import { Injectable } from '@angular/core';
import { User } from './../app/model/User';
import { UserLoadModel } from './../app/model/UserLoadModel';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { UserRequest } from '../app/model/UserRequest';
import { ActivatedRoute } from '@angular/router';
import { map } from "rxjs/operators";
import { Questionnaire } from '../app/model/Questionnaire';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  url = "http://localhost:5017/User/get-by-id";
  urlRemoveUser = "http://localhost:5017/User/remove";
  urlUsers = "http://localhost:5017/User/get-all";
  urlUpdate = "http://localhost:5017/User/update-user";
  urlUpdatePass = "http://localhost:5017/User";
  urlSaveQuestionnaire = "http://localhost:5017/User/questionnaire";
  constructor(private http: HttpClient, private route: ActivatedRoute) {

  }


  getUser(id:string): Observable<User> {
    return this.http.get<User>(`${this.url}?id=${id}`);
  }

  removeUser(id: string) {
    return this.http.delete<any>(`${this.urlRemoveUser}?id=${id}`);
  }

  getAll(): Observable<UserLoadModel> {
    return this.http.get<UserLoadModel>(`${this.urlUsers}`);
  }

  updateUser(user: User): Observable<User> {
    return this.http.post<User>(`${this.urlUpdate}`, user);
  }

  saveQuestionnaire(questionnaire: Questionnaire, patientId: string): Observable<Questionnaire> {
    return this.http.post<Questionnaire>(`${this.urlSaveQuestionnaire}?patientId=${patientId}`, questionnaire);
  }



  updatePassword(id: string, password: string) {
    return this.http.put<boolean>(`${this.urlUpdatePass}/${id}/update-password?password=${password}`, null).pipe(
      map((userData) => {

        return userData;
      })
    );

  }


}
