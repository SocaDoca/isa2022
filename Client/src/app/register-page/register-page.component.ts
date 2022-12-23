import { Component, OnInit } from '@angular/core';
import { User } from './../model/User';
import { RegistrationService } from "../../service/registration.service";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { Genders } from './../model/Genders';
import { Role } from './../model/Role';
import { UserRequest } from './../model/UserRequest';
import { UserService } from "../../service/user.service";



@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit{
  show: boolean = false;
  which_gender = Genders;
  
  confirmedPassword: string | undefined;


  constructor(private registrationService: RegistrationService, private router: Router, private http: HttpClient, private userService: UserService) {
}
    ngOnInit(): void {
        
  }
  newUser: User = new User({
    //id: 0,
    firstName: '',
    lastName: '',
    username: '',
    email:'',
    address: '',
    gender:0,
    role: '',
    job: '',
    password: '',
    country: '',
    city:''

  });
  choices_for_gender = ['Female', 'Male', 'Other'];
  choices_for_roles = ['Guest', 'Employee', 'Admin'];
  error: string | undefined;
  which_role = Role;

  registrationRequest: UserRequest = new UserRequest({
    //id: 0,
    username: this.newUser.username,
    password: this.newUser.password,
    firstName: this.newUser.firstName,
    lastName: this.newUser.lastName,
    address: this.newUser.address,
    email: this.newUser.email,
    job: this.newUser.job,
    role: this.newUser.role,
    gender: this.newUser.gender,
   // moblie: this.newUser.moblie
    country: this.newUser.country,
    city: this.newUser.city

  })


  addNewUser() {

    //if (this.newUser.password == this.confirmedPassword) {
    //this.registrationRequest.id = this.newUser.id;
      this.registrationRequest.username = this.newUser.username;
      this.registrationRequest.password = this.newUser.password;
      this.registrationRequest.firstName = this.newUser.firstName;
      this.registrationRequest.lastName = this.newUser.lastName;
      this.registrationRequest.gender = this.newUser.gender;
      this.registrationRequest.email = this.newUser.email;
      this.registrationRequest.address = this.newUser.address;
      this.registrationRequest.job = this.newUser.job;
    //this.registrationRequest.moblie = this.newUser.moblie;
      this.registrationRequest.country = this.newUser.country;
      this.registrationRequest.city = this.newUser.city;
    /*if (this.which_gender == Genders.Female) {
      this.registrationRequest.gender == this.newUser.gender;
    }*/

      this.registrationService.registerUser(this.registrationRequest).subscribe(res => {
        this.newUser = res
      //this.userService.saveUser(res).subscribe(
      //);
        
     } );
      this.router.navigate(['sign-in'])
    /*} else {
      this.error = "passwords are not equal";
    }*/

    }

  

// click event function toggle
  password() {
      this.show = !this.show;
  }
}
