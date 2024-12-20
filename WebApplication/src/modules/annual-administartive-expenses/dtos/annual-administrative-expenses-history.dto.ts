import { AuthorityDto } from "../../nomenclature/dtos/authority.dto";

export class AnnualAdministrativeExpensesHistoryDto {
  userAuthority: AuthorityDto;
  userFullName: string;
  modificationDate: Date;
  annualAdministrativeExpenseAmount: number;
}