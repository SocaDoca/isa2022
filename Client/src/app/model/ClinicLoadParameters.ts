export interface ClinicLoadParametersInterface {
  searchCriteria?: string;
  offset?: number;
  limit?: number;
  sortBy?: string;
  orderAsc?: boolean;

}

export class ClinicLoadParameters implements ClinicLoadParametersInterface {
  searchCriteria?: string;
  offset?: number;
  limit?: number;
  sortBy?: string;
  orderAsc?: boolean;


  constructor(obj: ClinicLoadParametersInterface) {
    this.searchCriteria = obj.searchCriteria;
    this.offset = obj.offset;
    this.limit = obj.limit;
    this.sortBy = obj.sortBy;
    this.orderAsc = obj.orderAsc;
  }
}
