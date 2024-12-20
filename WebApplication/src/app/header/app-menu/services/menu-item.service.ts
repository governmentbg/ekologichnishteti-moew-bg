import { Injectable } from '@angular/core';
import { IMenuItem } from '../../../../infrastructure/interfaces/IMenuItem';
import { UserRoleAliases } from '../../../../infrastructure/constants/constants';
import { RoleService } from '../../../../infrastructure/services/role.service';

@Injectable({
  providedIn: 'root'
})
export class MenuItemsService {
  isAdmin: boolean = false;
  isMosv: boolean = false;
  isAuthorizedActive: boolean = false;

  constructor(private roleService: RoleService
  ) { }

  getMainMenuItems(isLoggedInUser: boolean): IMenuItem[] {
    if (!isLoggedInUser) {
      return [];
    }

    this.isAdmin = this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR);
    this.isMosv = this.roleService.hasRole(UserRoleAliases.MOSV);
    this.isAuthorizedActive = this.roleService.hasRole(UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE);

    return [
      {
        title: 'Случаи', icon: 'file-earmark-fill', isVisible: true,
        children: [
          { title: 'Търсене', icon: 'search', routerLink: '/application/search', isVisible: true },
          { title: 'Нов случай', icon: 'plus-circle', routerLink: '/application/new', isVisible: this.isAdmin || this.isMosv || this.isAuthorizedActive }
        ]
      },
      {
        title: 'Годишни административни разходи', icon: 'calendar', routerLink: '/administrativeExpenses', isVisible: this.isAdmin || this.isMosv || this.isAuthorizedActive
      },
      {
        title: 'Номенклатури', icon: 'collection-fill', routerLink: '/nomenclature', isVisible: this.isAdmin || this.isMosv || this.isAuthorizedActive
      },
      {
        title: 'Справки', icon: 'bar-chart-fill', isVisible: this.isAdmin || this.isMosv || this.isAuthorizedActive,
        children: [
          { title: 'Случаи', icon: 'file-earmark-bar-graph', routerLink: '/reports', isVisible: true },
          { title: 'Доклад', icon: 'file-earmark-text', routerLink: '/summary', isVisible: this.isAdmin }
        ]
      }
    ];
  }
}