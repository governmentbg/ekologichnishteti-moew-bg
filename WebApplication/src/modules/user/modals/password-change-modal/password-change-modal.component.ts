import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { RegExps } from '../../../../infrastructure/constants/constants';
import { UserPasswordDto } from '../../models/user-password.dto';
import { UserResource } from '../../resources/user.resource';
import { ToastrService } from 'ngx-toastr';
import { HelpTooltipComponent } from '../../../../infrastructure/components/help-tooltip/help-tooltip.component';

@Component({
  selector: 'app-password-change-modal',
  standalone: true,
  templateUrl: './password-change-modal.component.html',
  imports: [
    TranslateModule,
    CommonModule,
    FormsModule,
    HelpTooltipComponent
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PasswordChangeModalComponent {
  @ViewChild('resultForm') resultForm: NgForm;

  passwordRegex = RegExps.PASSWORD_REGEX;

  @Input() model: UserPasswordDto;

  @Input() confirmationMessage: string;

  arePasswordsMatching: boolean = false;

  constructor(
    private resource: UserResource,
    private toastrService: ToastrService,
    public modal: NgbActiveModal
  ) { }

  arePasswordsEqual(): boolean {
    if ((!this.model.newPassword || this.model.newPassword.length === 0)
      && (!this.model.newPasswordConfirmation || this.model.newPasswordConfirmation.length === 0)) {
      this.arePasswordsMatching = false;
      return;
    }

    this.arePasswordsMatching = this.model.newPassword === this.model.newPasswordConfirmation;
    return this.arePasswordsMatching;
  }

  submit() {
    this.resource.changePassword(this.model).subscribe(() => {
      this.modal.close(true);
      this.toastrService.success('Успешно променена парола!');
    });
  }
}
