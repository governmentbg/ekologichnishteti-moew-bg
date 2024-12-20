import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './header/header.component';
import { TranslateService } from '@ngx-translate/core';
import { IMenuItem } from '../infrastructure/interfaces/IMenuItem';
import { MenuItemsService } from './header/app-menu/services/menu-item.service';
import { LoadingIndicatorComponent } from '../infrastructure/components/loading-indicator/loading-indicator.component';
import { UserContextService } from '../modules/user/services/user-context.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, LoadingIndicatorComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  menuItems: IMenuItem[] = [];
  isLoggedIn = false;

  constructor(private translate: TranslateService,
    private menuItemsService: MenuItemsService,
    private userContextService: UserContextService
  ) {
    this.translate.setDefaultLang('bg');
  }

  ngOnInit(): void {
    this.isLoggedIn = this.userContextService.userContext !== null;
    this.menuItems = this.menuItemsService.getMainMenuItems(this.isLoggedIn);

    this.userContextService.subscribe((value: { event: boolean }) => {
      this.isLoggedIn = value.event;
      this.menuItems = this.menuItemsService.getMainMenuItems(this.isLoggedIn);
    });
  }
}
