import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { AuthenticationService } from "../../service/authentication.service";
import { Router } from "@angular/router";
import { FormControl, FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { ReactiveFormsModule } from "@angular/forms";
import Validation from './../utils/validation';


@Component({
  selector: 'app-sign-in-page',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.css']
})
export class SignInPageComponent implements OnInit {
  form: FormGroup = new FormGroup({
    firstName: new FormControl(''),
    username: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    //confirmPassword: new FormControl(''),
    //acceptTerms: new FormControl(false),
  });
  username: any;
  email: any;
  password: any;
  role: any;
  id: any;
  show: boolean = false;
  invalidLogin = false;
  submitted: boolean = false;

  @Output()
  LogIn: EventEmitter<void> = new EventEmitter();

  @Input() error: string | null | undefined;
  constructor(private router: Router, private loginservice: AuthenticationService, private formBuilder: FormBuilder) { }


  ngOnInit(): void {
    this.form = this.formBuilder.group(
      {

        email: ['', Validators.required],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(3),
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
      this.login();
    }

    console.log(JSON.stringify(this.form.value, null, 2));
  }
  
  login() {
    if (this.email == '' || this.password == '')
      this.error = "Username and password must be filled out";
    else {
      this.loginservice.authenticate(this.email, this.password).subscribe(
        (data: any) => {
          console.log(data)
          this.LogIn.next();
          //window.location.reload();
          this.router.navigate(['']);
         
          this.invalidLogin = false
        },
        (error: { message: string | null; }) => {
          this.invalidLogin = true
          this.error = "Invalid username or password or your account is not active";

        })
    }
  }

  // click event function toggle
  password1() {
    this.show = !this.show;
  }

}
