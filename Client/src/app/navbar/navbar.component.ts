import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../service/authentication.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  isActive: boolean = false;
  role: any;
  visiable_instrucotrs_button!: boolean;

  constructor(public loginService: AuthenticationService) { }

  ngOnInit(): void {
    this.role = sessionStorage.getItem('role');
    if (this.role == 'User') {
      this.visiable_instrucotrs_button = true;
    } else {
      this.visiable_instrucotrs_button = false;
    }
  }

  HideInstrucotsButton() {
    this.role = sessionStorage.getItem('role');
    if (this.role == 'User') {
      this.visiable_instrucotrs_button = true;
    } else {
      this.visiable_instrucotrs_button = false;
    }
  }
  SignIn() {
    this.role = sessionStorage.getItem('role');
    if (this.role == 'User') {
      this.visiable_instrucotrs_button = true;
    } else {
      this.visiable_instrucotrs_button = false;
    }
  }

}
