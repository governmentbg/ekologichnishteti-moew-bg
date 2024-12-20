import { ChangeDetectorRef, Component } from '@angular/core';
import { PartPanelComponent } from '../../../../../infrastructure/components/part-panel/part-panel.component';
import { ApplicationAboutFormComponent } from "../common/application-about-form/application-about-form.component";
import { ApplicationOneDto } from '../../models/application-one.dto';
import { TranslateModule } from '@ngx-translate/core';
import { ApplicationBasicFormComponent } from '../common/application-basic-form/application-basic-form.component';
import { ApplicantType } from '../../enums/applicant-type.enum';
import { ApplicationOneLegalEntityFormComponent } from '../common/application-one-legal-entity-form/application-one-legal-entity-form.component';
import { CommonModule } from '@angular/common';
import { ApplicationOneType } from '../../enums/application-one-type.enum';
import { ApplicationOneThreatFormComponent } from '../common/application-one-threat-form/application-one-threat-form.component';
import { ApplicationOneDamageFormComponent } from '../common/application-one-damage-form/application-one-damage-form.component';
import { SvgIconComponent } from '../../../../../infrastructure/components/svg-icon/svg-icon.component';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { ActionConfirmationModalComponent } from '../../../../../infrastructure/components/action-confirmation-modal/action-confirmation-modal.component';
import { LoadingIndicatorService } from '../../../../../infrastructure/components/loading-indicator/services/loading-indicator.service';
import { SharedService } from '../../../../../infrastructure/services/shared-service';
import { ApplicationOneResource } from '../../resources/application-one.resource';
import { ApplicationFileFormComponent } from '../../../common/components/application-file-form/application-file-from.component';

@Component({
  selector: 'app-application-new',
  standalone: true,
  templateUrl: './application-one-new.component.html',
  styleUrl: './application-one-new.component.css',
  imports: [
    PartPanelComponent,
    CommonModule,
    TranslateModule,
    ApplicationAboutFormComponent,
    ApplicationBasicFormComponent,
    ApplicationOneLegalEntityFormComponent,
    ApplicationOneThreatFormComponent,
    ApplicationOneDamageFormComponent,
    SvgIconComponent,
    ApplicationFileFormComponent
  ],
  providers: [
    ApplicationOneResource
  ]
})
export class ApplicationOneNewComponent {

  private forms: { [key: string]: string } = {};

  model: ApplicationOneDto = new ApplicationOneDto();
  modelBasicName: string;

  applicantTypes = ApplicantType;
  applicationOneTypes = ApplicationOneType;

  canSubmit: boolean = false;
  isEditMode: boolean = true;
  isViewMode: boolean = false;
  isFileFormDisplayed: boolean = false;

  constructor(
    private resource: ApplicationOneResource,
    private loadingIndicator: LoadingIndicatorService,
    private router: Router,
    private modal: NgbModal,
    private toastrService: ToastrService,
    private cdr: ChangeDetectorRef,
    public sharedService: SharedService,
  ) {
  }

  changeFormValidStatus(form: string, status: string): void {
    if (this.model.applicant.applicantType === this.applicantTypes.individual
      || this.model.applicant.applicantType === this.applicantTypes.legalEntity
      || this.model.applicant.applicantType === this.applicantTypes.nonGovernmentalOrganization) {
      delete this.forms['applicationOneDamageForm'];
      delete this.forms['applicationOneThreadForm'];

    } else {
      delete this.forms['applicationOneLegalEntityForm'];

      if (this.model.applicationOneType === this.applicationOneTypes.damage) {
        delete this.forms['applicationOneThreadForm'];
      }
      else if (this.model.applicationOneType === this.applicationOneTypes.threat) {
        delete this.forms['applicationOneDamageForm'];
      }
    }
    this.forms[form] = status;
    this.canSubmit = Object.keys(this.forms).findIndex(e => this.forms[e] === 'INVALID') < 0;
  }

  sendApplicationOne() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате да създадете нов случай?';
    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();

          this.model.applicationOneBasic.name = this.modelBasicName;
          this.resource.create(this.model)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe(() => {
              this.toastrService.success('Случаят е създаден успешно!');
              this.router.navigate(['/application/search']);
            });
        }
      });
  }

  clearAll() {
    this.model = new ApplicationOneDto();
  }

  routeTo(route: string) {
    this.router.navigate([route]);
  }

  ngDoCheck() {
    this.cdr.detectChanges();
  }

  displayFileForm() {
    this.isFileFormDisplayed = true;
  }
}
