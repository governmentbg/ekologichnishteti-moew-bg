namespace Zopoesht.Infrastructure.DomainValidation.Enums
{
    public enum ValidationErrorCode
    {
        Validation_RequiredField = 301,
        Validation_InvalidEmail = 302,
        Validation_InvalidFormat = 303,
        Validation_InvalidApplicantType = 304,
        Validation_InvalidApplicationOneType = 305,
        Validation_NotSelectedDamage = 306,
        Validation_ApplicationFromDateGreaterThanNow = 307,
        Validation_ApplicationToDateGreaterThanNow = 308,
        Validation_FromDateGreaterThanToDate = 309,
        Validation_ExcessiveText = 310,
        Validation_OperatorRequired = 311
    }
}
