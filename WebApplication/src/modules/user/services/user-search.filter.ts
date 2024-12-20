import { BaseSearchFilter } from "../../../infrastructure/models/base-search.filter";
import { UserStatus } from "../enums/user-status.enum";

export class UserSearchFilter extends BaseSearchFilter {
  firstName: string | null;
  middleName: string | null;
  lastName: string | null;
  email: string | null;
  status: UserStatus | null;

  constructor() {
    super(10);
    this.status = null;
  }
}