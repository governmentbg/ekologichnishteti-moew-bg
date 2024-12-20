import { Component, Input } from '@angular/core';
import { CommonFormComponent } from '../../../../../../infrastructure/components/common-form/common-form.component';
import { ApplicationOneDto } from '../../../models/application-one.dto';
import { SharedService } from '../../../../../../infrastructure/services/shared-service';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { AsyncSelectComponent } from '../../../../../../infrastructure/components/async-select/async-select.component';
import { ApplicantType } from '../../../enums/applicant-type.enum';
import { ApplicantTypeLocalization } from '../../../../../../infrastructure/constants/enum-localization.const';
import { CommonModule } from '@angular/common';
import { ApplicationOneType } from '../../../enums/application-one-type.enum';
import { IndividualDto } from '../../../models/individual.dto';
import { LegalEntityDto } from '../../../models/legal-entity.dto';
import { RegExps, UserRoleAliases } from '../../../../../../infrastructure/constants/constants';
import { SvgIconComponent } from '../../../../../../infrastructure/components/svg-icon/svg-icon.component';
import { ApplicationOneAuthorityDto } from '../../../models/application-one-authority.dto';
import { UserContextService } from '../../../../../user/services/user-context.service';
import { RoleService } from '../../../../../../infrastructure/services/role.service';
import { UserResource } from '../../../../../user/resources/user.resource';
import { ApplicationAuthorityDto } from '../../../../common/models/application-authority.dto';

@Component({
  selector: 'app-application-about-form',
  standalone: true,
  imports: [
    FormsModule,
    TranslateModule,
    AsyncSelectComponent,
    CommonModule,
    SvgIconComponent
  ],
  templateUrl: './application-about-form.component.html',
  styleUrl: './application-about-form.component.css'
})
export class ApplicationAboutFormComponent extends CommonFormComponent<ApplicationOneDto> {
  canSubmit: boolean = false;
  canAddAuthority: boolean = true;

  @Input() isViewMode: boolean;

  applicationOneTypes = ApplicationOneType;
  applicantTypes = ApplicantType;
  applicantTypesLocalization = ApplicantTypeLocalization;

  emailRegex = RegExps.EMAIL_REGEX;
  numberRegex = RegExps.NUMBER_REGEX;
  latinAndCyrillicNamesRegex = RegExps.LATIN_AND_CYRILLIC_NAMES_REGEX;
  phoneRegex = RegExps.PHONE_NUMBER_REGEX;

  applicationOneAuthorityIds: number[] = [];
  mainAuthorityId: number = undefined;

  isAdmin: boolean = false;
  isMosv: boolean = false;

  constructor(
    private roleService: RoleService,
    private userContextService: UserContextService,
    private userResource: UserResource,
    public sharedService: SharedService
  ) {
    super();
    this.isAdmin = this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR);
    this.isMosv = this.roleService.hasRole(UserRoleAliases.MOSV);
  }

  ngDoCheck() {
    this.applicationOneAuthorityIds = this.model.applicationOneAuthorities
      .map(({ authorityId }) => authorityId)
      .filter(i => i !== undefined);

    if (this.mainAuthorityId !== undefined) {
      this.applicationOneAuthorityIds.push(this.mainAuthorityId);
    }
  }

  onApplicantTypeChange(selectedType: ApplicantType) {
    this.model.clearAll();
    this.clearApplicationOneBasicOperator();
    this.mainAuthorityId = undefined;

    if (selectedType === this.applicantTypes.authority && !this.isAdmin) {
      this.userResource.getById(this.userContextService.userContext.userId).subscribe((user) => {
        this.model.applicant.authority = user.authority;
        this.mainAuthorityId = user.authorityId;
      });
    }

    if (selectedType === this.applicantTypes.individual
      || selectedType === this.applicantTypes.legalEntity
      || selectedType === this.applicantTypes.nonGovernmentalOrganization) {

      if (selectedType === this.applicantTypes.individual) {
        this.model.applicant.individual = new IndividualDto();
      }
      else if (selectedType === this.applicantTypes.legalEntity
        || selectedType === this.applicantTypes.nonGovernmentalOrganization) {
        this.model.applicant.legalEntity = new LegalEntityDto();

      }
      this.setAppOneType();
    } else {
      this.removeAppOneType();
    }
  }

  onOperatorChange(id: number) {
    this.model.applicant.operator.id = id;
    this.clearApplicationOneBasicOperator();
  }

  clearApplicationOneBasicOperator() {
    this.model.applicationOneBasic.operatorId = 0;
    this.model.applicationOneBasic.operator = null;
    this.model.applicationOneBasic.activityOfferings = [];
  }

  setAppOneType() {
    this.model.applicationOneType = ApplicationOneType.reportedDamage;
  }

  removeAppOneType() {
    this.model.applicationOneType = null;
  }

  isAnyChecked(): boolean {
    return this.model.applicationOneType === ApplicationOneType.threat ||
      this.model.applicationOneType === ApplicationOneType.damage;
  }

  fillAuthority(event: any, i: number): void {
    this.model.applicationOneAuthorities[i].authorityId = event.id;

    this.canAddAuthority = true;
  }

  validateAuthorities(): void {
    for (let index = 0; index < this.model.applicationOneAuthorities.length; index++) {
      if (this.model.applicationOneAuthorities[index].authorityId === 0 ||
        this.model.applicationOneAuthorities[index].authorityId === undefined) {
        this.canAddAuthority = false;
        return;
      }
    }

    this.canAddAuthority = true;
  }

  addAuthority() {
    this.model.applicationOneAuthorities.push(new ApplicationAuthorityDto());
    this.canAddAuthority = false;
  }

  removeAuthority(index: number) {
    this.model.applicationOneAuthorities.splice(index, 1);

    this.validateAuthorities();
  }
}