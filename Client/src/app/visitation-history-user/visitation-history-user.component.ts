import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { UserService } from '../../service/user.service';
import { User } from '../model/User';

@Component({
  selector: 'app-visitation-history-user',
  templateUrl: './visitation-history-user.component.html',
  styleUrls: ['./visitation-history-user.component.css']
})
export class VisitationHistoryUserComponent {
  id: any;
  user!: User;
  constructor(private userService: UserService, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.loadHistory();
  }

  loadHistory() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.userService.getUser(this.id).subscribe(res => {
      this.user = res;
      console.log(this.user);
    });
  }
}
