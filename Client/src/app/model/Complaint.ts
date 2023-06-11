import { ClinicSaveModel } from "./ClinicSaveModel";

export interface ComplaintInterface {
  id?: string;
  description?: string;
  status?: boolean;
  type?: string;
  isForClinic?: boolean;
  isForEmployee?: boolean;
  patientId?: any;
  clinicId?: any;
  userInput?: any;
  answer?: any;
  employeeId?: any;
}

export class Complaint implements ComplaintInterface {
  id?: string;
  description?: string;
  status?: boolean;
  type?: string;
  isForClinic?: boolean;
  isForEmployee?: boolean;
  patientId?: any;
  clinicId?: any;
  userInput?: any;
  answer?: any;
  employeeId?: any;


  constructor(obj: ComplaintInterface) {
    this.id = obj.id;
    this.status = obj.status;
    this.description = obj.description;
    this.type = obj.type;
    this.isForClinic = obj.isForClinic;
    this.isForEmployee = obj.isForEmployee;
    this.answer = obj.answer;
    this.userInput = obj.userInput;
    this.clinicId = obj.clinicId;
    this.patientId = obj.patientId;
    this.employeeId = obj.employeeId;
  }
}
