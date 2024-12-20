import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { LoadingIndicatorService } from '../../../../../infrastructure/components/loading-indicator/services/loading-indicator.service';
import { PartPanelComponent } from '../../../../../infrastructure/components/part-panel/part-panel.component';
import { SvgIconComponent } from '../../../../../infrastructure/components/svg-icon/svg-icon.component';
import { SharedService } from '../../../../../infrastructure/services/shared-service';
import { ApplicantType } from '../../enums/applicant-type.enum';
import { ApplicationOneType } from '../../enums/application-one-type.enum';
import { ApplicationOneDto } from '../../models/application-one.dto';
import { ApplicationOneResource } from '../../resources/application-one.resource';
import { ApplicationAboutFormComponent } from '../common/application-about-form/application-about-form.component';
import { ApplicationBasicFormComponent } from '../common/application-basic-form/application-basic-form.component';
import { ApplicationOneDamageFormComponent } from '../common/application-one-damage-form/application-one-damage-form.component';
import { ApplicationOneLegalEntityFormComponent } from '../common/application-one-legal-entity-form/application-one-legal-entity-form.component';
import { ApplicationOneThreatFormComponent } from '../common/application-one-threat-form/application-one-threat-form.component';
import { finalize } from 'rxjs';
import { CommitStateLocalization } from '../../../../../infrastructure/constants/enum-localization.const';
import { ActionConfirmationModalComponent } from '../../../../../infrastructure/components/action-confirmation-modal/action-confirmation-modal.component';
import { CommitState } from '../../../common/enums/commit-state.enum';
import { ChangeApplicationStateDto } from '../../../common/models/change-application-state.dto';
import { ApplicationFileFormComponent } from '../../../common/components/application-file-form/application-file-from.component';
import { UserRoleAliases } from '../../../../../infrastructure/constants/constants';
import { RoleService } from '../../../../../infrastructure/services/role.service';

@Component({
  selector: 'app-application-one-view',
  standalone: true,
  templateUrl: './application-one-view.component.html',
  styleUrl: './application-one-view.component.css',
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
    RouterModule,
    ApplicationFileFormComponent
  ],
  providers: [
    ApplicationOneResource
  ]
})
export class ApplicationOneViewComponent {
  isAdmin: boolean = false;
  isMosv: boolean = false;

  private forms: { [key: string]: string } = {};
  @Input() model: ApplicationOneDto;
  @Input() isEditMode: boolean;
  @Input() modelBasicName: string;
  @Input() applicationOneId: number;

  @Output() startedAppTwo = new EventEmitter<boolean>();
  @Output() modelChange = new EventEmitter<ApplicationOneDto>();

  applicantTypes = ApplicantType;
  applicationOneTypes = ApplicationOneType;
  changeStateDescription: string;

  commitStateLocalization = CommitStateLocalization;
  commitState = CommitState;

  canSubmit: boolean = false;
  isViewMode: boolean = true;
  isFileFormDisplayed: boolean = false;
  isApplicationTwoFormDisplayed: boolean = false;

  constructor(
    private resource: ApplicationOneResource,
    private loadingIndicator: LoadingIndicatorService,
    private router: Router,
    private modal: NgbModal,
    private toastrService: ToastrService,
    private cdr: ChangeDetectorRef,
    private roleService: RoleService,
    public sharedService: SharedService
  ) {
    this.isAdmin = this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR);
    this.isMosv = this.roleService.hasRole(UserRoleAliases.MOSV);
  }

  enter() {
    if (!(this.model.applicationOneBasic.operator?.id > 0)) {
      this.toastrService.error('Необходимо е да се въведе оператор, за да се приключи случаят!');
      return;
    }

    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате да приключите първоначалното заявяване на информация по случая? Ако изберете "Да, искам", ще можете да редактирате само информацията по чл. 19а, но не и току-що въведените данни.';

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {

          const changeApplicationStateDto = new ChangeApplicationStateDto(this.model.id, null)

          this.resource.enterApplication(this.model.id, changeApplicationStateDto)
            .pipe(finalize(() => this.loadingIndicator.hide()))
            .subscribe((model: ApplicationOneDto) => {
              this.model = model;
              this.modelChange.emit(this.model);

              this.toastrService.success('Успешно приключване на информацията!');
            })
        }
      });
  }

  addApplicationTwo() {
    this.isApplicationTwoFormDisplayed = true;
    this.startedAppTwo.emit(true);
  }

  beginModification() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да започнете редакция?";

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();

          this.resource.beginModification(this.model, CommitState.modification)
            .pipe(finalize(() => this.loadingIndicator.hide()))
            .subscribe((model: ApplicationOneDto) => {
              this.model = model;
              this.modelChange.emit(this.model);

              this.toastrService.success('Успешно стартиране на редакция!');

              this.isEditMode = true;
              this.router.navigate(['/application/one/', model.id]);
            })
        }
      })
  }

  saveModification() {
    this.loadingIndicator.show();

    this.model.applicationOneBasic.name = this.modelBasicName;
    this.resource.saveModification(this.model)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((result) => {
        this.model = result;
        this.toastrService.success('Текущите промени са запазени успешно!');
      })
  }

  finishModification() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате да запазите промените?';

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();

          this.model.applicationOneBasic.name = this.modelBasicName;
          this.resource.finishModification(this.model, CommitState.pending)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe(result => {
              this.model = result;
              this.modelChange.emit(this.model);

              this.toastrService.success('Информацията е редактирана успешно!');

              this.isEditMode = false;
            });
        }
      });
  }

  enableModification() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да редактирате информацията?";

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {

          this.isEditMode = true;
        }
      });
  }

  cancelModification(dto: ApplicationOneDto) {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да откажете редакцията?";

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();

          this.resource.cancelModification(dto)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe(() => {
              this.toastrService.success('Редакцията е отказана!');
              this.router.navigate(['/application/search']);

              this.isEditMode = false;
            });
        }
      });
  }

  deleteApplication(id: number) {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да изтриете информацията?";

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();

          this.resource.delete(id)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe(() => {
              this.toastrService.success('Успешно изтриване на информацията!');
              this.router.navigate(['/application/search']);
            });
        }
      });
  }

  restoreApplication(id: number) {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = "Сигурни ли сте, че искате да възстановите информацията?";

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loadingIndicator.show();

          this.resource.restore(id)
            .pipe(
              finalize(() => this.loadingIndicator.hide())
            )
            .subscribe((result) => {
              this.model = result;
              this.modelChange.emit(this.model);

              this.toastrService.success('Успешно възстановяване на информацията!');
            });
        }
      });
  }

  changeFormValidStatus(form: string, status: string): void {
    this.forms[form] = status;
    this.canSubmit = Object.keys(this.forms).findIndex(e => this.forms[e] == 'INVALID') < 0;
  }

  ngDoCheck() {
    this.cdr.detectChanges();
  }

  displayFileForm() {
    this.isFileFormDisplayed = true;
  }
}