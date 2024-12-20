import { BaseSearchFilter } from "../../../infrastructure/models/base-search.filter";
import { Authority } from "../../nomenclature/models/authority.model";
import { ReportType } from "../enums/report-type.enum";

export class ReportSearchFilter extends BaseSearchFilter {
  reportType: ReportType;
  territorialRange: Authority;

  constructor() {
    super(10);
    this.reportType = null;
    this.territorialRange = null;
  }
}