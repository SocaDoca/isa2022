import { Component } from '@angular/core';
import { ClinicService } from '../../service/clinic.service';
import { Complaint } from '../model/Complaint';

@Component({
  selector: 'app-user-complaint',
  templateUrl: './user-complaint.component.html',
  styleUrls: ['./user-complaint.component.css']
})
export class UserComplaintComponent {
  complaint: any;
  selectedOption: any;

  constructor(private clinicService: ClinicService) {
    this.complaint = new Complaint({
      description: '',
      status: true,
      type: ''
    })
  }


  saveComplaint() {
    this.clinicService.saveComplaint(this.complaint).subscribe(res => {
      this.complaint = res;
      console.log(this.complaint);
    });
    this.complaint = new Complaint({
      description: '',
      status: true,
      type: ''
    });
    this.selectedOption = '';
  }
 }

