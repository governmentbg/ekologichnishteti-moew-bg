import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationOneViewComponent } from '../../../application-one/components/application-one-view/application-one-view.component';
import { finalize, catchError } from 'rxjs';
import { LoadingIndicatorService } from '../../../../../infrastructure/components/loading-indicator/services/loading-indicator.service';
import { SharedService } from '../../../../../infrastructure/services/shared-service';
import { ApplicationOneDto } from '../../../application-one/models/application-one.dto';
import { ApplicationOneResource } from '../../../application-one/resources/application-one.resource';
import { CommitState } from '../../enums/commit-state.enum';
import { CommonModule } from '@angular/common';
import { ApplicationTwoNewComponent } from '../../../application-two/components/application-two-new/application-two-new.component';
import { ApplicationTwoViewComponent } from '../../../application-two/components/application-two-view/application-two-view.component';
import { ApplicationTwoDto } from '../../../application-two/models/application-two.dto';
import { ApplicationTwoResource } from '../../../application-two/resources/application-two.resource';
import { LoginService } from '../../../../user/services/login.service';
import { ApplicationTwoCommitStateLocalization, CommitStateLocalization } from '../../../../../infrastructure/constants/enum-localization.const';
import { TranslateModule } from '@ngx-translate/core';
import { HelpTooltipComponent } from '../../../../../infrastructure/components/help-tooltip/help-tooltip.component';

@Component({
  selector: 'app-application-view',
  standalone: true,
  imports: [ApplicationOneViewComponent, ApplicationTwoNewComponent, ApplicationTwoViewComponent, CommonModule, TranslateModule, HelpTooltipComponent],
  templateUrl: './application-view.component.html',
  styleUrl: './application-view.component.css',
  providers: [ApplicationOneResource, ApplicationTwoResource]
})
export class ApplicationViewComponent implements OnInit {
  applicationOneId: number;

  applicationOne: ApplicationOneDto = new ApplicationOneDto();
  applicationTwo: ApplicationTwoDto = new ApplicationTwoDto();
  isEditMode: boolean = false;
  isEditModeTwo: boolean = false;
  modelBasicName: string;
  hasApplicationTwo: boolean;
  newApplicationTwo: boolean;
  scroll: boolean = true;

  commitStateLocalization = CommitStateLocalization;
  commitStates = CommitState;

  applicationTwoCommitStateLocalization = ApplicationTwoCommitStateLocalization;


  constructor(private resourceOne: ApplicationOneResource,
    private resourceTwo: ApplicationTwoResource,
    private loadingIndicator: LoadingIndicatorService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    public sharedService: SharedService,
    private userService: LoginService) {

  }

  ngOnInit(): void {
    this.applicationOneId = +this.activatedRoute.snapshot.params.parentId;

    this.getById();
  }

  getById() {
    this.loadingIndicator.show();
    this.resourceOne.getById(this.applicationOneId)
      .pipe(
        finalize(() => {
          this.loadingIndicator.hide();
        }),
        catchError(() => this.router.navigate(['/application/search']))
      )
      .subscribe((result: ApplicationOneDto) => {
        this.applicationOne = result;
        this.hasApplicationTwo = result.hasApplicationTwo;
        this.isEditMode = result.commitState === CommitState.modification;
        this.modelBasicName = result.applicationOneBasic?.name;

        if (this.hasApplicationTwo) {
          this.getTwoById();
        }
      })
  }

  startAppTwo() {
    this.newApplicationTwo = true;
  }

  getTwoById() {
    this.loadingIndicator.show();
    this.resourceTwo.getById(this.applicationOne.applicationTwoRootId)
      .pipe(
        finalize(() => this.loadingIndicator.hide()),
        catchError(() => this.router.navigate(['/application/search']))
      )
      .subscribe((result: ApplicationTwoDto) => {
        this.applicationTwo = result;
        this.isEditModeTwo = result.commitState === CommitState.modification;
      })
  }
}
