import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../service/authentication.service';


@Component({
  selector: 'app-navbar-profile',
  templateUrl: './navbar-profile.component.html',
  styleUrls: ['./navbar-profile.component.css']
})
export class NavbarProfileComponent {
  username: any;
  dash_url = '/';
  id: any;
  role: any;

  @Output()
  LogOut: EventEmitter<void> = new EventEmitter();

  constructor(private _http: HttpClient, private loginService: AuthenticationService, private router: Router) { }

  ngOnInit(): void {
  }
  logOut() {
    this.loginService.logOut();
    this.LogOut.next();
    this.router.navigate(['']);
  }

  showDashboard() {
    console.log(this.id)
    this.id = sessionStorage.getItem('id');
    this.role = sessionStorage.getItem('role');
    if (this.role == 'User') {
      this.router.navigate(['profile', this.id]);

    }  else if (this.role == 'Admin' || this.role == 'SysAdmin') {

      this.router.navigate(['profileEmployee', this.id]);
    } 

  }

}
