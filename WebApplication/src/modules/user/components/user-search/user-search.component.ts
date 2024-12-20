import { Component } from "@angular/core";
import { BaseSearchComponent } from "../../../../infrastructure/components/search-component/base-search.component";
import { UserSearchResultDto } from "../../models/user-search-result.dto";
import { UserSearchFilter } from "../../services/user-search.filter";
import { UserResource } from "../../resources/user.resource";
import { LoadingIndicatorService } from "../../../../infrastructure/components/loading-indicator/services/loading-indicator.service";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { TranslateModule } from "@ngx-translate/core";
import { SvgIconComponent } from "../../../../infrastructure/components/svg-icon/svg-icon.component";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { UserStatusLocalization } from "../../../../infrastructure/constants/enum-localization.const";
import { UserStatus } from "../../enums/user-status.enum";
import { Router } from "@angular/router";
import { UserRoleAliases } from "../../../../infrastructure/constants/constants";

@Component({
  selector: 'user-search',
  standalone: true,
  templateUrl: 'user-search.component.html',
  styleUrls: [],
  imports: [
    FormsModule,
    CommonModule,
    TranslateModule,
    SvgIconComponent,
    NgbModule,
  ],
  providers: [
    UserResource,
    UserSearchFilter
  ]
})

export class UserSearchComponent extends BaseSearchComponent<UserSearchResultDto> {
  userStatusLocalization = UserStatusLocalization;
  userStatuses = UserStatus;
  mosv = UserRoleAliases.MOSV;
  administrator = UserRoleAliases.ADMINISTRATOR;

  constructor(
    public override filter: UserSearchFilter,
    protected override resource: UserResource,
    protected override loadingIndicator: LoadingIndicatorService,
    private router: Router
  ) {
    super(filter, resource, loadingIndicator)
  }

  routeToNew() {
    this.router.navigate(['/user/new']);
  }

  routeToView(id: number) {
    this.router.navigate(['/user/view', id]);
  }
}