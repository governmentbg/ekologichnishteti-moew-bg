import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseNomenclatureFilterDto } from '../models/base-nomenclature-filter.dto';
import { Nomenclature } from '../models/nomenclature.model';
import { BaseResource } from '../../../../infrastructure/services/base.resource';
import { Configuration } from '../../../../infrastructure/configuration/configuration';
import { SearchResultItemDto } from '../../../../infrastructure/models/search-result-item.dto';

@Injectable()
export class NomenclatureResource<T extends Nomenclature, TFilter extends BaseNomenclatureFilterDto> extends BaseResource {
  constructor(
    protected override http: HttpClient,
    protected override configuration: Configuration
  ) {
    super(http, configuration);
  }

  getFiltered(filter?: TFilter): Observable<SearchResultItemDto<T>> {
    return this.http.get<SearchResultItemDto<T>>(`${this.getBaseUrl()}${this.composeQueryString(filter)}`);
  }

  add(entity: T): Observable<T> {
    return this.http.post<T>(this.baseUrl, entity);
  }

  update(id: number, entity: T): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/${id}`, entity);
  }

  // delete(id: number): Observable<any> {
  //   return this.http.delete(`${this.baseUrl}/${id}`);
  // }
}