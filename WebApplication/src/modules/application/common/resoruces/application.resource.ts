import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { BaseResource } from "../../../../infrastructure/services/base.resource";
import { Configuration } from "../../../../infrastructure/configuration/configuration";
import { CommitState } from "../enums/commit-state.enum";
import { ChangeApplicationStateDto } from "../models/change-application-state.dto";
import { ApplicationHistoryResultDto } from "../models/application-history-result.dto";

export class ApplicationResource<TDto> extends BaseResource {

  constructor(
    protected http: HttpClient,
    protected configuration: Configuration,
    protected suffix: string
  ) {
    super(http, configuration, suffix);
  }

  getById(id: number): Observable<TDto> {
    return this.http.get<TDto>(`${this.baseUrl}/${id}`);
  }

  getHistory(rootId: number): Observable<ApplicationHistoryResultDto> {
    return this.http.get<ApplicationHistoryResultDto>(`${this.baseUrl}/history/${rootId}`);
  }

  getByState(state: CommitState): Observable<TDto[]> {
    return this.http.get<TDto[]>(`${this.baseUrl}/getByState?state=${state}`);
  }

  saveModification(dto: TDto): Observable<TDto> {
    return this.http.post<TDto>(`${this.baseUrl}/saveModification`, dto);
  }

  create(dto: TDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}`, dto);
  }

  beginModification(model: TDto, state: CommitState): Observable<TDto> {
    return this.http.post<TDto>(`${this.baseUrl}/beginModification?state=${state}`, model);
  }

  finishModification(dto: TDto, state: CommitState): Observable<TDto> {
    return this.http.post<TDto>(`${this.baseUrl}/finishModification?state=${state}`, dto);
  }

  completeApplication(applicationId: number, changeStateDto: ChangeApplicationStateDto): Observable<TDto> {
    return this.http.post<TDto>(`${this.baseUrl}/${applicationId}/complete`, changeStateDto);
  }

  enterApplication(applicationId: number, changeStateDto: ChangeApplicationStateDto): Observable<TDto> {
    return this.http.post<TDto>(`${this.baseUrl}/${applicationId}/enter`, changeStateDto);
  }
}