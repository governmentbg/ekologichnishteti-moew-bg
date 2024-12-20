import { Injectable } from "@angular/core";
import { BaseResource } from "../../../infrastructure/services/base.resource";
import { HttpClient } from "@angular/common/http";
import { Configuration } from "../../../infrastructure/configuration/configuration";
import { Observable } from "rxjs";
import { PeriodDto } from "../dtos/period.dto";

@Injectable()
export class PeriodResource extends BaseResource {
  constructor(
    protected override http: HttpClient,
    protected override configuration: Configuration
  ) {
    super(http, configuration, 'Period');
  }

  getAll(): Observable<PeriodDto[]> {
    return this.http.get<PeriodDto[]>(`${this.baseUrl}`);
  }

  add(model: PeriodDto): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}`, model);
  }

  update(model: PeriodDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}`, model);
  }
}