import { CommunicationMessType } from "./../model/CommunicationMessType";

export interface DbCommunicationChannelInterface {
  id: number;
  value: string;
  isDeleted: boolean;
  createdAt: Date;
  type: CommunicationMessType;
}

export class DbCommunicationChannel implements DbCommunicationChannelInterface {
  id: number;
  value: string;
  isDeleted: boolean;
  createdAt: Date;
  type: CommunicationMessType;

  constructor(obj: DbCommunicationChannelInterface) {
    this.id = obj.id;
    this.value = obj.value;
    this.isDeleted = obj.isDeleted;
    this.createdAt = obj.createdAt;
    this.type = obj.type;
  }
}
