import { HttpClient } from "@angular/common/http";
import { BaseResource } from "../../../infrastructure/services/base.resource";
import { Configuration } from "../../../infrastructure/configuration/configuration";
import { Injectable } from "@angular/core";
import { SummaryReportDto } from "../models/summary/summary-result-dto";
import { Observable } from "rxjs/internal/Observable";
import { SummaryFilterDto } from "../services/summary.filter";

@Injectable()
export class SummaryResource extends BaseResource {

  constructor(
    protected override http: HttpClient,
    protected override configuration: Configuration
  ) {
    super(http, configuration, 'Summary');
  }

  getSummary(filter: SummaryFilterDto): Observable<SummaryReportDto> {
    return this.http.post<SummaryReportDto>(`${this.baseUrl}`, filter);
  }

  exportWordFile(filter: SummaryFilterDto): Observable<Blob> {
    return this.http.post(`${this.baseUrl}/Export`, filter, { responseType: 'blob' });
  }
}