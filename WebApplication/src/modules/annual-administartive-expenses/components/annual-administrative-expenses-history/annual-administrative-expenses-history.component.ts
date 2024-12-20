import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { TranslateModule } from "@ngx-translate/core";
import { AnnualAdministrativeExpensesResource } from "../../resources/annual-administrative-expenses.resource";
import { LoadingIndicatorService } from "../../../../infrastructure/components/loading-indicator/services/loading-indicator.service";
import { ActivatedRoute, Router, RouterModule } from "@angular/router";
import { catchError, finalize, throwError } from "rxjs";
import { AnnualAdministrativeExpensesHistoryDto } from "../../dtos/annual-administrative-expenses-history.dto";
import { AnnualAdministrativeExpensesHistorySearchResultDto } from "../../dtos/annual-administrative-expenses-history-search-result.dto";

@Component({
  selector: 'annual-administrative-expenses-history-component',
  standalone: true,
  templateUrl: './annual-administrative-expenses-history.component.html',
  imports: [
    CommonModule,
    TranslateModule,
    RouterModule
  ],
  providers: [
    AnnualAdministrativeExpensesResource
  ]
})

export class AnnualAdministrativeExpensesHistoryComponent {
  authorityName: string;
  periodName: string;
  histories: AnnualAdministrativeExpensesHistoryDto[] = [];

  constructor(
    private resource: AnnualAdministrativeExpensesResource,
    private loadingIndicator: LoadingIndicatorService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    const rootId = +this.activatedRoute.snapshot.params.rootId;

    this.loadingIndicator.show();
    this.resource.getHistory(rootId)
      .pipe(
        finalize(() => this.loadingIndicator.hide()),
        catchError((error: any) => {
          this.router.navigate(['/administrativeExpenses']);
          return throwError(() => new Error());
        })
      )
      .subscribe((result: AnnualAdministrativeExpensesHistorySearchResultDto) => {
        this.authorityName = result.authorityName;
        this.periodName = result.periodName;
        this.histories = result.histories;
      });
  }
}