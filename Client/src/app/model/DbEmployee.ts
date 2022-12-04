export interface DbEmployeeInterface {
  id: number;
  isAdminCenter: boolean;
  //jos jedan kljuc
}

export class DbEmployee implements DbEmployeeInterface {
  id: number;
  isAdminCenter: boolean;

  constructor(obj: DbEmployeeInterface) {
    this.id = obj.id;
    this.isAdminCenter = obj.isAdminCenter;
  }
}
