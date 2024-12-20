import { NomenclatureDto } from "../../nomenclature/common/models/nomenclature.dto";
import { AnnualAdministrativeExpensesDto } from "./annual-administrative-expenses.dto";

export class PeriodDto extends NomenclatureDto {
  startDate: Date;
  endDate: Date;
  hasAnnualAdministrativeExpenses: boolean;
  annualAdministrativeExpenses: AnnualAdministrativeExpensesDto[];

  isInEditMode: boolean = false;
  minDate: any;
}