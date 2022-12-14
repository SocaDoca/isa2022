import { Component } from '@angular/core';

@Component({
  selector: 'app-user-personal-profile-update',
  templateUrl: './user-personal-profile-update.component.html',
  styleUrls: ['./user-personal-profile-update.component.css']
})
export class UserPersonalProfileUpdateComponent {
show: boolean = false;

constructor() {
}

// click event function toggle
password() {
    this.show = !this.show;
}
}
