import { Role } from './Role';
import { Genders } from './Genders';

export interface UserInterface {
  id?: number;
  firstName?: string;
  lastName: string;
  username?: string;
  email?: string;
  address?: string;
  country?: string;
  city?: string;
  job?: string;
  role?: string;
  gender?: Genders;
  password: string;

}

export class User implements UserInterface {
  id?: number;
  firstName?: string;
  lastName: string;
  username?: string;
  email?: string;
  address?: string;
  country?: string;
  city?: string;
  job?: string;
  role?: string;
  gender?: Genders;
  password: string;

  constructor(obj: UserInterface) {
    this.id = obj.id;
    this.firstName = obj.firstName;
    this.lastName = obj.lastName;
    this.username = obj.username;
    this.email = obj.email;
    this.address = obj.address;
    this.country = obj.country;
    this.city = obj.city;
    this.role = obj.role;
    this.gender = obj.gender;
   this.job = obj.job;
    this.password = obj.password;

  }
}
