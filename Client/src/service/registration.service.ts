import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Observable, throwError } from "rxjs";
import { User } from "./../app/model/User";
import { UserRequest } from "../app/model/UserRequest";
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  url = "http://localhost:5017/Auth/register";
  constructor(private http: HttpClient, private snackbar: MatSnackBar) { }

  registerUser(newUser: UserRequest): Observable<User> {
    return this.http.post<UserRequest>(this.url, newUser).pipe(
    catchError(e => {
      this.snackbar.open('Doslo je do greske zbog: ${e.error.message}');
      return throwError(e);
    }))
  }
}
