import { Injectable } from '@angular/core';
import { User } from './../app/model/User';
import { UserLoadModel } from './../app/model/UserLoadModel';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { UserRequest } from '../app/model/UserRequest';
import { ActivatedRoute } from '@angular/router';
import { map } from "rxjs/operators";
import { Questionnaire } from '../app/model/Questionnaire';
import { LoadAllUsersParameters } from '../app/model/LoadAllUsersParameters';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  url = "http://localhost:5017/User/get-by-id";
  urlQuestionnaire = "http://localhost:5017/User/get-questionnaire";
  urlRemoveUser = "http://localhost:5017/User/remove";
  urlUsers = "http://localhost:5017/User/get-all";
  urlEmployees = "http://localhost:5017/User/get-all-employees";
  urlUpdate = "http://localhost:5017/User/update-user";
  urlUpdatePass = "http://localhost:5017/User";
  urlSaveQuestionnaire = "http://localhost:5017/User/questionnaire";
  constructor(private http: HttpClient, private route: ActivatedRoute) {

  }


  getUser(id:string): Observable<User> {
    return this.http.get<User>(`${this.url}?id=${id}`);
  }

  getQuestionnaire(userId: string): Observable<Questionnaire> {
    return this.http.get<Questionnaire>(`${this.urlQuestionnaire}?userId=${userId}`);
  }

  removeUser(id: string) {
    return this.http.delete<any>(`${this.urlRemoveUser}?id=${id}`);
  }

  getAll(user: LoadAllUsersParameters): Observable<UserLoadModel[]> {
    return this.http.post<UserLoadModel[]>(`${this.urlUsers}`, user);
  }

  getEmployees() {
    return this.http.post<User[]>(`${this.urlEmployees}`, null);
  }

  updateUser(user: User): Observable<User> {
    return this.http.post<User>(`${this.urlUpdate}`, user);
  }

  saveQuestionnaire(questionnaire: Questionnaire) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post<boolean>(`${this.urlSaveQuestionnaire}`, questionnaire, { headers });
  }



  updatePassword(id: string, password: string) {
    return this.http.put<boolean>(`${this.urlUpdatePass}/${id}/update-password?password=${password}`, null).pipe(
      map((userData) => {

        return userData;
      })
    );

  }


}
