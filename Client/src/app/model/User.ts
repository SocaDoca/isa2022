import { Roles } from './Roles';
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
  roles?: string;
  gender?: Genders;
  password: string;
  confirmPassword: string;

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
  roles?: string;
  gender?: Genders;
  password: string;
  confirmPassword: string;

  constructor(obj: UserInterface) {
    this.id = obj.id;
    this.firstName = obj.firstName;
    this.lastName = obj.lastName;
    this.username = obj.username;
    this.email = obj.email;
    this.address = obj.address;
    this.country = obj.country;
    this.city = obj.city;
    this.roles = obj.roles;
    this.gender = obj.gender;
   this.job = obj.job;
    this.password = obj.password;
    this.confirmPassword = obj.confirmPassword;

  }
}
