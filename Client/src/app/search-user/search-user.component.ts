import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../service/user.service';
import { User } from '../model/User';
import { UserLoadModel } from '../model/UserLoadModel';

@Component({
  selector: 'app-search-user',
  templateUrl: './search-user.component.html',
  styleUrls: ['./search-user.component.css']
})
export class SearchUserComponent {
  id: any;
  loggedUser!: User;

  @Input()
  users: string[] = [];

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit(): void {
    this.loadUser();
  }

  loadUser() {
    this.id = this.route.snapshot.params['id'];
    //this.idLoginUser = sessionStorage.getItem('id');
    this.userService.getAll()
      .subscribe(res => {
        this.loggedUser = res;
        //this.connectionService.getUsersConnections(this.id).subscribe(res => this.connections = res);
      }

      )
    //console.log(this.id);
  }



}
