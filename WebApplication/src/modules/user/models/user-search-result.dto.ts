import { AuthorityDto } from "../../nomenclature/dtos/authority.dto";
import { UserStatus } from "../enums/user-status.enum";
import { RoleDto } from "./role.dto";

export class UserSearchResultDto {
  id: number;
  fullname: string;
  phone: string;
  email: string;
  role: RoleDto;
  authority: AuthorityDto;
  status: UserStatus;
}