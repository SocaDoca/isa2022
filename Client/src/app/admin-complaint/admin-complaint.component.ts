import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ClinicService } from '../../service/clinic.service';
import { Complaint } from '../model/Complaint';

@Component({
  selector: 'app-admin-complaint',
  templateUrl: './admin-complaint.component.html',
  styleUrls: ['./admin-complaint.component.css']
})
export class AdminComplaintComponent {
  complaint: any;
  @ViewChild('mymodal')
  public mymodal: ModalDirective | undefined;
  isModalOpen = false;
  id: any;
  selectedComplaintId: any;

  openModal(complaintId: any) {
    console.log(complaintId);
    this.id = complaintId;
    this.modalService.open(this.mymodal).result.then((result) => {

    });
  }

  constructor(private clinicService: ClinicService, private route: ActivatedRoute, private modalService: NgbModal) {
    this.complaint = new Complaint({
      description: '',
      status: true,
      type: ''
    })
  }

  respond() {
    this.clinicService.answerComplaint(this.id, this.complaint.answer).subscribe(res => {
      if (res === true) {
        this.modalService.dismissAll();
        window.location.reload();
      }
     
    });
  }


  ngOnInit() {
    this.clinicService.getAllComplaints(this.complaint).subscribe(res => {
      this.complaint = res;
      console.log(this.complaint);
    });
  }
}
