import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../service/user.service';
import { User } from '../model/User';
import { UserLoadModel } from '../model/UserLoadModel';

@Component({
  selector: '[app-search-user]',
  templateUrl: './search-user.component.html',
  styleUrls: ['./search-user.component.css']
})
export class SearchUserComponent {
  id: any;
  user: any;
  loggedUser: UserLoadModel[] = [];



  constructor(private route: ActivatedRoute, private userService: UserService) {
  }

  ngOnInit(): void {
    this.loadUser();
  }

  loadUser() {
    this.id = this.route.snapshot.params['id'];
    this.userService.getAll()
      .subscribe(res => {
        this.user = res;
        console.log(this.user);
      }

      )
  }



}
