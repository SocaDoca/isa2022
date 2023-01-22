import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { AuthenticationService } from "../../service/authentication.service";
import { ActivatedRoute, Route, Router } from "@angular/router";
import { FormControl, FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { ReactiveFormsModule } from "@angular/forms";
import Validation from './../utils/validation';
import { User } from '../model/User';

@Component({
  selector: 'app-verify-user',
  templateUrl: './verify-user.component.html',
  styleUrls: ['./verify-user.component.css']
})
export class VerifyUserComponent {
  token: any;
  userId: any;
  email: any;


  constructor(private authenticationService: AuthenticationService, private router: Router, private formBuilder: FormBuilder, private route: ActivatedRoute) {
}


  verifyAcc() {
   // this.token = sessionStorage.getItem('token');
   // this.userId = sessionStorage.getItem('id');
   // this.email = sessionStorage.getItem('email');
    this.userId = this.route.snapshot.paramMap.get('userId');
    this.email = this.route.snapshot.paramMap.get('email')!;
    this.token = this.route.snapshot.paramMap.get('token');
    this.authenticationService.verify(this.token, this.email, this.userId).subscribe(
      (data: any) => {
        // let token = data['token'];
        //let email = data['email'];
        //let userId = data['userId']
        console.log(data)
        this.router.navigate(['sign-in'], data);
      });
  }



}
