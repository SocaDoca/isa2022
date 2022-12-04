import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import {Employee} from "../app/model/DbEmployee";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private access_token = null;

  constructor(private http : HttpClient) { }
  /*authenticate(korisnicko: string, password: string) {

    return this.http
      .post<any>("http://localhost:8082/auth/login", { korisnicko, password })
      .pipe(
        map((userData) => {
          sessionStorage.setItem("currentUser", JSON.stringify(userData))
          sessionStorage.setItem("username", korisnicko);
          let tokenStr = "Bearer " + userData.token.accessToken;
          this.access_token = userData.token.accessToken;
          sessionStorage.setItem("token", tokenStr);
          sessionStorage.setItem("id", userData.id);
          sessionStorage.setItem("role", userData.role);
          sessionStorage.setItem("firstLogin", 'true');
          return userData;
        })
      );
  }*/
  tokenIsPresent() {
    return this.access_token != undefined && this.access_token != null;
  }

  getToken() {
    return sessionStorage.getItem("token");
  }



  isUserLoggedIn() {
    let username = sessionStorage.getItem("username");

    console.log(!(username === null));
    return !(username === null);
  }

  logOut() {
    sessionStorage.removeItem("username");
    sessionStorage.removeItem("token");
    sessionStorage.removeItem("currentUser");
    sessionStorage.removeItem("id");
    sessionStorage.removeItem("role");

}
}
