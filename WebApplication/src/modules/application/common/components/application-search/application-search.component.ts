import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SvgIconComponent } from '../../../../../infrastructure/components/svg-icon/svg-icon.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ApplicationOneType } from '../../../application-one/enums/application-one-type.enum';
import { ApplicationOneSearchFilter } from '../../../application-one/services/application-one-search.filter';
import { CommonModule } from '@angular/common';
import { ApplicationOneSearchResultDto } from '../../../application-one/models/application-one-search-result.dto';
import { BaseSearchComponent } from '../../../../../infrastructure/components/search-component/base-search.component';
import { LoadingIndicatorService } from '../../../../../infrastructure/components/loading-indicator/services/loading-indicator.service';
import { ApplicationOneResource } from '../../../application-one/resources/application-one.resource';
import { Router } from '@angular/router';
import { ValidDateDirective } from '../../../../../infrastructure/directives/valid-date.directive';
import { CommitState } from '../../enums/commit-state.enum';
import { finalize } from 'rxjs';
import { SearchResultItemDto } from '../../../../../infrastructure/models/search-result-item.dto';
import { ApplicationSearchTableComponent } from '../application-search-table/application-search-table.component';
import { ApplicantType } from '../../../application-one/enums/applicant-type.enum';
import { AsyncSelectComponent } from '../../../../../infrastructure/components/async-select/async-select.component';
import { ApplicationOneDamage } from '../../../application-one/enums/application-one-damage.enum';
import { ApplicantTypeLocalization, ApplicationOneDamageLocalization, ApplicationOneTypeLocalization, CommitStateLocalization } from '../../../../../infrastructure/constants/enum-localization.const';
import { AuthorityType } from '../../../application-one/enums/authority-type.enum';
import { UserRoleAliases } from '../../../../../infrastructure/constants/constants';
import { RoleService } from '../../../../../infrastructure/services/role.service';

@Component({
  selector: 'application-search',
  standalone: true,
  templateUrl: 'application-search.component.html',
  imports: [
    FormsModule,
    CommonModule,
    TranslateModule,
    SvgIconComponent,
    NgbModule,
    ValidDateDirective,
    ApplicationSearchTableComponent,
    AsyncSelectComponent
  ],
  providers: [
    ApplicationOneResource,
    ApplicationOneSearchFilter,
    ApplicationSearchTableComponent,
  ]
})
export class ApplicationSearchComponent extends BaseSearchComponent<ApplicationOneSearchResultDto> {
  applicationTypeLocalization = ApplicationOneTypeLocalization;

  applicantTypeLocalization = ApplicantTypeLocalization;
  applicantTypes = ApplicantType;

  applicationOneDamageLocalization = ApplicationOneDamageLocalization;
  commitStateLocalization = CommitStateLocalization;
  commitStates = CommitState;

  applicationTypes = ApplicationOneType;

  areActiveApplicationsShown: boolean = true;
  deletedApplications: ApplicationOneSearchResultDto[] = [];
  activeApplications: SearchResultItemDto<ApplicationOneSearchResultDto> = new SearchResultItemDto<ApplicationOneSearchResultDto>();
  authorityType = AuthorityType;
  applicationOneDamages = ApplicationOneDamage;

  isAdmin: boolean = false;
  isMosv: boolean = false;
  isAuthorizedPassive: boolean = false;
  today = new Date();
  maxDate = { year: this.today.getFullYear(), month: this.today.getMonth() + 1, day: this.today.getDate() };

  constructor(
    public override filter: ApplicationOneSearchFilter,
    protected override resource: ApplicationOneResource,
    protected override loadingIndicator: LoadingIndicatorService,
    private router: Router,
    private roleSerivce: RoleService
  ) {
    super(filter, resource, loadingIndicator)
    this.isAdmin = this.roleSerivce.hasRole(UserRoleAliases.ADMINISTRATOR);
    this.isMosv = this.roleSerivce.hasRole(UserRoleAliases.MOSV);
    this.isAuthorizedPassive = this.roleSerivce.hasRole(UserRoleAliases.AUTHORIZED_ENTITY_PASSIVE);
  }

  ngAfterViewInit() {
    if (this.isAdmin || this.isMosv) {
      this.loadingIndicator.show();

      this.resource.getByState(CommitState.deleted)
        .pipe(
          finalize(() => this.loadingIndicator.hide()),
        )
        .subscribe((result: any) => {
          this.deletedApplications = result;
        });
    }
  }

  routeToNew() {
    this.router.navigate(['/application/new']);
  }

  routeToReports(): void {
    this.router.navigate(['/reports']);
  }

  clearSelects(): void {
    if (this.filter.applicantType === this.applicantTypes.authority) {
      this.filter.operatorId = null;
      this.filter.operator = null;
    }

    if (this.filter.applicantType === this.applicantTypes.operator) {
      this.filter.applicantAuthority = null;
    }
  }
}
