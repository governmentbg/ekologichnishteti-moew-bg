import { SummaryDto } from "./summary-dto";

export class SummaryReportDto {
  count: number;
  finishedSummaries: SummaryDto[];
  onGoingSummaries: SummaryDto[];
}