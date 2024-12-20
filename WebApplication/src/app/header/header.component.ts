import { Component, Input } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TranslateModule } from "@ngx-translate/core";
import { IMenuItem } from "../../infrastructure/interfaces/IMenuItem";
import { CommonModule } from "@angular/common";
import { AppMenuComponent } from "./app-menu/app-menu.component";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [TranslateModule, RouterModule, CommonModule, AppMenuComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  @Input() menuItems: IMenuItem[] = [];
  @Input() isLoggedIn = false;

  constructor() {

  }
}
