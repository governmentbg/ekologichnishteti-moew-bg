import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApplicationTwoDto } from '../../models/application-two.dto';
import { CommitState } from '../../../common/enums/commit-state.enum';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { catchError, finalize } from 'rxjs';
import { ActionConfirmationModalComponent } from '../../../../../infrastructure/components/action-confirmation-modal/action-confirmation-modal.component';
import { LoadingIndicatorService } from '../../../../../infrastructure/components/loading-indicator/services/loading-indicator.service';
import { SharedService } from '../../../../../infrastructure/services/shared-service';
import { ApplicationTwoResource } from '../../resources/application-two.resource';
import { ApplicationTwoCommitStateLocalization } from '../../../../../infrastructure/constants/enum-localization.const';
import { ChangeApplicationStateDto } from '../../../common/models/change-application-state.dto';
import { ApplicationTwoCommonComponent } from '../application-two-common/application-two-common.component';
import { TranslateModule } from '@ngx-translate/core';
import { RoleService } from '../../../../../infrastructure/services/role.service';
import { UserRoleAliases } from '../../../../../infrastructure/constants/constants';

@Component({
  selector: 'app-application-two-view',
  standalone: true,
  imports: [
    TranslateModule,
    FormsModule,
    CommonModule,
    RouterModule,
    NgbModule,
    ApplicationTwoCommonComponent
  ],
  templateUrl: './application-two-view.component.html',
  styleUrl: './application-two-view.component.css',
  providers: [ApplicationTwoResource]
})
export class ApplicationTwoViewComponent implements OnInit {
  isAdmin: boolean = false;
  isMosv: boolean = false;

  @Input() model: ApplicationTwoDto = new ApplicationTwoDto();
  @Input() isEditMode: boolean;
  @Input() scroll: boolean = false;
  @Input() loadApplicationTwo: boolean = true;

  @Output() modelChange = new EventEmitter<ApplicationTwoDto>();

  isViewMode: boolean = true;
  canSubmit: boolean = true;
  areCodesValid: boolean = true;
  areActivityOfferingsValid: boolean = true;

  applicationTwoId: number;

  commitState = CommitState;
  commitStateLocalization = ApplicationTwoCommitStateLocalization;

  constructor(private resource: ApplicationTwoResource,
    private loadingIndicator: LoadingIndicatorService,
    private router: Router,
    private modal: NgbModal,
    private toastrService: ToastrService,
    private roleService: RoleService,
    public sharedService: SharedService,
    private activatedRoute: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.applicationTwoId = +this.activatedRoute.snapshot.params.parentId;

    if (this.loadApplicationTwo) {
      this.getById(this.applicationTwoId);
    }

    if (this.scroll) {
      document.querySelector('#applicationTwoForm').scrollIntoView();
    }

    this.isAdmin = this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR);
    this.isMosv = this.roleService.hasRole(UserRoleAliases.MOSV);
  }

  changeFormValidStatus(status: string): void {
    this.canSubmit = status === 'INVALID' ? false : true;
  }

  getById(id: number) {
    this.loadingIndicator.show();
    this.resource.getById(id)
      .pipe(
        finalize(() => this.loadingIndicator.hide()),
        catchError(() => this.router.navigate(['/application/search']))
      )
      .subscribe((result: ApplicationTwoDto) => {
        this.model = result;
        this.isEditMode = result.commitState === CommitState.modification && this.model.hasPermissionToEdit;
      })
  }

  complete() {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате да финализирате процеса по попълване на информация по чл. 19а по случая? Ако изберете "Да, искам", преписката ще се счита за приключена и повече няма да имате възможност да редактирате данни по нея.';

    confirmationModal.result
      .then((result: boolean) => {
        if (result) {

          const changeApplicationStateDto = new ChangeApplicationStateDto(this.model.id, null)

          this.resource.completeApplication(this.model.id, changeApplicationStateDto)
            .pipe(finalize(() => this.loadingIndicator.hide()))
            .subscribe((model: ApplicationTwoDto) => {
              this.model = model;
              this.modelChange.emit(this.model);

              this.toastrService.success('Успешно приключване на информацията!')
            })
        }
      });
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
            .subscribe((model: ApplicationTwoDto) => {
              this.model = model;
              this.modelChange.emit(this.model);

              this.toastrService.success('Успешно стартиране на редакция!');

              this.isEditMode = true;
            })
        }
      })
  }

  saveModification() {
    this.loadingIndicator.show();

    this.resource.saveModification(this.model)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(() => {
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

  cancelModification(dto: ApplicationTwoDto) {
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
}
