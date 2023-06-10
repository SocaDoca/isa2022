import { Component } from '@angular/core';
import { ClinicService } from '../../service/clinic.service';
import { UserService } from '../../service/user.service';
import { ClinicLoadParameters } from '../model/ClinicLoadParameters';
import { Complaint } from '../model/Complaint';
import { DbClinic } from '../model/DbClinic';
import { User } from '../model/User';

@Component({
  selector: 'app-user-complaint',
  templateUrl: './user-complaint.component.html',
  styleUrls: ['./user-complaint.component.css']
})
export class UserComplaintComponent {
  complaint: any;
  selectedOption: any;
  users!: User[];
  clinic!: ClinicLoadParameters;
  clinics!: DbClinic[];

  constructor(private clinicService: ClinicService, private userService: UserService) {
    this.complaint = new Complaint({
      description: '',
      status: true,
      type: ''
    })
  }

  ngOnInit() {
    this.loadAllUsers();
    this.loadAllClinics();
  }

  loadAllUsers() {
    this.userService.getEmployees().subscribe(res => {
      this.users = res;
    });
  }

  loadAllClinics() {
    this.clinicService.getAll(this.clinic).subscribe(res => {
      this.clinics = res;
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

