import { NomenclatureDto } from "../../../nomenclature/common/models/nomenclature.dto";
import { ActivityOfferingDto } from "../../../nomenclature/dtos/activity-offering.dto";
import { AuthorityDto } from "../../../nomenclature/dtos/authority.dto";
import { OperatorDto } from "../../../nomenclature/dtos/operator.dto";
import { ActivityOfferingType } from "../../common/enums/activity-offering-type.enum";

export class ApplicationOneBasicDto {
  id: number;
  incomingDocNumber: string;
  name: string;

  operatorId: number;
  operator: OperatorDto;

  activityOfferingType: ActivityOfferingType;
  notListedDescription: string;
  diffuseNatureDescription: string;

  activityOfferings: ActivityOfferingDto[] = [];

  hasWaterDamage: boolean;
  hasSoilDamage: boolean;
  hasSpeciesDamage: boolean;

  territorialRanges: AuthorityDto[] = [];
  occurenceLocation: string;
  occurenceReasons: string;
  additionalInformation: string;

  hasInternationalElement: boolean;
  internationalElementDescription: string;

  culpritCountryId: number;
  culpritCountry: NomenclatureDto;
  affectedCountries: NomenclatureDto[] = [];
}