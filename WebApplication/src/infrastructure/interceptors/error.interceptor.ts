import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { EMPTY, Observable, throwError as observableThrowError, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LoginService } from '../../modules/user/services/login.service';
import { LoadingIndicatorService } from '../components/loading-indicator/services/loading-indicator.service';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private router: Router,
    private loginService: LoginService,
    private loadingIndicatorService: LoadingIndicatorService,
    private toastr: ToastrService,
    private translateService: TranslateService
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(req).pipe(
      catchError((err: any) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status === 401) {
            this.loginService.logout();
            this.router.navigate(['login']);

            this.loadingIndicatorService.hide();

            return EMPTY;
          }
          if (err.status === 403) {
            this.toastr.error(this.translateService.instant('toastr.forbidden'));
          } else if (err.status === 422) {

            const errorAlias = 'toastrError.' + err.error?.errorMessages[0]?.domainErrorCode;
            if (errorAlias === "toastrError.ImportXmlError") {
              this.showError(errorAlias, 15000);
            } else {
              this.showError(errorAlias);
            }
          }
        }

        return throwError(() => err);
      }));
  }

  private showError(errorAlias: string, timeout?: number) {
    this.toastr.error(this.translateService.instant(errorAlias), null, { timeOut: (timeout === null || timeout === undefined) ? 5000 : timeout });
  }
}
