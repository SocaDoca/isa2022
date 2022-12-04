//proveriti da li treba da se dodaju liste

export interface DbClinicInterface {
  id: number;
  name?: string;
  address?: string;
  description?: string;
  rating?: number;
  capacity: number;
  workingFrom?: Date;
  workingTo?: Date;
  isDeleted: boolean;
}

export class DbClinic implements DbClinicInterface {
  id: number;
  name?: string;
  address?: string;
  description?: string;
  rating?: number;
  capacity: number;
  workingFrom?: Date;
  workingTo?: Date;
  isDeleted: boolean;

  constructor(obj: DbClinicInterface) {
    this.id = obj.id;
    this.name = obj.name;
    this.address = obj.address;
    this.description = obj.description;
    this.rating = obj.rating;
    this.capacity = obj.capacity;
    this.workingFrom = obj.workingFrom;
    this.workingTo = obj.workingTo;
    this.isDeleted = obj.isDeleted;
  }
}
