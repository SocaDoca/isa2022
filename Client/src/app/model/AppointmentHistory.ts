export interface AppointmentHistoryInterface {
  id?: string;
  appointmentId?: string;
  clinicId?: string;
  clinicName?: string;
  reportDescription?: string;
  timeFinished?: string;
  rating?: number;
  workingHours?: string;
}

export class AppointmentHistory implements AppointmentHistoryInterface {
  id?: string;
  appointmentId?: string;
  clinicId?: string;
  clinicName?: string;
  reportDescription?: string;
  timeFinished?: string;
  rating?: number;
  workingHours?: string;

  constructor(obj: AppointmentHistoryInterface) {
    this.id = obj.id;
    this.appointmentId = obj.appointmentId;
    this.clinicId = obj.clinicId;
    this.clinicName = obj.clinicName;
    this.reportDescription = obj.reportDescription;
    this.timeFinished = obj.timeFinished;
    this.rating = obj.rating;
    this.workingHours = obj.workingHours;
  }
}
