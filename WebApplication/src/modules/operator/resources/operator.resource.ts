import { Injectable } from "@angular/core";
import { BaseResource } from "../../../infrastructure/services/base.resource";
import { IFilterable } from "../../../infrastructure/interfaces/filterable.interface";
import { OperatorSearchFilter } from "../services/operator-search.filter";
import { SearchResultItemDto } from "../../../infrastructure/models/search-result-item.dto";
import { OperatorSearchResultDto } from "../dtos/operator-search-result.dto";
import { HttpClient } from "@angular/common/http";
import { Configuration } from "../../../infrastructure/configuration/configuration";
import { Observable } from "rxjs";
import { OperatorDto } from "../../nomenclature/dtos/operator.dto";

type NewType = OperatorSearchFilter;

@Injectable()
export class OperatorResource extends BaseResource implements IFilterable<OperatorSearchFilter, SearchResultItemDto<OperatorSearchResultDto>> {
  constructor(
    protected override http: HttpClient,
    protected override configuration: Configuration
  ) {
    super(http, configuration, 'Operator');
  }

  getFiltered(filter?: OperatorSearchFilter): Observable<SearchResultItemDto<OperatorSearchResultDto>> {
    return this.http.post<SearchResultItemDto<OperatorSearchResultDto>>(`${this.baseUrl}`, filter);
  }

  getById(id: number): Observable<OperatorDto> {
    return this.http.get<OperatorDto>(`${this.baseUrl}/${id}`);
  }

  add(model: OperatorDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/Add`, model);
  }

  update(model: OperatorDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}`, model);
  }
}