import { Component } from '@angular/core';
import { UserService } from '../../service/user.service';
import { FormControl, FormGroup, FormBuilder, Validators, AbstractControl, NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-pass',
  templateUrl: './change-pass.component.html',
  styleUrls: ['./change-pass.component.css']
})
export class ChangePassComponent {
  id: any;
  password: string = '';
  constructor(private userService: UserService, private router:Router) { }



  changePass() {
    this.id = sessionStorage.getItem('id');
    this.userService.updatePassword(this.id, this.password).subscribe(
      (data: any) => {

      console.log(data)
      this.router.navigate(['sign-in'], data);
    });

  }
}
