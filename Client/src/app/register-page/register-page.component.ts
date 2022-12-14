import { Component } from '@angular/core';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent {
show: boolean = false;

constructor() {
}

// click event function toggle
password() {
    this.show = !this.show;
}
}
