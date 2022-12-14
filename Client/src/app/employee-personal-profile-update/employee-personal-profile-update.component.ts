import { Component, ContentChild} from '@angular/core';


@Component({
  selector: 'app-employee-personal-profile-update',
  templateUrl: './employee-personal-profile-update.component.html',
  styleUrls: ['./employee-personal-profile-update.component.css']
})
export class EmployeePersonalProfileUpdateComponent {
show: boolean = false;

constructor() {
}

// click event function toggle
password() {
    this.show = !this.show;
}

}
