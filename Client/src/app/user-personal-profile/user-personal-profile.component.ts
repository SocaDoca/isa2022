import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from '../../service/user.service';
import { UserLoadModel } from '../../app/model/UserLoadModel';
import { Genders } from '../model/Genders';
import { ModalDirective } from 'ngx-bootstrap/modal';



@Component({
  selector: 'app-user-personal-profile',
  templateUrl: './user-personal-profile.component.html',
  styleUrls: ['./user-personal-profile.component.css']
})
export class UserPersonalProfileComponent implements OnInit{
  title = 'appBootstrap';
  closeResult = '';
  id: any;
  user: UserLoadModel;
  addressList: string = '';
  password: any;

  constructor(private modalService: NgbModal, private userService: UserService, private route: ActivatedRoute) {
    this.user = new UserLoadModel({
      role: '',
      username: '',
      name: '',
      fullAddress: '',
      mobile: '',
      job: '',
      gender: undefined,
      jmbg: '',
      password:''

    })
  }





  ngOnInit(): void {
    this.loadProfile();
  }

  showGender(user: UserLoadModel) {
    if (user.gender == 0) {
      user.gender = 'Male';
    } else if (user.gender == 1) {
      user.gender = 'Female';
    } else
      user.gender = 'Other';
  }

  loadProfile() {
    this.id = this.route.snapshot.params['id'];
    this.userService.getUser(this.id)
      .subscribe(res => {
        this.user = res;
        this.showGender(res);
 

      })
  }




}
