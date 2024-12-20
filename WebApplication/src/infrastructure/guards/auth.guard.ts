import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { LoginService } from '../../modules/user/services/login.service';
import { UserContextService } from '../../modules/user/services/user-context.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private loginService: LoginService,
    private userContextService: UserContextService
  ) { }

  canActivate(_: ActivatedRouteSnapshot, __: RouterStateSnapshot): boolean {
    const isUserLoggedIn = this.userContextService.userContext;
    if (isUserLoggedIn) {

      return true;
    }

    this.loginService.logout();
    this.router.navigate(['login']);
    return false;
  }
}
