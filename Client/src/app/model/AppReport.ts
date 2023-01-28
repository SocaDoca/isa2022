import { ClinicSaveModel } from "./ClinicSaveModel";

export interface AppReportInterface {
  id?: string;
  equipment?: string;
  description?: string;
  isDeleted?: boolean;

}

export class AppReport implements AppReportInterface {
  id?: string;
  equipment?: string;
  description?: string;
  isDeleted?: boolean;


  constructor(obj: AppReportInterface) {
    this.id = obj.id;
    this.equipment = obj.equipment;
    this.description = obj.description;
    this.isDeleted = obj.isDeleted;


  }
}
