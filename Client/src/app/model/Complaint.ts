import { ClinicSaveModel } from "./ClinicSaveModel";

export interface ComplaintInterface {
  id?: string;
  description?: string;
  status?: boolean;
  type?: string;

}

export class Complaint implements ComplaintInterface {
  id?: string;
  description?: string;
  status?: boolean;
  type?: string;


  constructor(obj: ComplaintInterface) {
    this.id = obj.id;
    this.status = obj.status;
    this.description = obj.description;
    this.type = obj.type;


  }
}
