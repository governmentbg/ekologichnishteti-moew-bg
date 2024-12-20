import { Injectable } from "@angular/core";
import { BaseResource } from "../../../infrastructure/services/base.resource";
import { HttpClient } from "@angular/common/http";
import { Configuration } from "../../../infrastructure/configuration/configuration";
import { Observable } from "rxjs";
import { ActivityOfferingDto } from "../../nomenclature/dtos/activity-offering.dto";

@Injectable()
export class ActivityOfferingResource extends BaseResource {
  constructor(
    protected override http: HttpClient,
    protected override configuration: Configuration
  ) {
    super(http, configuration, 'ActivityOffering');
  }

  add(model: ActivityOfferingDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}`, model);
  }

  update(model: ActivityOfferingDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}`, model);
  }
}