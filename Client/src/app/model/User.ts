import { Roles } from './Roles';
import { Genders } from './Genders';
import { AppointmentHistory } from './AppointmentHistory';

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
  gender?: any;
  password?: string;
  confirmPassword?: string;
  jmbg?: string;
  moblie?: string;
  mobile?: string;
  fullAddress?: string;
  isAdminCenter?: any;
  name?: string;
  isFirstTime?: boolean;
  penalty?: number;
  appointmentHistories?: AppointmentHistory[];
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
  gender?: any;
  password?: string;
  confirmPassword?: string;
  jmbg?: string;
  moblie?: string;
  fullAddress?: string;
  isAdminCenter?: any;
  name?: string;
  mobile?: string;
  isFirstTime?: boolean;
  penalty?: number;
  appointmentHistories?: AppointmentHistory[];

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
    this.moblie = obj.moblie;
    this.mobile = obj.mobile;
    this.fullAddress = obj.fullAddress;
    this.isAdminCenter = obj.isAdminCenter;
    this.name = obj.name;
    this.isFirstTime = obj.isFirstTime;
    this.penalty = obj.penalty;
    this.appointmentHistories = obj.appointmentHistories;
  }
}
