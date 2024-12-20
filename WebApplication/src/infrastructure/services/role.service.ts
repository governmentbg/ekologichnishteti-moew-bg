import { Injectable } from '@angular/core';
import { UserContextService } from '../../modules/user/services/user-context.service';

@Injectable()
export class RoleService {
  constructor(
    private userContextService: UserContextService
  ) { }

  hasRole(...aliases: string[]): boolean {
    const roleAlias = this.userContextService.userContext.role;

    let result = false;
    aliases.forEach((alias: string) => result = result || roleAlias === alias);
    return result;
  }
}
