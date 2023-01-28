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
      this.patient = new UserLoadModel({
        id: ''
      })
  }

  ngOnInit(): void {
   // this.loadQuestionnaire();
    this.id = this.route.params.subscribe(
      params => {
        this.patient.id = params['userId'];
      });

  }

  loadUsers() {
    //this.id = this.route.snapshot.params['id'];
    this.userService.getAll(this.res)
      .subscribe(res => {
        this.patients = res;
          this.userId = res.find(x => x.role === 'User')!.id;
        console.log(this.userId);
        /*this.userService.getQuestionnaire(this.userId).subscribe(res => {
          this.questionnaire = res;
          console.log(this.questionnaire);
          this.questionnaire.isValid = res.isValid;
          console.log(this.questionnaire.isValid);
        });*/
      });

        }








  removeUser() {
    console.log(this.id);
    this.id = this.route.snapshot.params['id'];
    this.userService.removeUser(this.id).subscribe(res => {
      this.res = res;

    });
    window.location.reload();
  }



}
