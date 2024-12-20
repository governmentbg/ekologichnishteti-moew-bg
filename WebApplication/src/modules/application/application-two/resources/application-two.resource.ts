import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Configuration } from "../../../../infrastructure/configuration/configuration";
import { ApplicationResource } from "../../common/resoruces/application.resource";
import { ApplicationTwoDto } from "../models/application-two.dto";
import { Observable } from "rxjs";

@Injectable()
export class ApplicationTwoResource extends ApplicationResource<ApplicationTwoDto> {

  constructor(
    protected override http: HttpClient,
    protected override configuration: Configuration
  ) {
    super(http, configuration, 'ApplicationTwo');
  }

  cancelModification(dto: ApplicationTwoDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/CancelModification`, dto);
  }
}