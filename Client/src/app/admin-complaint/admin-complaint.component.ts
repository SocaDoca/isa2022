import { Component } from '@angular/core';
import { ClinicService } from '../../service/clinic.service';
import { Complaint } from '../model/Complaint';

@Component({
  selector: 'app-admin-complaint',
  templateUrl: './admin-complaint.component.html',
  styleUrls: ['./admin-complaint.component.css']
})
export class AdminComplaintComponent {
  complaint: any;

  constructor(private clinicService: ClinicService) {
    this.complaint = new Complaint({
      description: '',
      status: true,
      type: ''
    })
  }


  ngOnInit() {
    this.clinicService.getAllComplaints(this.complaint).subscribe(res => {
      this.complaint = res;
      console.log(this.complaint);
    });
  }
}
