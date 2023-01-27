//proveriti da li treba da se dodaju liste
import { WorkingHours } from './WorkingHours';

export interface DbClinicInterface {
  id?: string;
  name?: string;
  address?: string;
  city?: string;
  country?: string;
  phone?: string;
  description?: string;
  rating?: number;
  workingHours?: WorkingHours[];
}

export class DbClinic implements DbClinicInterface {
  id?: string;
  name?: string;
  address?: string;
  city?: string;
  country?: string;
  phone?: string;
  description?: string;
  rating?: number;
  workingHours?: WorkingHours[];

  constructor(obj: DbClinicInterface) {
    this.id = obj.id;
    this.name = obj.name;
    this.address = obj.address;
    this.city = obj.city;
    this.country = obj.country;
    this.phone = obj.phone;
    this.description = obj.description;
    this.rating = obj.rating;
    this.workingHours = obj.workingHours;
  }
}
