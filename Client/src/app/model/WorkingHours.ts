//proveriti da li treba da se dodaju liste



export interface WorkingHoursInterface {
  id?: string;
  start?: string;
  end?: string;
  day?: string;


}

export class WorkingHours implements WorkingHoursInterface {
  id?: string;
  start?: string;
  end?: string;
  day?: string;


  constructor(obj: WorkingHoursInterface) {
    this.id = obj.id;
    this.start = obj.start;
    this.end = obj.end;
    this.day = obj.day;

  }
}
