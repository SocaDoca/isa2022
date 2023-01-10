import { Injectable } from '@angular/core';
import { User } from './../app/model/User';
import { UserLoadModel } from './../app/model/UserLoadModel';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { UserRequest } from '../app/model/UserRequest';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  url = "http://localhost:5017";
  urlUpdate = "http://localhost:5017/User/update";
  constructor(private http: HttpClient) { }


  getUser(id: string): Observable<User> {
    return this.http.get<User>(`${this.url}/${id}`);
  }

  updateUser(user: User): Observable<User> {
    return this.http.patch<User>(`${this.urlUpdate}`, user);
  }

}
