import { Injectable } from "@angular/core";
import { BaseResource } from "../../../infrastructure/services/base.resource";
import { HttpClient } from "@angular/common/http";
import { Configuration } from "../../../infrastructure/configuration/configuration";
import { Observable } from "rxjs";
import { AnnualAdministrativeExpensesDto } from "../dtos/annual-administrative-expenses.dto";
import { AnnualAdministrativeExpensesHistorySearchResultDto } from "../dtos/annual-administrative-expenses-history-search-result.dto";

@Injectable()
export class AnnualAdministrativeExpensesResource extends BaseResource {
  constructor(
    protected override http: HttpClient,
    protected override configuration: Configuration
  ) {
    super(http, configuration, 'AnnualAdministrativeExpenses');
  }

  getHistory(rootId: number): Observable<AnnualAdministrativeExpensesHistorySearchResultDto> {
    return this.http.get<AnnualAdministrativeExpensesHistorySearchResultDto>(`${this.baseUrl}/${rootId}`);
  }

  add(model: AnnualAdministrativeExpensesDto): Observable<AnnualAdministrativeExpensesDto> {
    return this.http.post<AnnualAdministrativeExpensesDto>(`${this.baseUrl}`, model);
  }

  update(model: AnnualAdministrativeExpensesDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}`, model);
  }
}