import { Directive } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs';
import { LoadingIndicatorService } from '../../../../../infrastructure/components/loading-indicator/services/loading-indicator.service';
import { ApplicationHistoryDto } from '../../models/application-history.dto';
import { ApplicationResource } from '../../resoruces/application.resource';
import { SharedService } from '../../../../../infrastructure/services/shared-service';
import { ApplicationHistoryResultDto } from '../../models/application-history-result.dto';
@Directive({
  standalone: true
})
export class ApplicationHistoryComponent<TDto> {
  applications: ApplicationHistoryDto[] = [];
  parentRouteId: number;
  recentApplicationId: number;

  constructor(
    protected resource: ApplicationResource<TDto>,
    protected activatedRoute: ActivatedRoute,
    protected loadingIndicator: LoadingIndicatorService,
    protected sharedService: SharedService
  ) { }

  ngOnInit(): void {
    const rootId = +this.activatedRoute.snapshot.params.rootId;
    this.parentRouteId = this.activatedRoute.parent.snapshot.params['parentId'];

    this.loadingIndicator.show();
    this.resource.getHistory(rootId)
      .pipe(finalize(() => this.loadingIndicator.hide()))
      .subscribe((result: ApplicationHistoryResultDto) => {
        this.applications = result.applicationHistoryDtos;
        this.recentApplicationId = result.recentApplicationId;
      });
  }
}
