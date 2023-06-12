import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ClinicService } from '../../service/clinic.service';
import { AppReport } from '../model/AppReport';
import { ClinicLoadParameters } from '../model/ClinicLoadParameters';
import { ClinicSaveModel } from '../model/ClinicSaveModel';
import { DbAppointment } from '../model/DbAppointment';
import { DbClinic } from '../model/DbClinic';

@Component({
  selector: 'app-available-appointments',
  templateUrl: './available-appointments.component.html',
  styleUrls: ['./available-appointments.component.css']
})
export class AvailableAppointmentsComponent {

  id: any;
  appointment: DbAppointment;
  appointments: DbAppointment[] = [];
  address: any;
  timeList: string = '';
  @ViewChild('mymodal')
  public mymodal: ModalDirective | undefined;
  isModalOpen = false;
  showError!: true;
  errorMessage!:any;

  openModal(appoitnmentId: any) {
    console.log(appoitnmentId);
    this.id = appoitnmentId;
    this.modalService.open(this.mymodal).result.then((result) => {

    });
  }

  reserve() {
    this.appointment.patientId = this.route.snapshot.params['id'];
    this.clinicService.reserveAppointment(this.id, this.appointment.patientId).subscribe(res => {
      if (res === true) {
        this.modalService.dismissAll();
        window.location.reload();
      }
      else {
          this.errorMessage = 'An error occurred. Please try again.';
        
      }
    });}

  constructor(private modalService: NgbModal,private clinicService: ClinicService, private route: ActivatedRoute) {
    this.appointment = new DbAppointment({

      title: 'Blood appointment',
      startDate: new Date,
      startTime: '',
      patient_RefId: '',
      clinic_RefID: '',
      isCanceled: false,
      isFinished: false,
      report: new AppReport({
        description: '0',
        equipment: '0',
        isDeleted:false
      })
    })
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['clinicId'];
    console.log(this.id);

    this.clinicService.getAllTermsByClinicId(this.id).subscribe(res => {
      console.log(res);

      this.appointments = res.filter(appointment => !appointment.isReserved);

      const regex = /T/g;
      this.appointments.forEach(appointment => {
        appointment.startDate = appointment.startDate.replace(regex, ' ');
      });

      console.log(this.appointments);
    });
  }




  loadTime(appointment: DbAppointment) {
    this.timeList = this.appointment.startDate;
    var splitted = this.timeList.split(" ");
    var splittedDate = splitted[0];
    var splittedTime = splitted[1];

    console.log(splittedDate + splittedTime);
    this.appointment.startDate = splittedDate;
    this.appointment.startTime = splittedTime;
    //this.user.city = splittedCity;
    //this.user.country = splittedCountry;
  }

  onSubmit() {
    /*this.clinicService.getTermById(this.id).subscribe(res => {
      this.appointment = res;
    })*/
    /*this.clinicService.saveAppointment(this.appointment).subscribe(res => {
      this.appointment = res;
    })*/
  }
}
