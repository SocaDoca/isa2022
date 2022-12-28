import { Component, OnInit } from '@angular/core';
import { User } from './../model/User';
import { RegistrationService } from "../../service/registration.service";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { Genders } from './../model/Genders';
import { Role } from './../model/Role';
import { UserRequest } from './../model/UserRequest';
import { UserService } from "../../service/user.service";
import { FormControl, FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { ReactiveFormsModule } from "@angular/forms";
import Validation from './../utils/validation';



@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit{
  form: FormGroup = new FormGroup({
    firstName: new FormControl(''),
    username: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
    //acceptTerms: new FormControl(false),
  });
  submitted: boolean = false;
  show: boolean = false;
  which_gender = Genders;

  confirmedPassword: string | undefined;

  constructor(private registrationService: RegistrationService, private router: Router, private http: HttpClient, private userService: UserService, private formBuilder: FormBuilder) {

}
  ngOnInit(): void {
    this.form = this.formBuilder.group(
      {
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        country: ['', Validators.required],
        city: ['', Validators.required],
        address: ['', Validators.required],
        job: ['', Validators.required],
        username: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(40)
          ]
        ],
        confirmPassword: ['', Validators.required],
        acceptTerms: [false, Validators.requiredTrue]
      },
      {
        validators: [Validation.match('password', 'confirmPassword')]
      }
    );
  }


  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    } else {
      this.addNewUser();
    }

    console.log(JSON.stringify(this.form.value, null, 2));
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
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
  firstName : string | undefined;

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
    if (this.form.invalid) {
      return;
    } else {


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

      });
      this.router.navigate(['sign-in'])
      /*} else {
        this.error = "passwords are not equal";
      }*/
    }
   }

  

// click event function toggle
  password() {
      this.show = !this.show;
  }
}
