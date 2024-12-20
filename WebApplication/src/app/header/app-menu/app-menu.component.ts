import { Component, Input } from '@angular/core';
import { IMenuItem } from '../../../infrastructure/interfaces/IMenuItem';
import { Router, RouterModule } from '@angular/router';
import { AppUserComponent } from '../app-user/app-user.component';
import { CommonModule } from '@angular/common';
import { SvgIconComponent } from '../../../infrastructure/components/svg-icon/svg-icon.component';
import { FilterPipe } from '../../../infrastructure/pipes/filter.pipe';
import { NgbDropdown, NgbDropdownMenu, NgbDropdownToggle } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [AppUserComponent, CommonModule, RouterModule, SvgIconComponent, FilterPipe, NgbDropdown, NgbDropdownMenu, NgbDropdownToggle],
  templateUrl: './app-menu.component.html',
  styleUrl: './app-menu.component.css'
})
export class AppMenuComponent {
  @Input() menuItems: IMenuItem[] = [];
  filePath: string;
  isCollapsed = false;

  constructor(
    public router: Router
  ) { }
}