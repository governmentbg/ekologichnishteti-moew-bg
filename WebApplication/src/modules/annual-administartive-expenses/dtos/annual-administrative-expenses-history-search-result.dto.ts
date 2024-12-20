import { AnnualAdministrativeExpensesHistoryDto } from "./annual-administrative-expenses-history.dto";

export class AnnualAdministrativeExpensesHistorySearchResultDto {
  authorityName: string;
  periodName: string;
  histories: AnnualAdministrativeExpensesHistoryDto[];
}