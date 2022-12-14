import { Component, OnInit } from '@angular/core';
import { User } from './../model/User';
import { RegistrationService } from "../../service/registration.service";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { Genders } from './../model/Genders';
import { Roles } from './../model/Roles';
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
    roles: new FormControl('')

  });
  submitted: boolean = false;
  show: boolean = false;
  which_gender = Genders;

  private genders =  Genders;


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
        mobile: ['', Validators.required],
        jmbg: ['', [Validators.required, Validators.maxLength(13), Validators.minLength(13)]],
        roles: ['', Validators.required],
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

    firstName: '',
    lastName: '',
    username: '',
    email: '',
    address: '',
    gender: 0,
    roles: '',
    job: '',
    password: '',
    confirmPassword: '',
    country: '',
    city: '',
    jmbg: '',
    mobile: ''

  });
  choices_for_gender = ['Female', 'Male', 'Other'];



  registrationRequest: UserRequest = new UserRequest({
    username: this.newUser.username,
    password: this.newUser.password,
    firstName: this.newUser.firstName,
    lastName: this.newUser.lastName,
    address: this.newUser.address,
    email: this.newUser.email,
    job: this.newUser.job,
    jmbg: this.newUser.jmbg,
    confirmPassword: this.newUser.confirmPassword,
    roles: this.newUser.roles,
    gender: this.newUser.gender,
    mobile: this.newUser.mobile,
    country: this.newUser.country,
    city: this.newUser.city
  })


  addNewUser() {



      this.registrationRequest.username = this.newUser.username;
      this.registrationRequest.password = this.newUser.password;
      this.registrationRequest.confirmPassword = this.newUser.confirmPassword;
      this.registrationRequest.firstName = this.newUser.firstName;
      this.registrationRequest.lastName = this.newUser.lastName;
      this.registrationRequest.gender = this.newUser.gender;
      this.registrationRequest.email = this.newUser.email;
      this.registrationRequest.address = this.newUser.address;
      this.registrationRequest.job = this.newUser.job;
      this.registrationRequest.jmbg = this.newUser.jmbg;
      this.registrationRequest.roles = this.newUser.roles;
    this.registrationRequest.mobile = this.newUser.mobile;
      this.registrationRequest.country = this.newUser.country;
      this.registrationRequest.city = this.newUser.city;


      this.registrationService.registerUser(this.registrationRequest).subscribe(res => {
        this.newUser = res
        //this.userService.saveUser(res).subscribe(
        //);

      });
      this.router.navigate(['sign-in'])
      /*} else {
        this.error = "passwords are not equal";
      }*/
  //  }
   }

  

// click event function toggle
  password() {
      this.show = !this.show;
  }
}
