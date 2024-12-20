import { ApplicationHistoryType } from "../enums/application-history-type.enum";

export class ApplicationHistoryDto {
  applicationId: number;
  rootId: number;
  type: ApplicationHistoryType;
  modificationDate: Date;
  userFullName: string;
}