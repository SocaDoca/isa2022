//proveriti da li treba da se dodaju liste



export interface WorkingHoursInterface {
  id?: string;
  start?: any;
  end?: any;
  dayOfWeek?: any;


}

export class WorkingHours implements WorkingHoursInterface {
  id?: string;
  start?: any;
  end?: any;
  dayOfWeek?: any;


  constructor(obj: WorkingHoursInterface) {
    this.id = obj.id;
    this.start = obj.start;
    this.end = obj.end;
    this.dayOfWeek = obj.dayOfWeek;

  }
}
