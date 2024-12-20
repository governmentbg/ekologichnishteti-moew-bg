import { OperatorType } from "../../application/application-one/enums/operator-type.enum";
import { Authority } from "../../nomenclature/models/authority.model";
import { Settlement } from "../../nomenclature/models/settlement/settlement.model";

export class OperatorSearchResultDto {
  id: number;

  type: OperatorType;

  fullname: string;
  legalEntityUic: string;

  authorityRiosvId: number;
  authorityRiosv: Authority;

  authorityBasinId: number;
  authorityBasin: Authority;

  settlementId: number;
  settlement: Settlement;
}