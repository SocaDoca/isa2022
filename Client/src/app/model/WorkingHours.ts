//proveriti da li treba da se dodaju liste



export interface WorkingHoursInterface {
  id?: string;
  worksFrom?: string;
  worksTo?: string;
  day?: string;


}

export class WorkingHours implements WorkingHoursInterface {
  id?: string;
  worksFrom?: string;
  worksTo?: string;
  day?: string;


  constructor(obj: WorkingHoursInterface) {
    this.id = obj.id;
    this.worksFrom = obj.worksFrom;
    this.worksTo = obj.worksTo;
    this.day = obj.day;

  }
}
