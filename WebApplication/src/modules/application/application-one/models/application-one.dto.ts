import { AuthorityDto } from "../../../nomenclature/dtos/authority.dto";
import { ApplicationAuthorityDto } from "../../common/models/application-authority.dto";
import { ApplicationFileDto } from "../../common/models/application-file.dto";
import { CommitDto } from "../../common/models/commit.dto";
import { ApplicationOneType } from "../enums/application-one-type.enum";
import { ApplicantDto } from "./applicant.dto";
import { ApplicationOneAuthorityDto } from "./application-one-authority.dto";
import { ApplicationOneBasicDto } from "./application-one-basic.dto";
import { ApplicationOneDamageDto } from "./application-one-damage.dto";
import { ApplicationOneLegalEntityDto } from "./application-one-legal-entity.dto";
import { ApplicationOneThreatDto } from "./application-one-threat.dto";

export class ApplicationOneDto extends CommitDto {
  id: number;
  registerNumber: string;
  applicationOneType: ApplicationOneType;

  applicationOneAuthorities: ApplicationAuthorityDto[] = [];

  applicantId: number;
  applicant: ApplicantDto;

  applicationOneBasic: ApplicationOneBasicDto;
  applicationOneLegalEntity: ApplicationOneLegalEntityDto;
  applicationOneThreat: ApplicationOneThreatDto;
  applicationOneDamage: ApplicationOneDamageDto;

  applicationOneFiles: ApplicationFileDto[] = [];
  hasApplicationTwo: boolean;
  applicationTwoRootId: number;

  constructor() {
    super()

    this.applicant = new ApplicantDto();
    this.applicationOneBasic = new ApplicationOneBasicDto();
  }

  clearAll() {
    this.clearOptionals();

    this.applicant.clearAll();
  }

  clearOptionals() {
    this.applicationOneLegalEntity = new ApplicationOneLegalEntityDto();
    this.applicationOneThreat = new ApplicationOneThreatDto();
    this.applicationOneDamage = new ApplicationOneDamageDto();
  }
}