import { NomenclatureDto } from "../../../nomenclature/common/models/nomenclature.dto";
import { ActivityOfferingDto } from "../../../nomenclature/dtos/activity-offering.dto";
import { CodeDto } from "../../../nomenclature/dtos/code.dto";
import { OperatorDto } from "../../../nomenclature/dtos/operator.dto";
import { ApplicationOneType } from "../../application-one/enums/application-one-type.enum";
import { ApplicantDto } from "../../application-one/models/applicant.dto";
import { ActivityOfferingType } from "../../common/enums/activity-offering-type.enum";
import { ApplicationAuthorityDto } from "../../common/models/application-authority.dto";
import { ApplicationFileDto } from "../../common/models/application-file.dto";
import { CommitDto } from "../../common/models/commit.dto";
import { CaseStatus } from "../enums/case-status.enum";
import { PaymentSource } from "../enums/payment-source.enum";
import { ProceedingType } from "../enums/proceeding-type.enum";
import { ApplicationTwoMinistryOfInteriorDto } from "./applicatio-two-ministry-of-interior.dto";
import { ApplicationTwoAdministrativeCourtDto } from "./application-two-administrative-court.dto";
import { ApplicationTwoAuhtority } from "./application-two-authority.dto";
import { ApplicationTwoProsecutorDto } from "./application-two-prosecutor.dto";
import { BankGuaranteeDto } from "./financial-assurance/bank-guarantee.dto";
import { InsurancePolicyDto } from "./financial-assurance/insurance-policy.dto";
import { MortgageDto } from "./financial-assurance/mortgage-dto";
import { StakeDto } from "./financial-assurance/stake-dto";

export class ApplicationTwoDto extends CommitDto {
  applicationOneId: number;
  applicationOneType: ApplicationOneType;

  hasWaterDamage: boolean;
  hasSoilDamage: boolean;
  hasSpeciesDamage: boolean;

  occurrenceDate: Date;
  occurrenceDateDescription: string;

  confirmedDate: Date;

  subActivities: NomenclatureDto[];

  procedureInitiatedDate: Date;
  procedureInitiatedDateDescription: string;

  applicantId: number;
  applicant: ApplicantDto;

  operatorId: number;
  operator: OperatorDto;

  activityOfferingType: ActivityOfferingType;
  notListedDescription: string;
  diffuseNatureDescription: string;

  applicationTwoActivityOfferings: ActivityOfferingDto[] = [];
  applicationTwoAuthorities: ApplicationAuthorityDto[] = [];

  codes: CodeDto[] = [];

  proceedingInfoAbsence: boolean = false;
  proceedingType: ProceedingType;
  applicationTwoAdministrativeCourts: ApplicationTwoAdministrativeCourtDto[] = [];
  applicationTwoProsecutors: ApplicationTwoProsecutorDto[] = [];
  applicationTwoMinistryOfInteriors: ApplicationTwoMinistryOfInteriorDto[] = [];
  proceedingInfo: string;

  legalProcedureResult: string;

  hasPreventionProcedureResult: boolean;
  preventionProcedureResult: string;

  hasRemovalProcedureResult: boolean;
  removalProcedureResult: string;

  caseStatus: CaseStatus;
  preventionOrRemovalProcedureFinishDate: Date;
  preventionOrRemovalProcedureFinishInformation: string;

  actionsTaken: string;

  paidByResponsibleParties: number;
  recoveredSubsequentlyByResponsibleParties: number;
  hasUnreimbursedExpense: boolean;
  unreimbursedExpenseValue: number;
  unreimbursedExpense: string;
  additionalExpenseText: string;

  paymentSource: PaymentSource;
  otherPaymentSource: string;

  hasInsurancePolicy: boolean;
  insurancePolicyId: number;
  insurancePolicy: InsurancePolicyDto;

  hasBankGuarantee: boolean;
  bankGuaranteeId: number;
  bankGuarantee: BankGuaranteeDto;

  hasMortgage: boolean;
  mortgageId: number;
  mortgage: MortgageDto;

  hasStake: boolean;
  stakeId: number;
  stake: StakeDto;

  hasNoCollateral: boolean;
  noCollateralAdditionalInformation: string;

  additionalInformation: string;

  applicationTwoFiles: ApplicationFileDto[] = [];

  constructor() {
    super()

    this.applicant = new ApplicantDto();
    this.operator = new OperatorDto();
  }
}