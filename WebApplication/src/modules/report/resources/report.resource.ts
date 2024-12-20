import { HttpClient } from "@angular/common/http";
import { BaseResource } from "../../../infrastructure/services/base.resource";
import { Configuration } from "../../../infrastructure/configuration/configuration";
import { Injectable } from "@angular/core";
import { IFilterable } from "../../../infrastructure/interfaces/filterable.interface";
import { ReportSearchFilter } from "../services/report-search.filter";
import { SearchResultItemDto } from "../../../infrastructure/models/search-result-item.dto";
import { ReportSearchResultDto } from "../models/report-search-result.dto";
import { Observable } from "rxjs";

type NewType = ReportSearchFilter;

@Injectable()
export class ReportResource extends BaseResource implements IFilterable<ReportSearchFilter, SearchResultItemDto<ReportSearchResultDto>> {

  constructor(
    protected override http: HttpClient,
    protected override configuration: Configuration
  ) {
    super(http, configuration, 'Report');
  }

  getFiltered(filter?: NewType): Observable<SearchResultItemDto<ReportSearchResultDto>> {
    return this.http.post<SearchResultItemDto<ReportSearchResultDto>>(`${this.baseUrl}`, filter);
  }
}