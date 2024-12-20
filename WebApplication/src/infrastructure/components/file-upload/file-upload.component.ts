import { Component, ElementRef, Input, ViewChild, forwardRef } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Configuration } from '../../configuration/configuration';
import { FileUploadService } from '../../services/file-upload.service';
import { catchError, throwError } from 'rxjs';
import { HttpEvent, HttpEventType } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { SvgIconComponent } from '../svg-icon/svg-icon.component';
import { ZopoeshtAttachedFile } from '../../models/zopoesht-attached-file';

@Component({
  selector: 'file-upload',
  standalone: true,
  imports: [FormsModule, TranslateModule, CommonModule, SvgIconComponent],
  providers: [
    FileUploadService,
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FileUploadComponent),
      multi: true
    }
  ],
  styleUrl: './file-upload.component.css',
  templateUrl: './file-upload.component.html',
})
export class FileUploadComponent implements ControlValueAccessor {

  documentFormats: string[] = [
    'application/pdf',
    'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
    'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
  ];

  constructor(
    private configuration: Configuration,
    private service: FileUploadService,
    private toastrService: ToastrService,
  ) {
  }

  @Input() fileFormat = '';
  @Input() disabled = false;
  @Input() required = false;
  @Input() showFileUrl = false;
  @Input() fileStorageChildUrld = 'FilesStorage';
  @Input() btnClass = 'btn-primary btn-sm';
  @Input() btnTimeClass = 'btn-light btn-sm';
  @Input() formControlClass = 'form-control form-control-sm';
  @Input() inputGroupClass = 'input-group input-group-sm';
  @Input() hideNoFileUpload = false;
  @Input() isDocumentFile = true;
  @ViewChild('fileUploadInput') fileUploadInput: ElementRef;

  // Use this when file upload should accept only one format
  @Input() acceptedFormat = '';

  model: ZopoeshtAttachedFile | null;
  fileUrl: string;

  private fileStorageUrl: any;

  ngOnInit(): void {
    this.fileStorageUrl = `${this.configuration.restUrl}/${this.fileStorageChildUrld}`;

    if (this.acceptedFormat.length > 0) {
      this.documentFormats.push(this.acceptedFormat);
    }
  }

  propagateChange = (_: any) => { };
  propagateTouched = () => { };

  writeValue(obj: any): void {
    this.model = obj;
    this.setFileUrl();
  }

  registerOnChange(fn: any): void {
    this.propagateChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.propagateTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  getFile() {
    return this.service.getFile(this.fileUrl, this.model.mimeType)
      .subscribe((blob: Blob) => {
        var url = window.URL.createObjectURL(blob);
        window.open(url);
      });
  }

  uploadFileFromInput(event: Event) {
    event.stopPropagation();
    if (this.fileUploadInput) {
      this.fileUploadInput.nativeElement.click();
    }
  }

  uploadFile(event: any): void {
    if (this.disabled) {
      return;
    }

    const target = event.target || event.srcElement;
    const files = target.files;

    if (files.length === 1) {

      let file = files[0];

      if (this.isDocumentFile && !this.documentFormats.includes(file.type)) {
        this.toastrService.error("Грешен формат на файл. Системата не позволява прикачването на файл с това разширение. Позволените разщирения са: '.pdf', '.docx', '.xlsx'");
      } else {
        this.service.uploadFile(file, this.fileStorageUrl)
          .pipe(
            catchError(() => {
              return throwError(() => new Error('Error'));
            })
          )
          .subscribe((resultEvent: HttpEvent<ZopoeshtAttachedFile>) => {
            if (resultEvent.type === HttpEventType.Response) {
              this.markAsUploaded(files[0], resultEvent.body);
            }
          });
      }
    }
  }

  private markAsUploaded(file: File, additionalInfo: ZopoeshtAttachedFile): void {
    if (!this.model) {
      this.model = new ZopoeshtAttachedFile();
    }

    this.model.name = file.name;
    this.model.mimeType = file.type;
    this.model.size = file.size;
    this.model.key = additionalInfo.key || (additionalInfo as any).fileKey;
    this.model.hash = additionalInfo.hash;
    this.model.dbId = additionalInfo.dbId;

    this.setFileUrl();
    this.propagateChange(this.model);
  }

  private setFileUrl(): void {
    if (!this.model) {
      return;
    }

    this.fileUrl = `${this.configuration.restUrl}/FilesStorage?key=${this.model.key}&fileName=${this.model.name}&dbId=${this.model.dbId}`;
  }
}
