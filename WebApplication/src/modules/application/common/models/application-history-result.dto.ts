import { ApplicationHistoryDto } from "./application-history.dto";

export class ApplicationHistoryResultDto {
  recentApplicationId: number;
  applicationHistoryDtos: ApplicationHistoryDto[];
}