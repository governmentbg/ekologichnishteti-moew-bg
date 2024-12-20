import { ChangeDetectorRef, Component } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonModule, DatePipe } from "@angular/common";
import { TranslateModule } from "@ngx-translate/core";
import { SvgIconComponent } from "../../../../infrastructure/components/svg-icon/svg-icon.component";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { PeriodDto } from "../../dtos/period.dto";
import { LoadingIndicatorService } from "../../../../infrastructure/components/loading-indicator/services/loading-indicator.service";
import { PeriodResource } from "../../resources/period.resource";
import { catchError, finalize, throwError } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { CollapsableLabelComponent } from "../../../../infrastructure/components/collapsable-label/collapsable-label.component";
import { AnnualAdministrativeExpensesComponent } from "../annual-administrative-expenses/annual-administrative-expenses.component";
import { Router } from "@angular/router";
import { RoleService } from "../../../../infrastructure/services/role.service";
import { UserRoleAliases } from "../../../../infrastructure/constants/constants";

@Component({
  selector: 'periods-component',
  standalone: true,
  templateUrl: './periods.component.html',
  styleUrl: "./periods.component.css",
  imports: [
    FormsModule,
    CommonModule,
    TranslateModule,
    SvgIconComponent,
    NgbModule,
    CollapsableLabelComponent,
    AnnualAdministrativeExpensesComponent
  ],
  providers: [
    PeriodResource,
    DatePipe
  ]
})
export class PeriodsComponent {
  periods: PeriodDto[] = [];
  periodCopies: PeriodDto[];

  isAdmin: boolean = false;
  isPeriodFormDisplayed: boolean = false;

  newPeriod: PeriodDto;
  newPeriodMinDate: any;

  constructor(
    private resource: PeriodResource,
    private loadingIndicator: LoadingIndicatorService,
    private toastrService: ToastrService,
    private router: Router,
    private datePipe: DatePipe,
    private cdr: ChangeDetectorRef,
    private roleService: RoleService
  ) { }

  ngOnInit(): void {
    this.isAdmin = this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR);

    this.loadingIndicator.show();

    this.resource.getAll()
      .pipe(
        finalize(() => this.loadingIndicator.hide()),
        catchError((error: any) => {
          this.router.navigate(['/']);
          return throwError(() => new Error());
        })
      )
      .subscribe((result) => {
        this.periods = result;
      });
  }

  ngDoCheck() {
    this.cdr.detectChanges();
  }

  displayPeriodForm(): void {
    this.isPeriodFormDisplayed = true;
    this.newPeriod = new PeriodDto();
  }

  addPeriod(): void {
    this.loadingIndicator.show();

    this.resource.add(this.newPeriod)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((id: number) => {
        this.toastrService.success('Успешно добавяне на период!');

        this.newPeriod.name = this.getPeriodName(this.newPeriod.startDate, this.newPeriod.endDate);
        this.newPeriod.id = id;
        this.periods.push(this.newPeriod);

        this.cancelAddingPeriod();
      });
  }

  cancelAddingPeriod(): void {
    this.isPeriodFormDisplayed = false;
    this.newPeriod = null;
    this.newPeriodMinDate = null;
  }

  enablePeriodModification(period: PeriodDto): void {
    if (this.periodCopies === undefined ||
      this.periodCopies === null ||
      this.periodCopies?.length === 0) {
      this.copyPeriods();
    }

    period.isInEditMode = true;
  }

  cancelPeriodModification(period: PeriodDto): void {
    period.isInEditMode = false;
    let oldPeriod = this.periodCopies.find(p => p.id === period.id);
    let modifiedPeriodIndex = this.periods.findIndex(p => p.id === period.id);

    this.periods[modifiedPeriodIndex] = JSON.parse(JSON.stringify(oldPeriod));
  }

  modifyPeriod(period: PeriodDto): void {
    this.loadingIndicator.show();

    this.resource.update(period)
      .pipe(
        finalize(() => this.loadingIndicator.hide()),
        catchError((error: any) => {
          this.cancelPeriodModification(period);
          return throwError(error);
        })
      )
      .subscribe(() => {
        this.toastrService.success('Успешно редактиране на период!');

        period.name = this.getPeriodName(period.startDate, period.endDate);
        let modifiedPeriodIndex = this.periods.findIndex(p => p.id === period.id);
        this.periodCopies[modifiedPeriodIndex] = JSON.parse(JSON.stringify(period));
        this.periods[modifiedPeriodIndex].isInEditMode = false;
      });
  }

  onPeriodDateChange(i: number): void {
    if (this.periods[i].startDate !== undefined &&
      this.periods[i].startDate !== null) {
      const startDate = new Date(this.periods[i].startDate);
      this.periods[i].minDate = { year: startDate.getFullYear(), month: startDate.getMonth() + 1, day: startDate.getDate() };
    }
  }

  onNewPeriodDateChange(): void {
    if (this.newPeriod.startDate !== undefined &&
      this.newPeriod.startDate !== null) {
      const startDate = new Date(this.newPeriod.startDate);
      this.newPeriodMinDate = { year: startDate.getFullYear(), month: startDate.getMonth() + 1, day: startDate.getDate() };
    }
  }

  private copyPeriods(): void {
    if (this.periods?.length > 0) {
      this.periodCopies = JSON.parse(JSON.stringify(this.periods));
    }
  }

  private getPeriodName(start: Date, end: Date): string {
    let startString = this.datePipe.transform(start, 'dd.MM.yyyy');
    let endString = this.datePipe.transform(end, 'dd.MM.yyyy');

    return `${startString} г. - ${endString} г.`;
  }
}
