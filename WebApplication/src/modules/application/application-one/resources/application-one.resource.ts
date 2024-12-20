import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Configuration } from "../../../../infrastructure/configuration/configuration";
import { SearchResultItemDto } from "../../../../infrastructure/models/search-result-item.dto";
import { ApplicationOneSearchResultDto } from "../models/application-one-search-result.dto";
import { Observable } from "rxjs";
import { ApplicationResource } from "../../common/resoruces/application.resource";
import { ApplicationOneDto } from "../models/application-one.dto";
import { ApplicationOneSearchFilter } from "../services/application-one-search.filter";
import { IFilterable } from "../../../../infrastructure/interfaces/filterable.interface";
import { ReportSearchFilter } from "../../../report/services/report-search.filter";

type NewType = ApplicationOneSearchFilter;

@Injectable()

export class ApplicationOneResource extends ApplicationResource<ApplicationOneDto> implements IFilterable<ApplicationOneSearchFilter, SearchResultItemDto<ApplicationOneSearchResultDto>> {

  constructor(
    protected override http: HttpClient,
    protected override configuration: Configuration
  ) {
    super(http, configuration, 'ApplicationOne');
  }

  getFiltered(filter?: NewType): Observable<SearchResultItemDto<ApplicationOneSearchResultDto>> {
    return this.http.post<SearchResultItemDto<ApplicationOneSearchResultDto>>(`${this.baseUrl}/Filter`, filter);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  restore(id: number): Observable<ApplicationOneDto> {
    return this.http.put<ApplicationOneDto>(`${this.baseUrl}`, id);
  }

  getIdByApplicationTwoId(appTwoId: number): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}/GetId?appTwoId=${appTwoId}`);
  }

  exportExcelFile(filter?: ReportSearchFilter): Observable<Blob> {
    return this.http.post(`${this.baseUrl}/Export`, filter, { responseType: 'blob' });
  }

  cancelModification(dto: ApplicationOneDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/CancelModification`, dto);
  }
}