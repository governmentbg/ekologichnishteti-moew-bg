import { Nomenclature } from "../common/models/nomenclature.model";
import { Year } from "./year.model";

export class Period extends Nomenclature {
  startYearId: number;
  startYear: Year;
  endYearId: number;
  endYear: Year;
}