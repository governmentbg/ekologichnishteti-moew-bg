namespace Zopoesht.Infrastructure.DomainValidation.Enums
{
    public enum UserErrorCode
    {
        User_InvalidCredentials = 201,
        User_InvalidEmail = 202,
        User_NotActive = 203,
        User_ExistingEmail = 204,
        User_ExistingUsername = 205,
        User_DoesNotExist = 206,
        User_NotEnoughPermission = 207,
        User_MissmatchedPasswords = 208,
        User_InvalidPassword = 209,
        User_IsDeactivated = 210,
        User_IsInactive = 211
    }
}