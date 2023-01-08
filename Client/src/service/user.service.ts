import { Injectable } from '@angular/core';
import { User } from './../app/model/User';
import { UserLoadModel } from './../app/model/UserLoadModel';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  url = "http://localhost:5017/User/user";
  constructor(private http: HttpClient) { }


  getUser(id: string): Observable<UserLoadModel> {
    return this.http.get<UserLoadModel>(`${this.url}/${id}`);
  }

}
