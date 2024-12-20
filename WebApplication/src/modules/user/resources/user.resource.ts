import { Injectable } from "@angular/core";
import { BaseResource } from "../../../infrastructure/services/base.resource";
import { IFilterable } from "../../../infrastructure/interfaces/filterable.interface";
import { UserSearchFilter } from "../services/user-search.filter";
import { SearchResultItemDto } from "../../../infrastructure/models/search-result-item.dto";
import { UserSearchResultDto } from "../models/user-search-result.dto";
import { HttpClient } from "@angular/common/http";
import { Configuration } from "../../../infrastructure/configuration/configuration";
import { Observable } from "rxjs";
import { UserDto } from "../models/user.dto";
import { UserContext } from "../models/user-context.dto";
import { UserPasswordDto } from "../models/user-password.dto";

type NewType = UserSearchFilter;

@Injectable()
export class UserResource extends BaseResource implements IFilterable<UserSearchFilter, SearchResultItemDto<UserSearchResultDto>> {

  constructor(
    protected override http: HttpClient,
    protected override configuration: Configuration
  ) {
    super(http, configuration, 'User');
  }

  getFiltered(filter?: NewType): Observable<SearchResultItemDto<UserSearchResultDto>> {
    return this.http.post<SearchResultItemDto<UserSearchResultDto>>(`${this.baseUrl}`, filter);
  }

  getById(id: number): Observable<UserDto> {
    return this.http.get<UserDto>(`${this.baseUrl}/${id}`);
  }

  add(model: UserDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/Add`, model);
  }

  update(model: UserDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}`, model);
  }

  changePassword(model: UserPasswordDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/ChangePassword`, model);
  }

  getUserInfo(): Observable<UserContext> {
    return this.http.get<UserContext>(`${this.baseUrl}/UserContext`)
  }
}