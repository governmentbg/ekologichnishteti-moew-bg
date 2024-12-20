export class UserPasswordDto {
  userId: number;
  currentPassword: string;
  newPassword: string;
  newPasswordConfirmation: string;
}