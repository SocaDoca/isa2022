import { CommunicationMessageType } from "./CommunicationMessageType";

export interface DbCommunicationChannelInterface {
  id: number;
  value: string;
  isDeleted: boolean;
  createdAt: Date;
  type: CommunicationMessageType;
}

export class DbCommunicationChannel implements DbCommunicationChannelInterface {
  id: number;
  value: string;
  isDeleted: boolean;
  createdAt: Date;
  type: CommunicationMessageType;

  constructor(obj: DbCommunicationChannelInterface) {
    this.id = obj.id;
    this.value = obj.value;
    this.isDeleted = obj.isDeleted;
    this.createdAt = obj.createdAt;
    this.type = obj.type;
  }
}
