import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SvgIconComponent } from '../../../../infrastructure/components/svg-icon/svg-icon.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BaseSearchComponent } from '../../../../infrastructure/components/search-component/base-search.component';
import { Router } from '@angular/router';
import { LoadingIndicatorService } from '../../../../infrastructure/components/loading-indicator/services/loading-indicator.service';
import { OperatorSearchFilter } from '../../services/operator-search.filter';
import { OperatorResource } from '../../resources/operator.resource';
import { OperatorTypeLocalization } from '../../../../infrastructure/constants/enum-localization.const';
import { OperatorType } from '../../../application/application-one/enums/operator-type.enum';
import { AsyncSelectComponent } from '../../../../infrastructure/components/async-select/async-select.component';
import { AuthorityType } from '../../../application/application-one/enums/authority-type.enum';
import { Component, OnInit } from '@angular/core';
import { OperatorSearchResultDto } from '../../dtos/operator-search-result.dto';

@Component({
  selector: 'operator-search',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    TranslateModule,
    SvgIconComponent,
    AsyncSelectComponent,
    NgbModule,
  ],
  providers: [
    OperatorResource,
    OperatorSearchFilter,
    LoadingIndicatorService
  ],
  templateUrl: './operator-search.component.html',
})
export class OperatorSearchComponent extends BaseSearchComponent<OperatorSearchResultDto> implements OnInit {

  operatorTypeLocalization = OperatorTypeLocalization;
  operatorTypes = OperatorType;

  authorityType = AuthorityType;

  constructor(
    public override filter: OperatorSearchFilter,
    protected override resource: OperatorResource,
    protected override loadingIndicator: LoadingIndicatorService,
    private router: Router
  ) {
    super(filter, resource, loadingIndicator)
  }

  routeToNew() {
    this.router.navigate(['/operator/new']);
  }

  routeToView(id: number) {
    this.router.navigate(['/operator/view', id]);
  }

  onOperatorTypeChange(operatorType: OperatorType) {
    this.filter.type = operatorType;

    if (operatorType === this.operatorTypes.person) {
      this.filter.legalEntityName = null;
      this.filter.legalEntityUic = null;
    }
    else if (operatorType === this.operatorTypes.legalEntity) {
      this.filter.firstName = null;
      this.filter.middleName = null;
      this.filter.lastName = null;
    }
  }
}
