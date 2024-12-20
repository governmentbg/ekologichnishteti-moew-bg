import { Observable } from "rxjs";
import { UserContext } from "../models/user-context.dto";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class UserContextResource {

  url = 'api/User';

  constructor(
    private http: HttpClient
  ) { }

  getUserInfo(): Observable<UserContext> {
    return this.http.get<UserContext>(`${this.url}/UserContext`)
  }
}