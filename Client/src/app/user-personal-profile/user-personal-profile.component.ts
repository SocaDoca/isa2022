import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from '../../service/user.service';
import { UserLoadModel } from '../../app/model/UserLoadModel';
import { Genders } from '../model/Genders';


@Component({
  selector: 'app-user-personal-profile',
  templateUrl: './user-personal-profile.component.html',
  styleUrls: ['./user-personal-profile.component.css']
})
export class UserPersonalProfileComponent implements OnInit, AfterViewInit{
  title = 'appBootstrap';
  @ViewChild('mymodal') mymodal: ElementRef | undefined;
  closeResult = '';
  id!: string;
  user: UserLoadModel;

  constructor(private modalService: NgbModal, private userService: UserService, private route: ActivatedRoute) {
    this.user = new UserLoadModel({
      role: '',
      username: '',
      name: '',
      fullAddress: '',
      mobile: '',
      job: '',
      gender: 0,
      jmbg:''

    })
  }
  ngAfterViewInit(): void {
    this.open(this.mymodal);
  }
  ngOnInit(): void {
    this.loadProfile();
  }

  loadProfile() {
    this.id = this.route.snapshot.params['id'];
    this.userService.getUser(this.id)
      .subscribe(res => {
        this.user = res;

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

}
