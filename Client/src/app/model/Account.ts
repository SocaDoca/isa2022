import { LoyaltyLevel } from "./LoyaltyLevel";
import { Role } from "./Role";


export interface AccountInterface {
  id: number;
  username: string;
  password: string;
  email: string;
  isActive: boolean;
  createdAt: Date;
  isDeleted: boolean;
  loyaltyLevel: LoyaltyLevel;
  role: Role
}

export class Account implements AccountInterface {
  id: number;
  username: string;
  password: string;
  email: string;
  isActive: boolean;
  createdAt: Date;
  isDeleted: boolean;
  loyaltyLevel: LoyaltyLevel;
  role: Role

  constructor(obj: AccountInterface) {
    this.id = obj.id;
    this.username = obj.username;
    this.password = obj.password;
    this.email = obj.email;
    this.isActive = obj.isActive;
    this.createdAt = obj.createdAt;
    this.isDeleted = obj.isDeleted;
    this.loyaltyLevel = obj.loyaltyLevel;
    this.role = obj.role;
  }
}
