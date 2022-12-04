export interface DbAppointmentInterface {
  id: number;
  startDate: Date; //koja je razlika ako su oba tipa DateTime na back-u
  start: Date;
  duration: number;
  isReserved: boolean;
  isDeleted: boolean;
  createdAt: Date;
}

export class DbAppointment implements DbAppointmentInterface {
  id: number;
  startDate: Date; 
  start: Date;
  duration: number;
  isReserved: boolean;
  isDeleted: boolean;
  createdAt: Date;

  constructor(obj: DbAppointmentInterface) {
    this.id = obj.id;
    this.startDate = obj.startDate;
    this.start = obj.start;
    this.duration = obj.duration;
    this.isReserved = obj.isReserved;
    this.isDeleted = obj.isDeleted;
    this.createdAt = obj.createdAt;
  }
}
