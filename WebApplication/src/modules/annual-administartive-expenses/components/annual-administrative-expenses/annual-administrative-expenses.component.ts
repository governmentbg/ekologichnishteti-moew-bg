import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { TranslateModule } from "@ngx-translate/core";
import { SvgIconComponent } from "../../../../infrastructure/components/svg-icon/svg-icon.component";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { AsyncSelectComponent } from "../../../../infrastructure/components/async-select/async-select.component";
import { LoadingIndicatorService } from "../../../../infrastructure/components/loading-indicator/services/loading-indicator.service";
import { ToastrService } from "ngx-toastr";
import { AnnualAdministrativeExpensesResource } from "../../resources/annual-administrative-expenses.resource";
import { AnnualAdministrativeExpensesDto } from "../../dtos/annual-administrative-expenses.dto";
import { UserRoleAliases } from "../../../../infrastructure/constants/constants";
import { RoleService } from "../../../../infrastructure/services/role.service";
import { UserContextService } from "../../../user/services/user-context.service";
import { UserResource } from "../../../user/resources/user.resource";
import { finalize } from "rxjs";
import { RouterModule } from "@angular/router";

@Component({
  selector: 'annual-administrative-expenses-component',
  standalone: true,
  templateUrl: './annual-administrative-expenses.component.html',
  styleUrl: "./annual-administrative-expenses.component.css",
  imports: [
    FormsModule,
    CommonModule,
    TranslateModule,
    SvgIconComponent,
    NgbModule,
    AsyncSelectComponent,
    RouterModule
  ],
  providers: [
    AnnualAdministrativeExpensesResource
  ]
})

export class AnnualAdministrativeExpensesComponent {
  @Input() annualAdministrativeExpenses: AnnualAdministrativeExpensesDto[] = [];
  @Input() periodId: number;

  @Output() periodHasAnnualAdministrativeExpenses: EventEmitter<boolean> = new EventEmitter();

  annualAdministrativeExpensesCopies: AnnualAdministrativeExpensesDto[];

  isAdmin: boolean = false;
  isAnnualAdministrativeExpensesFormDisplayed: boolean = false;
  canAddAnnualAdministrativeExpense: boolean = true;

  usedAuthorityIds: number[] = [];

  constructor(
    private resource: AnnualAdministrativeExpensesResource,
    private roleService: RoleService,
    private userContextService: UserContextService,
    private userResource: UserResource,
    private loadingIndicator: LoadingIndicatorService,
    private toastrService: ToastrService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.isAdmin = this.roleService.hasRole(UserRoleAliases.ADMINISTRATOR);
  }

  ngDoCheck(): void {
    this.canAddAnnualAdministrativeExpense = !this.annualAdministrativeExpenses?.some(a => a.authorityId === undefined ||
      a.amount === undefined);

    if (this.annualAdministrativeExpenses !== undefined) {
      this.usedAuthorityIds = this.annualAdministrativeExpenses
        .map(({ authorityId }) => authorityId)
        .filter(i => i !== undefined);
    }
  }

  addNewAnnualAdministrativeExpense(): void {
    if (this.annualAdministrativeExpenses === null ||
      this.annualAdministrativeExpenses === undefined) {
      this.annualAdministrativeExpenses = [];
    }

    let newAnnualAdministrativeExpense = new AnnualAdministrativeExpensesDto();
    newAnnualAdministrativeExpense.isInEditMode = true;

    if (!this.isAdmin) {
      this.userResource.getById(this.userContextService.userContext.userId).subscribe((user) => {
        newAnnualAdministrativeExpense.authorityId = user.authorityId;
        newAnnualAdministrativeExpense.authority = user.authority;
      });
    }

    this.annualAdministrativeExpenses.push(newAnnualAdministrativeExpense);
  }

  add(annualAdministrativeExpense: AnnualAdministrativeExpensesDto, index: number): void {
    this.loadingIndicator.show();

    annualAdministrativeExpense.periodId = this.periodId;

    this.resource.add(annualAdministrativeExpense)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((expenseResult: AnnualAdministrativeExpensesDto) => {
        this.toastrService.success('Успешно добавяне на годишен административен разход!');

        this.annualAdministrativeExpenses[index].isInEditMode = false;
        this.annualAdministrativeExpenses[index].id = expenseResult.id;
        this.annualAdministrativeExpenses[index].rootId = expenseResult.rootId;

        this.periodHasAnnualAdministrativeExpenses.emit(true);

        this.cdr.detectChanges();
      });
  }

  edit(index: number): void {
    let annualAdministrativeExpenseToModify = this.annualAdministrativeExpenses[index];
    annualAdministrativeExpenseToModify.isInEditMode = true;
  }

  modify(annualAdministrativeExpense: AnnualAdministrativeExpensesDto): void {
    this.loadingIndicator.show();

    annualAdministrativeExpense.periodId = this.periodId;

    this.resource.update(annualAdministrativeExpense)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(() => {
        this.toastrService.success('Успешно редактиране на годишен административен разход!');

        annualAdministrativeExpense.isInEditMode = false;
      });
  }

  remove(index: number): void {
    this.annualAdministrativeExpenses.splice(index, 1);
  }
}
