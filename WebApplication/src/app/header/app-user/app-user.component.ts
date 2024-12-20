import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Configuration } from '../../../infrastructure/configuration/configuration';
import { UserRoleAliases } from '../../../infrastructure/constants/constants';
import { RoleService } from '../../../infrastructure/services/role.service';
import { LoginService } from '../../../modules/user/services/login.service';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { ActionConfirmationModalComponent } from '../../../infrastructure/components/action-confirmation-modal/action-confirmation-modal.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [TranslateModule, CommonModule],
  templateUrl: './app-user.component.html',
  styleUrl: './app-user.component.css'
})
export class AppUserComponent implements OnInit {
  fullname: string;

  isAdmin: boolean = false;
  isMosv: boolean = false;
  isAuthorizedEntityActive: boolean = false;

  constructor(
    private roleService: RoleService,
    private configuration: Configuration,
    private loginService: LoginService,
    private router: Router,
    private modal: NgbModal,
    config: NgbModalConfig
  ) {
    config.backdrop = 'static';
    config.keyboard = false;
    this.isAdmin = this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR);
    this.isMosv = this.roleService.hasRole(UserRoleAliases.MOSV);
    this.isAuthorizedEntityActive = this.roleService.hasRole(UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE);
  }

  ngOnInit(): void {
    this.fullname = localStorage.getItem(this.configuration.fullnameProperty);
  }

  logout(): void {
    const confirmationModal = this.modal.open(ActionConfirmationModalComponent, { backdrop: 'static' });
    const confirmationMessage = "Сигурни ли сте, че искате да излезете от системата?";
    confirmationModal.componentInstance.confirmationMessage = confirmationMessage;
    confirmationModal.result
      .then((result: boolean) => {
        if (result) {
          this.loginService.logout();
          this.router.navigate(['login']);
        }
      });
  }

  routeTo(route: string) {
    this.router.navigate([route]);
  }
}
