import { Roles } from './Roles';
import { Genders } from './Genders';

export interface UserLoadModelInterface {
  id?: string;
  name?: string;
  username?: string;
  email?: string;
  address?: string;
  country?: string;
  city?: string;
  job?: string;
  jmbg?: string;
  role?: string;
  gender?: any;
  fullAddress?: string;
  mobile?: string;
  password?: string;
  isFirstTime?: boolean;


}

export class UserLoadModel implements UserLoadModelInterface {
  id?: string;
  name?: string;
  username?: string;
  email?: string;
  address?: string;
  country?: string;
  city?: string;
  job?: string;
  jmbg?: string;
  role?: string;
  gender?: any;
  fullAddress?: string;
  mobile?: string;
  password?: string;
  isFirstTime?: boolean;


  constructor(obj: UserLoadModelInterface) {
    this.id = obj.id;
    this.name = obj.name;
    this.username = obj.username;
    this.email = obj.email;
    this.address = obj.address;
    this.country = obj.country;
    this.city = obj.city;
    this.jmbg = obj.jmbg;
    this.role = obj.role;
    this.gender = obj.gender;
    this.job = obj.job;
    this.fullAddress = obj.fullAddress;
    this.mobile = obj.mobile;
    this.password = obj.password;
    this.isFirstTime = obj.isFirstTime;

  }
}
