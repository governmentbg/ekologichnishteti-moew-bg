import { Settlement } from "../models/settlement/settlement.model";
import { OperatorType } from "../../application/application-one/enums/operator-type.enum";
import { OperatorCorrespondenceDto } from "./operator-correspondence.dto";
import { NomenclatureDto } from "../common/models/nomenclature.dto";
import { ActivityOfferingDto } from "./activity-offering.dto";
import { TerrainDto } from "./terrain.dto";

export class OperatorDto extends NomenclatureDto {
  firstName: string;
  middleName: string;
  lastName: string;
  fullname: string;

  type: OperatorType;

  legalEntityName: string;
  legalEntityUic: string;

  settlementId: number;
  settlement: Settlement;

  managementAddress: string;
  postalCode: string;

  operatorCorrespondenceId: number;
  operatorCorrespondence: OperatorCorrespondenceDto;

  terrains: TerrainDto[];
  activityOfferings: ActivityOfferingDto[];

  constructor() {
    super();
    this.settlement = new Settlement();
    this.operatorCorrespondence = new OperatorCorrespondenceDto();
  }
}