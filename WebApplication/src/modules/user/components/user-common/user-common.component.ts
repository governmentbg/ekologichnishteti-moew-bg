import { CommonModule } from "@angular/common";
import { ChangeDetectorRef, Component, Input } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TranslateModule } from "@ngx-translate/core";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { UserDto } from "../../models/user.dto";
import { UserStatusLocalization } from "../../../../infrastructure/constants/enum-localization.const";
import { UserStatus } from "../../enums/user-status.enum";
import { AsyncSelectComponent } from "../../../../infrastructure/components/async-select/async-select.component";
import { RegExps, UserRoleAliases } from "../../../../infrastructure/constants/constants";
import { CommonFormComponent } from "../../../../infrastructure/components/common-form/common-form.component";
import { SharedService } from "../../../../infrastructure/services/shared-service";
import { HelpTooltipComponent } from "../../../../infrastructure/components/help-tooltip/help-tooltip.component";

@Component({
  selector: 'user-common',
  standalone: true,
  templateUrl: './user-common.component.html',
  styleUrl: './user-common.component.css',
  imports: [
    FormsModule,
    CommonModule,
    TranslateModule,
    NgbModule,
    AsyncSelectComponent,
    HelpTooltipComponent
  ],
  providers: []
})

export class UserCommonComponent extends CommonFormComponent<UserDto> {
  userStatusLocalization = UserStatusLocalization;
  userStatuses = UserStatus;

  userRoleAliases = UserRoleAliases;

  passwordRegex = RegExps.PASSWORD_REGEX;
  emailRegex = RegExps.EMAIL_REGEX;

  @Input() model: UserDto = new UserDto();
  @Input() isEditMode: boolean = false;
  @Input() isPasswordRequired: boolean = false;

  mosvName = 'МОСВ';

  constructor(private cdr: ChangeDetectorRef, public sharedService: SharedService) {
    super();
  }


  selectRole(role: any) {
    if (role.alias != this.userRoleAliases.AUTHORIZED_ENTITY_ACTIVE) {
      this.model.authority = null;
    }
  }
}