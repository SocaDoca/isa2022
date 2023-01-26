export interface LoadAllUsersParametersInterface {
  searchCriteria?: string;
  offset?: number; 
  limit?: number;
  sortBy?: string;
  orderAsc?: boolean;

}

export class LoadAllUsersParameters implements LoadAllUsersParametersInterface {
  searchCriteria?: string;
  offset?: number;
  limit?: number;
  sortBy?: string;
  orderAsc?: boolean;


  constructor(obj: LoadAllUsersParametersInterface) {
    this.searchCriteria = obj.searchCriteria;
    this.offset = obj.offset;
    this.limit = obj.limit;
    this.sortBy = obj.sortBy;
    this.orderAsc = obj.orderAsc;
  }
}
