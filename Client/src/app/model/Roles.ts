export interface RolesInterface {
  id?: number;
  name: string;
  isDeleted?: boolean;
}

export class Roles implements RolesInterface {
  id?: number;
  name: string;
  isDeleted?: boolean;

  constructor(obj: RolesInterface) {
    this.id = obj.id;
    this.name = obj.name;
    this.isDeleted = obj.isDeleted;
  }
}
