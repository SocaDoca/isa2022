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

  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        console.log(params); // { orderby: "price" }
        this.token = params['token'];
        this.email = params['email'];
        this.userId = params['userId'];
        console.log(this.token);
      });
  }

  verifyAcc() {
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
