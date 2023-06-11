import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
  complaint: Complaint;
  selectedOption: any;
  users!: User[];
  clinic!: ClinicLoadParameters;
  clinics!: DbClinic[];
  id: any;

  constructor(private clinicService: ClinicService, private userService: UserService, private route: ActivatedRoute) {
    this.complaint = new Complaint({
      userInput: '',
      status: true,
      type: '',
      clinicId: ''
    })
  }

  onChange(event: any) {
    const selectedId = event.target.value;
    if (this.selectedOption === 'clinic') {
      this.complaint.clinicId = selectedId; // Assign the selected clinic ID
      console.log(this.complaint.clinicId);
    } else if (this.selectedOption === 'employee') {
      this.complaint.employeeId = selectedId; // Assign the selected employee ID
    }
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
    this.clinicService.getAllClinics(this.clinic).subscribe(res => {
      this.clinics = res;
    })
  }


  saveComplaint() {
    this.id = this.route.snapshot.params['id'];


    this.clinicService.saveComplaint(this.complaint).subscribe(res => {
      this.complaint = res;
      this.complaint.patientId = this.id;
      console.log(this.complaint);
    });
    this.selectedOption = '';
  }

}
