import { WorkItemType } from "./WorkItemType";

export interface WorkItemInterface {
  id?: string;
  name?: WorkItemType;
  usedInstances?: number;
  isDeleted?: boolean;
  isChecked?: boolean;
}

export class WorkItem implements WorkItemInterface {
  id?: string;
  name?: WorkItemType;
  usedInstances?: number;
  isDeleted?: boolean;
  isChecked?: boolean;

  constructor(obj: WorkItemInterface) {
    this.id = obj.id;
    this.name = obj.name;
    this.usedInstances = obj.usedInstances;
    this.isDeleted = obj.isDeleted;
    this.isChecked = obj.isChecked;
  }
}
