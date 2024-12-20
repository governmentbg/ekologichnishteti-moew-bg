import { BaseSearchFilter } from '../../../../infrastructure/models/base-search.filter';
import { CommitState } from '../../common/enums/commit-state.enum';
import { ApplicantType } from '../enums/applicant-type.enum';
import { OperatorDto } from '../../../nomenclature/dtos/operator.dto';
import { Authority } from '../../../nomenclature/models/authority.model';
import { ApplicationOneDamage } from '../enums/application-one-damage.enum';
import { ApplicationOneType } from '../enums/application-one-type.enum';
import { ApplicationOneDamageDto } from '../models/application-one-damage.dto';

export class ApplicationOneSearchFilter extends BaseSearchFilter {
  type: ApplicationOneType | null;
  dateFrom: Date | null;
  dateTo: Date | null;
  applicationOneState: CommitState | null;
  applicantType: ApplicantType | null;
  applicantAuthority: Authority | null;
  authority: Authority | null;

  //Change to collection
  riosv: Authority;
  riosvId: number | null;
  basin: Authority;
  basinId: number | null;

  operatorId: number;
  operator: OperatorDto;

  //Change to collection
  applicationOneDamage: ApplicationOneDamage | null;

  constructor() {
    super(10);
    this.type = null;
    this.applicationOneState = null;
    this.applicantType = null;
    this.authority = null;
    this.applicationOneDamage = null;
  }
}