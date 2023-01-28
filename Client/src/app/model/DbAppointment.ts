import { AppReport } from "./AppReport";

export interface DbAppointmentInterface {
  id?: string;
  time?: string;
  date?: Date;
  duration?: number;
  numberOfAppointmentsInDay?: number;
  clinic_RefID?: string;
  title?: string;
  startDate?: any;
  startTime?: any;
  patient_RefId?: string;
  isCanceled?: boolean;
  isFinished?: boolean;
  report?: AppReport;
}

export class DbAppointment implements DbAppointmentInterface {
  id?: string;
  time?: string;
  date?: Date;
  duration?: number;
  numberOfAppointmentsInDay?: number;
  clinic_RefID?: string;
  title?: string;
  startDate?: any;
  startTime?: any;
  patient_RefId?: string;
  isCanceled?: boolean;
  isFinished?: boolean;
  report?: AppReport;

  constructor(obj: DbAppointmentInterface) {
    this.id = obj.id;
    this.time = obj.time;
    this.date = obj.date;
    this.title = obj.title;
    this.startDate = obj.startDate;
    this.startTime = obj.startTime;
    this.patient_RefId = obj.patient_RefId;
    this.isCanceled = obj.isCanceled;
    this.isFinished = obj.isFinished;
    this.report = obj.report;
    this.numberOfAppointmentsInDay = obj.numberOfAppointmentsInDay;
    this.clinic_RefID = obj.clinic_RefID;
  }
}
