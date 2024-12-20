import { UserStatus } from "../enums/user-status.enum";
import { RoleDto } from "./role.dto";

export class UserInfoDto {
  id: number;
  firstName: string;
  middleName: string;
  lastName: string;
  username: string;
  phone: string;
  email: string;
  role: RoleDto;
  status: UserStatus;
}