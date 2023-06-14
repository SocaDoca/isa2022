import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ClinicService } from '../../service/clinic.service';
import { UserService } from '../../service/user.service';
import { AppointmentHistory } from '../model/AppointmentHistory';
import { ClinicRating } from '../model/ClinicRating';
import { User } from '../model/User';

@Component({
  selector: 'app-visitation-history-user',
  templateUrl: './visitation-history-user.component.html',
  styleUrls: ['./visitation-history-user.component.css']
})
export class VisitationHistoryUserComponent {
  id: any;
  user!: User;
  @ViewChild('mymodal')
  public mymodal: ModalDirective | undefined;
  isModalOpen = false;
  showError!: true;
  errorMessage!: any;
  rating!: any;
  patientId!: any;
  ratingData!: ClinicRating;

  constructor(private userService: UserService, private route: ActivatedRoute, private modalService: NgbModal, private clinicService: ClinicService) {

  }

  ngOnInit() {
    this.loadHistory();
  }

  openModal(clinicId: any) {
    console.log(clinicId);
    this.id = clinicId;
    this.modalService.open(this.mymodal).result.then((result) => {

    });
  }

  reserve() {
    this.patientId = this.route.snapshot.paramMap.get('id');
    const ratingData: ClinicRating = {
      clinicId: this.id,
      patientId: this.patientId,
      rating: parseFloat(this.rating)
    };

    this.clinicService.updateClinicRating(ratingData).subscribe(res => {
      // Handle the response from the service
    });
  }

  loadHistory() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.userService.getUser(this.id).subscribe(res => {
      this.user = res;
      console.log(this.user);

      // Format appointment time for each appointment history
      this.user.appointmentHistories!.forEach((history: AppointmentHistory) => {
        history.timeFinished = this.formatDateTime(history.timeFinished!);
      });
    });
  }

  formatDateTime(dateTimeString: string): string {
    const dateTime = new Date(dateTimeString); // Convert the dateTimeString to a Date object
    const year = dateTime.getFullYear();
    const month = (dateTime.getMonth() + 1).toString().padStart(2, '0');
    const day = dateTime.getDate().toString().padStart(2, '0');
    const hours = dateTime.getHours().toString().padStart(2, '0');
    const minutes = dateTime.getMinutes().toString().padStart(2, '0');
    const formattedDateTime = `${year}-${month}-${day} ${hours}:${minutes}`;
    return formattedDateTime;
  }
}
