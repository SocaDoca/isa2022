import { ClinicSaveModel } from "./ClinicSaveModel";

export interface PredefinedTermInterface {
  id?: string;
  time?: string;
  date?: Date;
  duration?: number;
  numberOfAppointmentsInDay?: number;
  clinic_RefID?: string;
}

export class PredefinedTerm implements PredefinedTermInterface {
  id?: string;
  time?: string;
  date?: Date;
  duration?: number;
  numberOfAppointmentsInDay?: number;
  clinic_RefID?: string;

  constructor(obj: PredefinedTermInterface) {
    this.id = obj.id;
    this.time = obj.time;
    this.date = obj.date;
    this.duration = obj.duration;
    this.numberOfAppointmentsInDay = obj.numberOfAppointmentsInDay;
    this.clinic_RefID = obj.clinic_RefID;

  }
}
