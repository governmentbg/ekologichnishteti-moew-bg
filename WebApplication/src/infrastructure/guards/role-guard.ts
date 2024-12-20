import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Configuration } from '../configuration/configuration';
import { UserContextService } from '../../modules/user/services/user-context.service';
import { RoleService } from '../services/role.service';

@Injectable()
export class RoleGuard {
  constructor(
    private router: Router,
    private config: Configuration,
    private toastrService: ToastrService,
    private userContextService: UserContextService,
    private roleService: RoleService
  ) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const roles = route.data['roles'] as Array<string>;

    let hasAccess: boolean = false;

    roles.forEach(role => {
      if (this.roleService.hasRole(role)) {
        hasAccess = true;
      }
    });

    if (!hasAccess) {
      this.router.navigate(['']);
      this.toastrService.error('Нямате достъп до тази страница!');
    }
    return hasAccess;
  }

}
