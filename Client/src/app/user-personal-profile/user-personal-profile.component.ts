import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from '@angular/core';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-user-personal-profile',
  templateUrl: './user-personal-profile.component.html',
  styleUrls: ['./user-personal-profile.component.css']
})
export class UserPersonalProfileComponent implements OnInit, AfterViewInit{
  title = 'appBootstrap';
  @ViewChild('mymodal') mymodal: ElementRef | undefined;
  closeResult = '';

constructor(private modalService: NgbModal) {}
  ngAfterViewInit(): void {
    this.open(this.mymodal);
  }
  ngOnInit(): void {}

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