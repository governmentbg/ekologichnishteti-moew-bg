import { Component } from '@angular/core';
import { ApplicationHistoryComponent } from '../../../common/components/application-history/application-history.component';
import { ApplicationOneDto } from '../../models/application-one.dto';
import { ApplicationOneResource } from '../../resources/application-one.resource';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { LoadingIndicatorService } from '../../../../../infrastructure/components/loading-indicator/services/loading-indicator.service';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { SvgIconComponent } from '../../../../../infrastructure/components/svg-icon/svg-icon.component';
import { SharedService } from '../../../../../infrastructure/services/shared-service';
import { ApplicationHistoryTypeLocalization } from '../../../../../infrastructure/constants/enum-localization.const';
import { ApplicationHistoryType } from '../../../common/enums/application-history-type.enum';

@Component({
  selector: 'app-application-one-history',
  standalone: true,
  imports: [TranslateModule, CommonModule, RouterModule, SvgIconComponent],
  providers: [ApplicationOneResource],
  templateUrl: './application-one-history.component.html'
})
export class ApplicationOneHistoryComponent extends ApplicationHistoryComponent<ApplicationOneDto> {
  applicationHistoryTypeLocalization = ApplicationHistoryTypeLocalization;
  applicationHistoryTypes = ApplicationHistoryType;

  constructor(
    protected resource: ApplicationOneResource,
    protected activatedRoute: ActivatedRoute,
    protected loadingIndicator: LoadingIndicatorService,
    protected sharedService: SharedService

  ) {
    super(resource, activatedRoute, loadingIndicator, sharedService);
  }
}
