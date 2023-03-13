import { Component } from '@angular/core';
import { ClinicService } from '../../service/clinic.service';
import { Complaint } from '../model/Complaint';

@Component({
  selector: 'app-complaint-history',
  templateUrl: './complaint-history.component.html',
  styleUrls: ['./complaint-history.component.css']
})
export class ComplaintHistoryComponent {
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
