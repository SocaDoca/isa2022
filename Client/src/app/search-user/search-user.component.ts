import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../service/user.service';
import { LoadAllUsersParameters } from '../model/LoadAllUsersParameters';
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
  res: LoadAllUsersParameters;
  loggedUser: UserLoadModel[] =[];



  constructor(private route: ActivatedRoute, private userService: UserService) {
    this.res = new LoadAllUsersParameters({
      searchCriteria: '',
      offset: 0,
      limit: 10,
      sortBy: '',
      orderAsc:true
    })
  }

  ngOnInit(): void {

  }

  loadUser() {
    this.id = this.route.snapshot.params['id'];
    this.userService.getAll(this.res)
      .subscribe(res => {
        this.loggedUser = res;
        console.log(this.res);
      }

      )
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
