import { BaseSearchFilter } from "../../../infrastructure/models/base-search.filter";
import { OperatorType } from "../../application/application-one/enums/operator-type.enum";
import { Authority } from "../../nomenclature/models/authority.model";
import { Settlement } from "../../nomenclature/models/settlement/settlement.model";

export class OperatorSearchFilter extends BaseSearchFilter {
  firstName: string;
  middleName: string;
  lastName: string;

  type: OperatorType;

  legalEntityName: string;
  legalEntityUic: string;

  authorityRiosvId: number;
  authorityRiosv: Authority;

  authorityBasinId: number;
  authorityBasin: Authority;

  settlementId: number;
  settlement: Settlement;

  constructor() {
    super(10)
    this.type = null;
  }
}