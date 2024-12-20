import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { SvgIconComponent } from '../../../../../infrastructure/components/svg-icon/svg-icon.component';
import { FileUploadComponent } from '../../../../../infrastructure/components/file-upload/file-upload.component';
import { CommonFormComponent } from '../../../../../infrastructure/components/common-form/common-form.component';
import { ApplicationFileDto } from '../../models/application-file.dto';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ActionConfirmationModalComponent } from '../../../../../infrastructure/components/action-confirmation-modal/action-confirmation-modal.component';

@Component({
  selector: 'app-application-file-form',
  standalone: true,
  imports: [
    FormsModule,
    TranslateModule,
    CommonModule,
    SvgIconComponent,
    FileUploadComponent
  ],
  templateUrl: './application-file-form.component.html'
})
export class ApplicationFileFormComponent<TDto> extends CommonFormComponent<TDto> {
  @Input() isViewMode: boolean;
  @Input() isEditMode: boolean;

  @Input() isFileFormDisplayed: boolean;
  @Output() isFileFormDisplayedChange = new EventEmitter<boolean>();

  @Input() files: ApplicationFileDto[];
  @Output() filesChange = new EventEmitter<ApplicationFileDto[]>();

  canAddFile: boolean = true;

  constructor(
    private cdr: ChangeDetectorRef,
    private modal: NgbModal
  ) {
    super();
  }

  ngOnInit(): void {
    if (this.files.length === 0) {
      this.add();
    }
  }

  validateFiles(): void {
    for (let index = 0; index < this.files.length; index++) {
      if (this.files[index].description === '' ||
        this.files[index].description === undefined ||
        this.files[index].zopoeshtAttachedFile === undefined) {
        this.canAddFile = false;
        return;
      }
    }

    this.canAddFile = true;
  }

  add() {
    this.files.push(new ApplicationFileDto());
    this.canAddFile = false;
  }

  remove(index: number) {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    confirmationModal.componentInstance.confirmationMessage = 'Сигурни ли сте, че искате премахнете файла?';
    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.files.splice(index, 1);
          this.validateFiles();

          if (this.files.length === 0) {
            this.isFileFormDisplayed = false;
            this.isFileFormDisplayedChange.emit(this.isFileFormDisplayed);
            this.isValidForm.emit('VALID');
          }
        }
      });
  }

  ngDoCheck() {
    this.cdr.detectChanges();
  }
}