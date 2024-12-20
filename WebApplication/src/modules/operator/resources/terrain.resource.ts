import { Injectable } from "@angular/core";
import { BaseResource } from "../../../infrastructure/services/base.resource";
import { HttpClient } from "@angular/common/http";
import { Configuration } from "../../../infrastructure/configuration/configuration";
import { Observable } from "rxjs";
import { TerrainDto } from "../../nomenclature/dtos/terrain.dto";

@Injectable()
export class TerrainResource extends BaseResource {
  constructor(
    protected override http: HttpClient,
    protected override configuration: Configuration
  ) {
    super(http, configuration, 'Terrain');
  }

  add(model: TerrainDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}`, model);
  }

  update(model: TerrainDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}`, model);
  }
}