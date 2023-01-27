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
  idUser: any;
  questionnaire: any;
  res: LoadAllUsersParameters;
  patients: UserLoadModel[] = [];
  patient!: UserLoadModel ;



  constructor(private route: ActivatedRoute, private userService: UserService) {
    this.res = new LoadAllUsersParameters({
      searchCriteria: '',
      offset: 0,
      limit: 10,
      sortBy: '',
      orderAsc:true
    }),
      this.questionnaire = new Questionnaire({
        isValid: undefined
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
        console.log(this.id);
        this.userService.getQuestionnaire(this.id).subscribe(res => {
          this.id = res;
        });

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
