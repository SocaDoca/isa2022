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
import { Questionnaire } from '../model/Questionnaire';



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
  userId: any;
  questionnaire!: Questionnaire;

  constructor(private modalService: NgbModal, private userService: UserService, private route: ActivatedRoute) {
    this.user = new UserLoadModel({
      role: '',
      username: '',
      firstName: '',
      lastName:'',
      fullAddress: '',
      mobile: '',
      job: '',
      gender: undefined,
      jmbg: '',
      password: '',
      penalty: 0

    })
  }





  ngOnInit(): void {
    this.loadProfile();
    this.loadQuestionnaire();
  }

  showGender(user: UserLoadModel) {
    if (user.gender == 0) {
      user.gender = 'Male';
    } else if (user.gender == 1) {
      user.gender = 'Female';
    } else
      user.gender = 'Other';
  }

  loadQuestionnaire() {
    this.userId = this.route.snapshot.params['id'];
    console.log(this.userId);
    this.userService.getQuestionnaire(this.userId).subscribe(res => {
      this.questionnaire = res;
      console.log(res);
    });
  }

  loadProfile() {
    this.id = this.route.snapshot.params['id'];
    this.userService.getUser(this.id)
      .subscribe(res => {
        this.user = res;
        this.showGender(res);
        this.user.penalty = res.penalty;
        const country = res.country ?? '';
        const address = res.address ?? '';
        const city = res.city ?? '';
        this.user.fullAddress = `${address}, ${city}, ${country}`;
        console.log(this.user.penalty);

      })

  }




}
