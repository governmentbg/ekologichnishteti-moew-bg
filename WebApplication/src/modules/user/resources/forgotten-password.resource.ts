import { Injectable } from '@angular/core';
import { BaseResource } from '../../../infrastructure/services/base.resource';
import { HttpClient } from '@angular/common/http';
import { Configuration } from '../../../infrastructure/configuration/configuration';
import { Observable } from 'rxjs';
import { ForgottenPasswordDto } from '../models/forgotten-password.dto';

@Injectable()
export class ForgottenPasswordResource extends BaseResource {
  constructor(
    protected http: HttpClient,
    protected configuration: Configuration
  ) {
    super(http, configuration, 'ForgottenPassword');
  }

  sendForgottenPassword(model: ForgottenPasswordDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}`, model);
  }
}
