import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { LoadingIndicatorService } from '../../../../../infrastructure/components/loading-indicator/services/loading-indicator.service';
import { ApplicationHistoryTypeLocalization } from '../../../../../infrastructure/constants/enum-localization.const';
import { SharedService } from '../../../../../infrastructure/services/shared-service';
import { ApplicationHistoryComponent } from '../../../common/components/application-history/application-history.component';
import { ApplicationHistoryType } from '../../../common/enums/application-history-type.enum';
import { ApplicationTwoDto } from '../../models/application-two.dto';
import { ApplicationTwoResource } from '../../resources/application-two.resource';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { SvgIconComponent } from '../../../../../infrastructure/components/svg-icon/svg-icon.component';
import { catchError, finalize } from 'rxjs';
import { ApplicationOneResource } from '../../../application-one/resources/application-one.resource';

@Component({
  selector: 'app-application-two-history',
  standalone: true,
  imports: [TranslateModule, CommonModule, RouterModule, SvgIconComponent],
  templateUrl: './application-two-history.component.html',
  providers: [ApplicationTwoResource, ApplicationOneResource]
})
export class ApplicationTwoHistoryComponent extends ApplicationHistoryComponent<ApplicationTwoDto> {
  applicationHistoryTypeLocalization = ApplicationHistoryTypeLocalization;
  applicationHistoryTypes = ApplicationHistoryType;
  recentApplicationOneId: number;

  constructor(
    protected resource: ApplicationTwoResource,
    protected activatedRoute: ActivatedRoute,
    protected loadingIndicator: LoadingIndicatorService,
    protected sharedService: SharedService,
    private resourceOne: ApplicationOneResource,
    private router: Router
  ) {
    super(resource, activatedRoute, loadingIndicator, sharedService);
  }

  ngAfterViewInit() {
    const rootId = +this.activatedRoute.snapshot.params.rootId;

    this.loadingIndicator.show();

    this.resourceOne.getIdByApplicationTwoId(rootId)
      .pipe(
        finalize(() => this.loadingIndicator.hide()),
        catchError(() => this.router.navigate(['/application/search']))
      )
      .subscribe((result: number) => {
        this.recentApplicationOneId = result;
      });
  }
}
