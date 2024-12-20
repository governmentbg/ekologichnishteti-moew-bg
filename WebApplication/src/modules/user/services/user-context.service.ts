import { Injectable } from "@angular/core";
import { UserContext } from "../models/user-context.dto";
import { UserContextResource } from "../resources/user-context.resource";
import { Observable, Observer, Subject, Subscription, catchError, throwError } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserContextService {
  userContext = new UserContext();
  private userContextSubject: Subject<{ event: boolean }> = new Subject();

  constructor(
    private userContextResource: UserContextResource
  ) {
  }

  setUserContext(userContext: UserContext) {
    this.userContext = userContext;
    this.userContextSubject.next({ event: true });
  }

  clearUserContext() {
    this.userContext = null;
    this.userContextSubject.next({ event: false });
  }

  getUserInfo() {
    return new Observable((observer: Observer<any>) => {
      return this.userContextResource.getUserInfo()
        .pipe(
          catchError((err) => {
            this.clearUserContext();
            observer.next(this.userContext);
            observer.complete();
            return throwError(() => err);
          })
        )
        .subscribe(userContext => {
          this.setUserContext(userContext);
          observer.next(this.userContext);
          observer.complete();
        });
    })
  }

  subscribe(next: (value: { event: boolean }) => void): Subscription {
    return this.userContextSubject.subscribe(next);
  }
}