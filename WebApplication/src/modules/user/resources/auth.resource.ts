import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Configuration } from '../../../infrastructure/configuration/configuration';
import { BaseResource } from '../../../infrastructure/services/base.resource';
import { Observable } from 'rxjs';
import { LoginDto } from '../models/login.dto';
import { LoginInfoDto } from '../models/login-info.dto';

@Injectable()
export class AuthResource extends BaseResource {
  constructor(
    protected http: HttpClient,
    protected configuration: Configuration
  ) {
    super(http, configuration, 'Auth');
  }

  login(model: LoginDto): Observable<LoginInfoDto> {
    return this.http.post<LoginInfoDto>(`${this.baseUrl}`, model);
  }
}
