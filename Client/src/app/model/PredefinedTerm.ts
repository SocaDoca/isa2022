import { ClinicSaveModel } from "./ClinicSaveModel";

export interface PredefinedTermInterface {
  id?: string;
  time?: string;
  date?: any;
  duration?: number;
  numberOfAppointmentsInDay?: number;
  clinic_RefID?: string;
  startDate?: any;
}

export class PredefinedTerm implements PredefinedTermInterface {
  id?: string;
  time?: string;
  date?: any;
  duration?: number;
  numberOfAppointmentsInDay?: number;
  clinic_RefID?: string;
  startDate?: any;

  constructor(obj: PredefinedTermInterface) {
    this.id = obj.id;
    this.time = obj.time;
    this.date = obj.date;
    this.duration = obj.duration;
    this.numberOfAppointmentsInDay = obj.numberOfAppointmentsInDay;
    this.clinic_RefID = obj.clinic_RefID;
    this.startDate = obj.startDate;
  }
}
