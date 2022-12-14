import { Roles } from './Roles';
import { Genders } from './Genders';

export interface UserInterface {
  id?: string;
  firstName?: string;
  lastName?: string;
  username?: string;
  email?: string;
  address?: string;
  country?: string;
  city?: string;
  job?: string;
  roles?: string;
  gender?: Genders;
  password?: string;
  confirmPassword?: string;
  jmbg?: string;
  mobile?: string;
  fullAddress?: string;

}

export class User implements UserInterface {
  id?: string;
  firstName?: string;
  lastName?: string;
  username?: string;
  email?: string;
  address?: string;
  country?: string;
  city?: string;
  job?: string;
  roles?: string;
  gender?: Genders;
  password?: string;
  confirmPassword?: string;
  jmbg?: string;
  mobile?: string;
  fullAddress?: string;

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
    this.jmbg = obj.jmbg;
    this.mobile = obj.mobile;
    this.fullAddress = obj.fullAddress;

  }
}
