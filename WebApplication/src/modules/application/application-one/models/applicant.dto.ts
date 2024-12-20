import { AuthorityDto } from "../../../nomenclature/dtos/authority.dto";
import { ApplicantType } from "../enums/applicant-type.enum";
import { IndividualDto } from "./individual.dto";
import { LegalEntityDto } from "./legal-entity.dto";
import { OperatorDto } from "../../../nomenclature/dtos/operator.dto";

export class ApplicantDto {
  applicantType: ApplicantType;

  authority: AuthorityDto;

  operator: OperatorDto;

  individual: IndividualDto;

  legalEntity: LegalEntityDto;

  clearAll() {
    this.authority = null;
    this.operator = null;
    this.individual = null;
    this.legalEntity = null;
  }
}