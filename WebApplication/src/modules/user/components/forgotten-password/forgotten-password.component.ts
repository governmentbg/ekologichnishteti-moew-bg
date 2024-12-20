import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { Router, RouterModule } from '@angular/router';
import { ForgottenPasswordDto } from '../../models/forgotten-password.dto';
import { ForgottenPasswordResource } from '../../resources/forgotten-password.resource';
import { CommonFormComponent } from '../../../../infrastructure/components/common-form/common-form.component';
import { LoadingIndicatorService } from '../../../../infrastructure/components/loading-indicator/services/loading-indicator.service';
import { finalize } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { RegExps } from '../../../../infrastructure/constants/constants';

@Component({
  selector: 'forgotten-password',
  standalone: true,
  templateUrl: 'forgotten-password.component.html',
  imports: [TranslateModule, FormsModule, RouterModule],
  providers: [ForgottenPasswordResource],
})
export class ForgottenPasswordComponent extends CommonFormComponent<ForgottenPasswordDto> {
  model: ForgottenPasswordDto = new ForgottenPasswordDto();

  emailRegex = RegExps.EMAIL_REGEX;

  constructor(
    private resource: ForgottenPasswordResource,
    private loadingIndicator: LoadingIndicatorService,
    private toastrService: ToastrService,
    private translateService: TranslateService,
    private router: Router
  ) {
    super();
  }

  sendForgottenPassword() {
    this.loadingIndicator.show();

    this.resource.sendForgottenPassword(this.model)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(() => {
        this.toastrService.success(this.translateService.instant('Успешно изпратен имейл за възстановяване на парола!'));
        this.router.navigate(['/login']);
      });
  }
}