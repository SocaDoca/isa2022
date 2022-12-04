export interface DbPersonInfoInterface {
  id: number;
  firstName: string;
  lastName: string;
  birthday: Date;
  jmbg: string;
  address: string;
  isDeleted: boolean;
  createdAt: Date;
}

export class DbPersonInfo implements DbPersonInfoInterface {
  id: number;
  firstName: string;
  lastName: string;
  birthday: Date;
  jmbg: string;
  address: string;
  isDeleted: boolean;
  createdAt: Date;

  constructor(obj: DbPersonInfoInterface) {
    this.id = obj.id;
    this.firstName = obj.firstName;
    this.lastName = obj.lastName;
    this.birthday = obj.birthday;
    this.jmbg = obj.jmbg;
    this.address = obj.address;
    this.isDeleted = obj.isDeleted;
    this.createdAt = obj.createdAt;
  }
}
