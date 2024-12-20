import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-action-confirmation-modal',
  standalone: true,
  templateUrl: './action-confirmation-modal.component.html',
  imports: [TranslateModule, CommonModule, FormsModule],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ActionConfirmationModalComponent {
  @ViewChild('resultForm') resultForm: NgForm;

  description: string = "";
  @Input() showTextArea: boolean = false;

  @Input() confirmationMessage: string;

  @Input() textAreaTitle: string = "Допълнителна информация";

  @Input() requireTextArea: boolean = false;

  @Output() passDescription: EventEmitter<string> = new EventEmitter();

  constructor(public modal: NgbActiveModal) { }

  closeModal() {
    this.passDescription.emit(this.description);
    this.modal.close(true);
  }
}
