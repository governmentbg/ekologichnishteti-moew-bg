import { AuthorityDto } from "../../nomenclature/dtos/authority.dto";
import { UserStatus } from "../enums/user-status.enum";
import { RoleDto } from "./role.dto";

export class UserDto {
  id: number;
  username: string;
  password: string;
  firstName: string;
  middleName: string;
  lastName: string;
  email: string;
  phone: string;
  role: RoleDto;
  status: UserStatus;
  authorityId: number;
  authority: AuthorityDto;
  position: string;
}