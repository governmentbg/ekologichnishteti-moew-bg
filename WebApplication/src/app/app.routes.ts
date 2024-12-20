import { Routes } from '@angular/router';
import { LoginComponent } from '../modules/user/components/login/login.component';
import { AuthGuard } from '../infrastructure/guards/auth.guard';
import { ApplicationSearchComponent } from '../modules/application/common/components/application-search/application-search.component';
import { NomenclatureHostComponent } from '../modules/nomenclature/components/nomenclature-host/nomenclature-host.component';
import { SectionComponent } from '../modules/nomenclature/components/section.component';
import { GroupComponent } from '../modules/nomenclature/components/group.component';
import { ActTypeComponent } from '../modules/nomenclature/components/act-type.component';
import { AffectedTypeComponent } from '../modules/nomenclature/components/affected-type.component';
import { AuthorityComponent } from '../modules/nomenclature/components/authority.component';
import { CodeComponent } from '../modules/nomenclature/components/code/code.component';
import { ActivityComponent } from '../modules/nomenclature/components/activity.component';
import { ExpenseTypeComponent } from '../modules/nomenclature/components/expense-type.component';
import { InsuranceComponent } from '../modules/nomenclature/components/insurance.component';
import { MeasureTypeComponent } from '../modules/nomenclature/components/measure-type.component';
import { SectorComponent } from '../modules/nomenclature/components/sector.component';
import { ForgottenPasswordComponent } from '../modules/user/components/forgotten-password/forgotten-password.component';
import { ApplicationOneNewComponent } from '../modules/application/application-one/components/application-one-new/application-one-new.component';
import { UserSearchComponent } from '../modules/user/components/user-search/user-search.component';
import { UserNewComponent } from '../modules/user/components/user-new/user-new.component';
import { UserViewComponent } from '../modules/user/components/user-view/user-view.component';
import { ApplicationOneHistoryComponent } from '../modules/application/application-one/components/application-one-history/application-one-history.component';
import { OperatorSearchComponent } from '../modules/operator/components/operator-search/operator-search.component';
import { OperatorNewComponent } from '../modules/operator/components/operator-new/operator-new.component';
import { ApplicationViewComponent } from '../modules/application/common/components/application-view/application-view.component';
import { ApplicationTwoHistoryComponent } from '../modules/application/application-two/components/application-two-history/application-two-history.component';
import { ApplicationTwoViewComponent } from '../modules/application/application-two/components/application-two-view/application-two-view.component';
import { CountryComponent } from '../modules/nomenclature/components/country.component';
import { RoleGuard } from '../infrastructure/guards/role-guard';
import { UserRoleAliases } from '../infrastructure/constants/constants';
import { ReportSearchComponent } from '../modules/report/components/report-search/report-search.component';
import { SummaryComponent } from '../modules/report/components/summary/summary.component';
import { UserProfileComponent } from '../modules/user/components/user-profile/user-profile.component';
import { YearComponent } from '../modules/nomenclature/components/year.component';
import { AdministrativeExpensesComponent } from '../modules/annual-administartive-expenses/components/adminstrative-expenses-common/administrative-expenses.component';
import { AnnualAdministrativeExpensesHistoryComponent } from '../modules/annual-administartive-expenses/components/annual-administrative-expenses-history/annual-administrative-expenses-history.component';
import { OperatorViewComponent } from '../modules/operator/components/operator-view/operator-view.component';
import { SubActivityComponent } from '../modules/nomenclature/components/subactivity/subactivity.component';
import { LoggedInGuard } from '../infrastructure/guards/logged-in.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'application/search',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginComponent,
    pathMatch: 'full',
    canActivate: [LoggedInGuard]
  },
  {
    path: 'forgottenPassword',
    component: ForgottenPasswordComponent,
    pathMatch: 'full',
    canActivate: [LoggedInGuard]
  },
  {
    path: 'user',
    children: [
      {
        path: 'search',
        component: UserSearchComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [UserRoleAliases.ADMINISTRATOR] }
      },
      {
        path: "new",
        component: UserNewComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [UserRoleAliases.ADMINISTRATOR] }
      },
      {
        path: "view/:id",
        component: UserViewComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [UserRoleAliases.ADMINISTRATOR] }
      },
      {
        path: "profile",
        component: UserProfileComponent,
        canActivate: [AuthGuard]
      }
    ]
  },
  {
    path: 'operator',
    children: [
      {
        path: 'search',
        component: OperatorSearchComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE] }
      },
      {
        path: "new",
        component: OperatorNewComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE] }
      },
      {
        path: "view/:id",
        component: OperatorViewComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE] }
      }
    ]
  },
  {
    path: 'application',
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE, UserRoleAliases.AUTHORIZED_ENTITY_PASSIVE] },
    children: [
      {
        path: '',
        redirectTo: 'search',
        pathMatch: 'full'
      },
      {
        path: 'search',
        component: ApplicationSearchComponent
      },
      {
        path: 'new',
        component: ApplicationOneNewComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE] }
      },
      {
        path: 'one/:parentId',
        children: [
          {
            path: '',
            component: ApplicationViewComponent
          },
          {
            path: 'history/:rootId',
            component: ApplicationOneHistoryComponent,
            canActivate: [AuthGuard, RoleGuard],
            data: { roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV] }
          },
        ]
      },
      {
        path: 'two/:parentId',
        children: [
          {
            path: '',
            component: ApplicationTwoViewComponent
          },
          {
            path: 'history/:rootId',
            component: ApplicationTwoHistoryComponent,
            canActivate: [AuthGuard, RoleGuard],
            data: { roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV] }
          }
        ]
      }

    ],
  },
  {
    path: 'nomenclature',
    component: NomenclatureHostComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE] },
    children: [
      {
        path: '',
        redirectTo: 'authority',
        pathMatch: 'full',
      },
      {
        path: 'actType',
        component: ActTypeComponent,
      },
      {
        path: 'affectedType',
        component: AffectedTypeComponent,
      },
      {
        path: 'authority',
        component: AuthorityComponent,
      },
      {
        path: 'code',
        component: CodeComponent,
      },
      {
        path: 'country',
        component: CountryComponent
      },
      {
        path: 'activity',
        component: ActivityComponent,
      },
      {
        path: 'subActivity',
        component: SubActivityComponent,
      },
      {
        path: 'expenseType',
        component: ExpenseTypeComponent,
      },
      {
        path: 'group',
        component: GroupComponent,
      },
      {
        path: 'insurance',
        component: InsuranceComponent,
      },
      {
        path: 'measureType',
        component: MeasureTypeComponent,
      },
      {
        path: 'section',
        component: SectionComponent,
      },
      {
        path: 'sector',
        component: SectorComponent,
      },
      {
        path: 'year',
        component: YearComponent,
      },
    ],
  },
  {
    path: 'reports',
    component: ReportSearchComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE] },
    pathMatch: 'full'
  },
  {
    path: 'summary',
    component: SummaryComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE] },
    pathMatch: 'full'
  },
  {
    path: 'administrativeExpenses',
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE] },
    children: [
      {
        path: '',
        component: AdministrativeExpensesComponent,
        pathMatch: 'full'
      },
      {
        path: 'history/:rootId',
        component: AnnualAdministrativeExpensesHistoryComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV] },
      },
    ]
  }
];
