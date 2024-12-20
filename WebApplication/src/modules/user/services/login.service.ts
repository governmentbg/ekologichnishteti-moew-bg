import { Injectable } from "@angular/core";
import { Subject, Subscription } from "rxjs";
import { UserLoginEventEnum } from "../enums/user-login-event.enum";
import { LoginInfoDto } from "../models/login-info.dto";
import { UserContextService } from "./user-context.service";
import { Configuration } from "../../../infrastructure/configuration/configuration";

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private loginSubject: Subject<{ event: UserLoginEventEnum }> = new Subject();

  constructor(
    private configuration: Configuration,
    private userContextService: UserContextService
  ) {
  }
  login(userLoginInfoDto: LoginInfoDto): void {
    localStorage.setItem(this.configuration.tokenProperty, userLoginInfoDto.token);
    localStorage.setItem(this.configuration.fullnameProperty, userLoginInfoDto.fullname);

    this.loginSubject.next({ event: UserLoginEventEnum.login });
  }

  logout(): void {
    localStorage.clear();

    this.userContextService.clearUserContext();

    this.loginSubject.next({ event: UserLoginEventEnum.logout });
  }

  subscribe(next: (value: { event: UserLoginEventEnum }) => void): Subscription {
    return this.loginSubject.subscribe(next);
  }
}