import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { AuthenticationService } from "../../service/authentication.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-sign-in-page',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.css']
})
export class SignInPageComponent implements OnInit {
  username: any
  password = ''
  invalidLogin = false;
  @Output()
  LogIn: EventEmitter<void> = new EventEmitter();

  @Input() error: string | null | undefined;
  constructor(private router: Router, private loginservice: AuthenticationService) { }


  ngOnInit(): void {
  }
  login() {
    if (this.username == '' || this.password == '')
      this.error = "Username and password must be filled out";
    else {
      this.loginservice.authenticate(this.username, this.password).subscribe(
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

}
