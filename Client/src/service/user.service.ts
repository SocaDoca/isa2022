import { Injectable } from '@angular/core';
import { User } from './../app/model/User';
import { UserLoadModel } from './../app/model/UserLoadModel';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { UserRequest } from '../app/model/UserRequest';
import { ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  url = "http://localhost:5017/User/get-by-id";
  urlUpdate = "http://localhost:5017/User/update";
  constructor(private http: HttpClient, private route: ActivatedRoute) {

  }


  getUser(id:string): Observable<User> {
    return this.http.get<User>(`${this.url}?id=${id}`);
  }

  updateUser(user: User): Observable<User> {
    return this.http.patch<User>(`${this.urlUpdate}`, user);
  }

 /* updatePassword(id:string, password:string): Observable<UserLoadModel> {
    return this.http.put<UserLoadModel>(`${this.urlUpdate}/${id}/update-password/?id=${id}?password=${password}', user);
  }*/

}

