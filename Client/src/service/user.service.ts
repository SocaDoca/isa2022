import { Injectable } from '@angular/core';
import { User } from './../app/model/User';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  url = "http://localhost:5017/Users/saveUser";
  constructor(private http: HttpClient) { }

  saveUser(user: User): Observable<User> {
    return this.http.post<User>(this.url, user);
  }


}
