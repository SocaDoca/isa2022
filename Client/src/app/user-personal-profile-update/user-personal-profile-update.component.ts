import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../service/user.service';
import { User } from '../../app/model/User';
import { UserLoadModel } from '../model/UserLoadModel';
import { UserRequest } from '../model/UserRequest';

@Component({
  selector: 'app-user-personal-profile-update',
  templateUrl: './user-personal-profile-update.component.html',
  styleUrls: ['./user-personal-profile-update.component.css']
})
export class UserPersonalProfileUpdateComponent {
  show: boolean = false;
  user: User;
  id: any;
  role: any;

  constructor(private router: Router, private route: ActivatedRoute, private http: HttpClient, private userService: UserService) {
    this.user = new User({
      email: "",
      mobile: "",
      job: "",
      fullAddress: "",
      password: "",
      city: "",
      address: "",
      country:""
    })
  }

  ngOnInit(): void {
    this.loadProfile();
  }
  loadProfile() {
    this.id = this.route.snapshot.params['id'];
    this.userService.getUser(this.id)
      .subscribe(res => {
        this.user = res;
      })
  }


  showDashboard() {
    console.log(this.id)
    this.id = sessionStorage.getItem('id');
    this.role = sessionStorage.getItem('role');
    if (this.role == 'User') {
      this.userService.updateUser(this.user)
        .subscribe(res => this.router.navigate(['/profile', res.id]));

    } else if (this.role == 'Admin' || this.role == 'SysAdmin') {

      this.userService.updateUser(this.user)
        .subscribe(res => this.router.navigate(['/profileEmployee', res.id]));
    }

  }


// click event function toggle
password() {
    this.show = !this.show;
}
}
