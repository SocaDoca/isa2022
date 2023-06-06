import { ClinicSaveModel } from "./ClinicSaveModel";

export interface ComplaintInterface {
  id?: string;
  description?: string;
  status?: boolean;
  type?: string;
  isForClinic?: boolean;
  isForEmployee?: boolean;
  isForClinic_Clinic_RefId?: any;
  isForEmployee_Employee_RefId?: any;
}

export class Complaint implements ComplaintInterface {
  id?: string;
  description?: string;
  status?: boolean;
  type?: string;
  isForClinic?: boolean;
  isForEmployee?: boolean;
  isForClinic_Clinic_RefId?: any;
  isForEmployee_Employee_RefId?: any;


  constructor(obj: ComplaintInterface) {
    this.id = obj.id;
    this.status = obj.status;
    this.description = obj.description;
    this.type = obj.type;
    this.isForClinic = obj.isForClinic;
    this.isForEmployee = obj.isForEmployee;
    this.isForClinic_Clinic_RefId = obj.isForClinic_Clinic_RefId;
    this.isForEmployee_Employee_RefId = obj.isForEmployee_Employee_RefId;

  }
}
