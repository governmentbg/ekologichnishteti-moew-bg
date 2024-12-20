import { Component, Input, OnInit } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { ActionConfirmationModalComponent } from '../../../../../infrastructure/components/action-confirmation-modal/action-confirmation-modal.component';
import { LoadingIndicatorService } from '../../../../../infrastructure/components/loading-indicator/services/loading-indicator.service';
import { SharedService } from '../../../../../infrastructure/services/shared-service';
import { ApplicationTwoDto } from '../../models/application-two.dto';
import { ApplicationTwoResource } from '../../resources/application-two.resource';
import { SvgIconComponent } from '../../../../../infrastructure/components/svg-icon/svg-icon.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ApplicationOneType } from '../../../application-one/enums/application-one-type.enum';
import { ApplicationOneDto } from '../../../application-one/models/application-one.dto';
import { ApplicationTwoCommonComponent } from '../application-two-common/application-two-common.component';
import { ActivityOfferingType } from '../../../common/enums/activity-offering-type.enum';

@Component({
  selector: 'app-application-two-new',
  standalone: true,
  imports: [
    TranslateModule,
    SvgIconComponent,
    FormsModule,
    CommonModule,
    NgbModule,
    ApplicationTwoCommonComponent
  ],
  templateUrl: './application-two-new.component.html',
  providers: [ApplicationTwoResource]
})
export class ApplicationTwoNewComponent implements OnInit {
  @Input() applicationOneDto: ApplicationOneDto;

  applicationOneId: number;
  model: ApplicationTwoDto = new ApplicationTwoDto();

  isEditMode: boolean = true;
  isViewMode: boolean = false;
  canSubmit: boolean = true;
  areCodesValid: boolean = false;
  areActivityOfferingsValid: boolean = false;

  applicationOneTypes = ApplicationOneType;

  constructor(
    private resource: ApplicationTwoResource,
    private loadingIndicator: LoadingIndicatorService,
    private router: Router,
    private modal: NgbModal,
    private toastrService: ToastrService,
    public sharedService: SharedService,
    private activatedRoute: ActivatedRoute
  ) {
  }

  ngOnInit(): void {
    this.applicationOneId = +this.activatedRoute.snapshot.params.parentId;

    this.fillExistingFields();

    document.querySelector('#applicationTwoForm').scrollIntoView();

    this.model.applicationOneType = this.applicationOneDto.applicationOneType;
  }

  changeFormValidStatus(status: string): void {
    this.canSubmit = status === 'INVALID' ? false : true;
  }

  sendApplicationTwo() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате да добавите информацията?';
    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();
          this.model.applicationOneId = this.applicationOneId;

          this.resource.create(this.model)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe(() => {
              this.toastrService.success('Информацията е добавена успешно!');
              this.router.navigate(['/application/search']);
            });
        }
      });
  }

  clearAll() {
    this.model = new ApplicationTwoDto();
  }

  routeTo(route: string) {
    this.router.navigate([route]);
  }

  fillExistingFields(): void {
    this.model.hasSpeciesDamage = this.applicationOneDto.applicationOneBasic.hasSpeciesDamage;
    this.model.hasWaterDamage = this.applicationOneDto.applicationOneBasic.hasWaterDamage;
    this.model.hasSoilDamage = this.applicationOneDto.applicationOneBasic.hasSoilDamage;

    this.model.applicant.applicantType = this.applicationOneDto.applicant.applicantType;
    this.model.applicantId = this.applicationOneDto.applicantId;
    this.model.applicant = JSON.parse(JSON.stringify(this.applicationOneDto.applicant));

    this.model.operatorId = this.applicationOneDto.applicationOneBasic.operator.id;
    this.model.operator = this.applicationOneDto.applicationOneBasic.operator;
    this.model.applicationTwoActivityOfferings = JSON.parse(JSON.stringify(this.applicationOneDto.applicationOneBasic.activityOfferings));
    this.model.applicationTwoAuthorities = JSON.parse(JSON.stringify(this.applicationOneDto.applicationOneAuthorities));
    this.model.activityOfferingType = this.applicationOneDto.applicationOneBasic.activityOfferingType;

    if (this.model.activityOfferingType === ActivityOfferingType.notListed) {
      this.model.notListedDescription = this.applicationOneDto.applicationOneBasic.notListedDescription;
    }

    if (this.model.activityOfferingType === ActivityOfferingType.diffuseNature) {
      this.model.diffuseNatureDescription = this.applicationOneDto.applicationOneBasic.diffuseNatureDescription;
    }
  }
}
