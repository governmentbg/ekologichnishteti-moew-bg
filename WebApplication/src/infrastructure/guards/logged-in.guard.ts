import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { UserContextService } from '../../modules/user/services/user-context.service';

@Injectable()
export class LoggedInGuard implements CanActivate {
  constructor(
    private router: Router,
    private userContextService: UserContextService
  ) { }

  canActivate(_: ActivatedRouteSnapshot, __: RouterStateSnapshot): boolean {
    const isUserLoggedIn = this.userContextService.userContext;
    if (isUserLoggedIn) {

      this.router.navigate(['']);
      return false;;
    }

    return true;
  }
}