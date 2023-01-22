import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import {User} from "./../app/model/User";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private access_token = null;
  url = "http://localhost:5017/Auth/verify-user";

  constructor(private http: HttpClient) { }
  authenticate(email: string, password: string) {

    return this.http
      .post<any>("http://localhost:5017/Auth/login", { email, password })
      .pipe(
        map((userData) => {
          sessionStorage.setItem("currentUser", JSON.stringify(userData))
          sessionStorage.setItem("email", email);
          let tokenStr = "Bearer " + userData.token.accessToken;
          this.access_token = userData.token.accessToken;
          sessionStorage.setItem("token", tokenStr);
          sessionStorage.setItem("id", userData.id);
          sessionStorage.setItem("role", userData.role);
          sessionStorage.setItem("firstLogin", 'true');
          return userData;
        })
      );
  }

  verify(token: string, email:string, userId:string)  {
    return this.http
      .post<any>(`${this.url}?token=${token}&email=${email}&userId=${userId}`, null)
      .pipe(
        map((userData) => {

          return userData;
        })
      );
  }

  tokenIsPresent() {
    return this.access_token != undefined && this.access_token != null;
  }

  getToken() {
    return sessionStorage.getItem("token");
  }


  isUserLoggedIn() {
    let username = sessionStorage.getItem("email");

    console.log(!(username === null));
    return !(username === null);
  }

  logOut() {
    sessionStorage.removeItem("email");
    sessionStorage.removeItem("token");
    sessionStorage.removeItem("currentUser");
    sessionStorage.removeItem("id");
    sessionStorage.removeItem("role");

}
}
