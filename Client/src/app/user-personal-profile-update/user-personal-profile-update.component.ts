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
  addressList: string = '';
  nameList: string = '';
  password:any;


  constructor(private router: Router, private route: ActivatedRoute, private http: HttpClient, private userService: UserService) {
    this.user = new User({
      email: '',
      mobile: '',
      job: '',
      fullAddress: '',
      password: '',
      city: '',
      address: '',
      country: '',
      name: ''
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
        this.loadAddress(res);
        this.loadName(res);
        console.log(this.user);
      })
  }

  loadAddress(user:User) {
    this.addressList = this.user.fullAddress!;
    var splitted = this.addressList.split(",");
    var splittedAddress = splitted[0];
    var splittedCity = splitted[1];
    var splittedCountry = splitted[2];
    console.log(splittedAddress + splittedCity + splittedCountry);
    this.user.address = splittedAddress;
    this.user.city = splittedCity;
    this.user.country = splittedCountry;
  }

  loadName(user: User) {
    this.nameList = this.user.name!;
    var splitName = this.nameList.split(" ");
    var splFirstname = splitName[0];
    var splLastname = splitName[1];
    console.log(splFirstname + splLastname);
    this.user.firstName = splFirstname;
    this.user.lastName = splLastname;

  }


  showDashboard() {
    console.log(this.user)
    this.id = sessionStorage.getItem('id');
    this.role = sessionStorage.getItem('role');
    console.log(this.role);
    if (this.role == 'User') {
      console.log(this.user.password);
      this.userService.updateUser(this.user)
        .subscribe(res => {
          this.router.navigate(['/profile', this.id])
        });

    } else if (this.role == 'Admin' || this.role == 'SysAdmin') {

      this.userService.updateUser(this.user)
        .subscribe(res => this.router.navigate(['/profileEmployee', this.id]));
    }

  }


// click event function toggle
pass() {
    this.show = !this.show;
}
}
