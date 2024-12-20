import { APP_INITIALIZER, ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter, withViewTransitions } from '@angular/router';

import { routes } from './app.routes';
import { AuthResource } from '../modules/user/resources/auth.resource';
import { HTTP_INTERCEPTORS, HttpClient, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { Configuration, configurationFactory } from '../infrastructure/configuration/configuration';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { LoadingIndicatorService } from '../infrastructure/components/loading-indicator/services/loading-indicator.service';
import { AuthInterceptor } from '../infrastructure/interceptors/auth.interceptor';
import { ErrorInterceptor } from '../infrastructure/interceptors/error.interceptor';
import { AuthGuard } from '../infrastructure/guards/auth.guard';
import { RoleService } from '../infrastructure/services/role.service';
import { NomenclatureResource } from '../modules/nomenclature/common/resources/nomenclature.resource';
import { ToastrModule } from 'ngx-toastr';
import { provideAnimations } from '@angular/platform-browser/animations';
import { NgbDateAdapter, NgbDateParserFormatter, NgbDatepickerI18n } from '@ng-bootstrap/ng-bootstrap';
import { StringDateAdapter } from '../infrastructure/services/string-date.adapter';
import { MomentDateFormatter } from '../infrastructure/services/moment-date.formatter';
import { CustomDatepickerI18n } from '../infrastructure/services/datepicker-i18n.service';
import { SharedService } from '../infrastructure/services/shared-service';
import { UserResource } from '../modules/user/resources/user.resource';
import { RoleGuard } from '../infrastructure/guards/role-guard';
import { UserContextResource } from '../modules/user/resources/user-context.resource';
import { LoggedInGuard } from '../infrastructure/guards/logged-in.guard';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes, withViewTransitions()),
    provideHttpClient(withInterceptorsFromDi()),
    provideAnimations(),
    {
      provide: APP_INITIALIZER,
      useFactory: configurationFactory,
      deps: [Configuration],
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    },
    { provide: NgbDateAdapter, useClass: StringDateAdapter },
    { provide: NgbDateParserFormatter, useClass: MomentDateFormatter },
    { provide: NgbDatepickerI18n, useClass: CustomDatepickerI18n },
    importProvidersFrom(
      TranslateModule.forRoot({
        loader: {
          provide: TranslateLoader,
          useFactory: HttpLoaderFactory,
          deps: [HttpClient]
        }
      }),
      ToastrModule.forRoot(),
    ),
    Configuration,
    AuthGuard,
    RoleGuard,
    LoggedInGuard,
    AuthResource,
    NomenclatureResource,
    UserContextResource,
    RoleService,
    LoadingIndicatorService,
    SharedService,
    UserResource
  ]
};

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
