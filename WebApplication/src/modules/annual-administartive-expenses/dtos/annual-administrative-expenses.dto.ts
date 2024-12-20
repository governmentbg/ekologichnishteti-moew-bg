import { AuthorityDto } from "../../nomenclature/dtos/authority.dto";
import { PeriodDto } from "./period.dto";

export class AnnualAdministrativeExpensesDto {
  id: number;
  rootId: number;
  authorityId: number;
  authority: AuthorityDto;
  amount: number;
  periodId: number;
  period: PeriodDto;

  isInEditMode: boolean = false;
}