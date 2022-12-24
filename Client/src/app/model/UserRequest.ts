import { Role } from './Role';
import { Genders } from './Genders';

export interface UserRequestInterface {
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

export class UserRequest implements UserRequestInterface {
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

  constructor(obj: UserRequestInterface) {
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
