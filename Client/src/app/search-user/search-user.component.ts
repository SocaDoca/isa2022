import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../service/user.service';
import { LoadAllUsersParameters } from '../model/LoadAllUsersParameters';
import { Questionnaire } from '../model/Questionnaire';
import { User } from '../model/User';
import { UserLoadModel } from '../model/UserLoadModel';

@Component({
  selector: '[app-search-user]',
  templateUrl: './search-user.component.html',
  styleUrls: ['./search-user.component.css']
})
export class SearchUserComponent {
  @Input()
  id: any;
  userId: any;
  role: any;
  questionnaire: any;
  res: LoadAllUsersParameters;
  patients: UserLoadModel[] = [];
  patient: UserLoadModel ;



  constructor(private route: ActivatedRoute, private userService: UserService) {
    this.res = new LoadAllUsersParameters({
      searchCriteria: '',
      offset: 0,
      limit: 10,
      sortBy: '',
      orderAsc: true
    }),
      this.questionnaire = new Questionnaire({
        isValid: undefined
      }),
      this.patient = new UserLoadModel({
        id: ''
      })
  }

  ngOnInit(): void {
   // this.loadQuestionnaire();
  }

  loadUsers() {
    //this.id = this.route.snapshot.params['id'];
    this.userService.getAll(this.res)
      .subscribe(res => {
        this.patients = res;
        //this.id = this.patients['id'];
        //this.id = Object.values()
        //console.log(this.patients);
        //this.id = this.patients.find(({ id }) => id);
        console.log(this.patients);
        this.role = this.patients[1].role;
        console.log(this.role);
        if (this.role == 'Admin') {
          this.questionnaire.isValid = false;
          console.log('Da ovo je admin ' + this.questionnaire.isValid);
        } else if(this.role == 'User') {
          this.questionnaire.isValid = true;
          console.log('Ovo ipak nije admin ' + this.questionnaire.isValid);
          this.userId = this.patients[1].id;
          console.log(this.userId);
          this.userService.getQuestionnaire(this.userId).subscribe(res => {
            this.questionnaire = res;
            console.log(this.questionnaire);
          });
        }



      }
    );

  }

  /*loadQuestionnaire() {
    this.userService.getQuestionnaire(this.id).subscribe(res => {
      this.id = res;
    })
    //this.isValid = this.route.snapshot.params['isValid'];
    console.log(this.id);
  }*/

  removeUser() {
    console.log(this.id);
    this.id = this.route.snapshot.params['id'];
    this.userService.removeUser(this.id).subscribe(res => {
      this.res = res;

    });
    window.location.reload();
  }



}
