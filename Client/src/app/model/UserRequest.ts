import { Roles } from './Roles';
import { Genders } from './Genders';

export interface UserRequestInterface {
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
  gender?: any;
  password?: string;
  fullAddress?: string;
  confirmPassword?: string;
  jmbg?: string; 
  moblie?: string;
  isAdminCenter?: any;
  name?: string;
}

export class UserRequest implements UserRequestInterface {
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
  gender?: any;
  password?: string;
  confirmPassword?: string;
  jmbg?: string;
  moblie?: string;
  fullAddress?: string;
  isAdminCenter?: any;
  name?: string;

  constructor(obj: UserRequestInterface) {
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
    this.moblie = obj.moblie;
    this.fullAddress = obj.fullAddress;
    this.isAdminCenter = obj.isAdminCenter;
    this.name = obj.name;
  }
}
