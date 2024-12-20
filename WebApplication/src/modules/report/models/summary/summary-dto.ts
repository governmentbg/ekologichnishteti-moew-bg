import { ApplicationOneType } from "../../../application/application-one/enums/application-one-type.enum";
import { ApplicantDto } from "../../../application/application-one/models/applicant.dto";
import { ActivityOfferingDto } from "../../../nomenclature/dtos/activity-offering.dto";
import { SummaryStageType } from "../../enums/summary-stage-type.enum";

export class SummaryDto {
  summaryStageType: SummaryStageType;
  description: string;
  descriptionAlt: string;
  applicationOneType: ApplicationOneType;
  startYear: number;
  endYear: number;

  hasWaterDamage: boolean;
  hasSoilDamage: boolean;
  hasSpeciesDamage: boolean;

  occurrenceDate: Date;
  occurrenceDateDescription: string;
  occurrenceDateDescriptionAlt: string;
  activityOfferings: ActivityOfferingDto[];

  procedureInitiatedDate: Date;
  procedureInitiatedDateDescription: string;
  procedureInitiatedDateDescriptionAlt: string;
  applicant: ApplicantDto;
  actionsTaken: string;
  actionsTakenAlt: string;
}