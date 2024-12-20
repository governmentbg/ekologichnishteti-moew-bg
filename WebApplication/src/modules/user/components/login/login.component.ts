import { Component } from "@angular/core";
import { LoginDto } from "../../models/login.dto";
import { AuthResource } from "../../resources/auth.resource";
import { FormsModule } from "@angular/forms";
import { TranslateModule } from "@ngx-translate/core";
import { Router, RouterModule } from "@angular/router";
import { finalize } from "rxjs";
import { LoginService } from "../../services/login.service";
import { LoadingIndicatorService } from "../../../../infrastructure/components/loading-indicator/services/loading-indicator.service";
import { LoginInfoDto } from "../../models/login-info.dto";
import { UserContextService } from "../../services/user-context.service";

@Component({
  selector: 'user-login',
  standalone: true,
  templateUrl: 'login.component.html',
  imports: [TranslateModule, FormsModule, RouterModule],
})
export class LoginComponent {
  model: LoginDto = new LoginDto();

  constructor(
    private authResource: AuthResource,
    private router: Router,
    private loginService: LoginService,
    private userContextService: UserContextService,
    private loadingIndicator: LoadingIndicatorService
  ) {
  }

  login() {
    this.loadingIndicator.show();
    this.authResource.login(this.model)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(
        (loginInfoDto: LoginInfoDto) => {
          this.loginService.login(loginInfoDto);
          this.getUserInfo();
        }
      );
  }

  getUserInfo() {
    this.userContextService.getUserInfo().subscribe(() => this.router.navigate(['']));
  }
}