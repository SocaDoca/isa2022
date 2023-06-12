import { ClinicSaveModel } from "./ClinicSaveModel";
import { WorkItem } from "./WorkItem";

export interface AppReportInterface {
  id?: string;
  description?: string;
  equipment?: WorkItem[];
  isDeleted?: boolean;
  appointmentId?: any;
}


export class AppReport implements AppReportInterface {
  id?: string;
  description?: string;
  equipment?: WorkItem[];
  isDeleted?: boolean;
  appointmentId?: any;

  constructor(obj: AppReportInterface) {
    this.id = obj.id;
    this.equipment = obj.equipment;
    this.description = obj.description;
    this.isDeleted = obj.isDeleted;
    this.appointmentId = obj.appointmentId;

  }
}
