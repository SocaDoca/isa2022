import { AppReport } from "./AppReport";
import { ClinicLoadParameters } from "./ClinicLoadParameters";
import { ClinicSaveModel } from "./ClinicSaveModel";
import { User } from "./User";

export interface DbAppointmentInterface {
  id?: string;
  time?: string;
  date?: Date;
  duration?: number;
  numberOfAppointmentsInDay?: number;
  clinic_RefID?: string;
  clinic?: ClinicSaveModel;
  title?: string;
  startDate?: any;
  startTime?: any;
  patient_RefId?: string;
  isCanceled?: boolean;
  isFinished?: boolean;
  isReserved?: boolean;
  report?: AppReport;
  patient?: User;
  worksFrom?: string;
  worksTo?: string;
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
  isReserved?: boolean;
  clinic?: ClinicSaveModel;
  report?: AppReport;
  patient?: User;
  worksFrom?: string;
  worksTo?: string;

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
    this.clinic = obj.clinic;
    this.patient = obj.patient;
    this.worksFrom = obj.worksFrom;
    this.worksTo = obj.worksTo;
    this.isReserved = obj.isReserved;
  }
}
