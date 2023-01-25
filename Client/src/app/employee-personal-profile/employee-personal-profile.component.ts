import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from '@angular/core';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../service/user.service';
import { UserLoadModel } from '../../app/model/UserLoadModel';
import { Genders } from '../model/Genders';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import Validation from '../utils/validation';

@Component({
  selector: 'app-employee-personal-profile',
  templateUrl: './employee-personal-profile.component.html',
  styleUrls: ['./employee-personal-profile.component.css']
})
export class EmployeePersonalProfileComponent implements OnInit, AfterViewInit {
  title = 'appBootstrap';
  @ViewChild('mymodal')
  public mymodal: ModalDirective | undefined;
  closeResult = '';
  id: any;
  isFirstTime: any;
  password: any;
  user: UserLoadModel;
  confirmPassword:string = '';

  form: FormGroup = new FormGroup({
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
  });

  submitted: boolean = false;
  show: boolean = false;

  constructor(private modalService: NgbModal, private userService: UserService, private route: ActivatedRoute, private formBuilder: FormBuilder) {
    this.user = new UserLoadModel({
      role: '',
      username: '',
      name: '',
      fullAddress: '',
      mobile: '',
      job: '',
      gender: undefined,
      jmbg: ''

    })
  }
  ngAfterViewInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isFirstTime = this.route.snapshot.params['isFirstTime'];
    this.userService.getUser(this.id)
      .subscribe(res => {
        this.user = res;
        this.showGender(res);
        console.log(res.isFirstTime);
        //console.log(res);
        if (res.isFirstTime == false) {
          //this.mymodal!.hide();
          this.modalService.dismissAll(this.mymodal);
        }
        else {
          this.modalService.open(this.mymodal).result.then((result) => {
            this.user.isFirstTime = false;
            console.log(this.user.isFirstTime);
            this.form = this.formBuilder.group(
              {
                password: [
                  '',
                  [
                    Validators.required,
                  ]
                ],
                confirmPassword: ['', Validators.required]
              },
              {
                validators: [Validation.match('password', 'confirmPassword')]
              }
            );
            this.changePass();

          });
        }
      });


  }

  changePass() {
    this.id = sessionStorage.getItem('id');
    this.userService.updatePassword(this.id, this.password).subscribe(
      (data: any) => {

        console.log(data)
      });
    this.user.isFirstTime = false;
  }
  ngOnInit(): void {



  }

  showGender(user: UserLoadModel) {
    if (user.gender == 0) {
      user.gender = 'Male';
    } else if (user.gender == 1) {
      user.gender = 'Female';
    } else
      user.gender = 'Other';
  }

  loadProfile() {
    this.id = this.route.snapshot.params['id'];
    this.isFirstTime = this.route.snapshot.params['isFirstTime'];
    this.userService.getUser(this.id)
      .subscribe(res => {
        this.user = res;
        this.showGender(res);
        console.log(res.isFirstTime);
        console.log(res);
      })
  }


  open(content: any) {
    this.modalService
      .open(content, { ariaLabelledBy: 'modal-basic-title' })
      .result.then(
        (result) => {
          this.closeResult = `Closed with: ${result}`;
        },
        (reason) => {
          this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
        }
      );
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }


  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }


  // click event function toggle
  pass() {
    this.show = !this.show;
  }
}
