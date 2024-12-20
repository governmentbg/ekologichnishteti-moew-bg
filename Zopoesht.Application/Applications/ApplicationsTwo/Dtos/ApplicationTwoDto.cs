using Zopoesht.Application.Applications.ApplicationsOne.Dtos;
using Zopoesht.Application.Applications.ApplicationsTwo.Dtos.FinancialAssurance;
using Zopoesht.Application.Applications.Common.Dtos;
using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Data.Applications.ApplicationOne.Enums;
using Zopoesht.Data.Applications.ApplicationTwo.Enums;
using Zopoesht.Data.Applications.Common.Enums;
using Zopoesht.Data.Nomenclatures.StateAdministration.Enums;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Constants;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Helpers.Dtos;

namespace Zopoesht.Application.Applications.ApplicationsTwo.Dtos
{
    public class ApplicationTwoDto : ApplicationCommitDto
    {
        public int ApplicationOneId { get; set; }
        public ApplicationOneType ApplicationOneType { get; set; }

        public bool HasWaterDamage { get; set; }
        public bool HasSoilDamage { get; set; }
        public bool HasSpeciesDamage { get; set; }

        public DateTime? OccurrenceDate { get; set; }
        public string OccurrenceDateDescription { get; set; }
        public string OccurrenceDateDescriptionAlt { get; set; }

        public DateTime? ConfirmedDate { get; set; }

        public List<NomenclatureDto> SubActivities { get; set; }

        public DateTime? ProcedureInitiatedDate { get; set; }
        public string ProcedureInitiatedDateDescription { get; set; }
        public string ProcedureInitiatedDateDescriptionAlt { get; set; }

        public int ApplicantId { get; set; }
        public ApplicantDto Applicant { get; set; }

        public int? OperatorId { get; set; }
        public OperatorDto Operator { get; set; }

        public ActivityOfferingType? ActivityOfferingType { get; set; }
        public string NotListedDescription { get; set; }
        public string DiffuseNatureDescription { get; set; }

        public List<ActivityOfferingDto> ApplicationTwoActivityOfferings { get; set; }

        public List<ApplicationAuthorityDto> ApplicationTwoAuthorities { get; set; }

        public List<CodeDto> Codes { get; set; }

        public bool ProceedingInfoAbsence { get; set; }
        public ProceedingType? ProceedingType { get; set; }
        public List<ApplicationTwoAdministrativeCourtDto> ApplicationTwoAdministrativeCourts { get; set; }
        public List<ApplicationTwoProsecutorDto> ApplicationTwoProsecutors { get; set; }
        public List<ApplicationTwoMinistryOfInteriorDto> ApplicationTwoMinistryOfInteriors { get; set; }
        public string ProceedingInfo { get; set; }


        public string LegalProcedureResult { get; set; }

        public bool HasPreventionProcedureResult { get; set; }
        public string PreventionProcedureResult { get; set; }

        public bool HasRemovalProcedureResult { get; set; }
        public string RemovalProcedureResult { get; set; }

        public CaseStatus CaseStatus { get; set; }
        public DateTime? PreventionOrRemovalProcedureFinishDate { get; set; }
        public string PreventionOrRemovalProcedureFinishInformation { get; set; }

        public string ActionsTaken { get; set; }
        public string ActionsTakenAlt { get; set; }

        public decimal? PaidByResponsibleParties { get; set; }
        public decimal? RecoveredSubsequentlyByResponsibleParties { get; set; }
        public bool HasUnreimbursedExpense { get; set; }
        public decimal? UnreimbursedExpenseValue { get; set; }
        public string UnreimbursedExpense { get; set; }
        public string AdditionalExpenseText { get; set; }

        public PaymentSource PaymentSource { get; set; }
        public string OtherPaymentSource { get; set; }

        public bool HasInsurancePolicy { get; set; }
        public int? InsurancePolicyId { get; set; }
        public InsurancePolicyDto InsurancePolicy { get; set; }
        public bool HasBankGuarantee { get; set; }
        public int? BankGuaranteeId { get; set; }
        public BankGuaranteeDto BankGuarantee { get; set; }
        public bool HasMortgage { get; set; }
        public int? MortgageId { get; set; }
        public MortgageDto Mortgage { get; set; }
        public bool HasStake { get; set; }
        public int? StakeId { get; set; }
        public StakeDto Stake { get; set; }
        public bool HasNoCollateral { get; set; }
        public string NoCollateralAdditionalInformation { get; set; } 

        public string AdditionalInformation { get; set; }

        public List<ApplicationFileDto> ApplicationTwoFiles { get; set; } = new List<ApplicationFileDto>();

        public override void ValidateProperties(DomainValidationService validationService)
        {
            if (!Enum.IsDefined(typeof(ApplicationOneType), ApplicationOneType))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }

            if (!HasWaterDamage && !HasSoilDamage && !HasSpeciesDamage)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_NotSelectedDamage);
            }

            if (!OccurrenceDate.HasValue || !ConfirmedDate.HasValue)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!string.IsNullOrWhiteSpace(OccurrenceDateDescription) && OccurrenceDateDescription.Length > ValidationConstants.TextMaxLength1024)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ExcessiveText);
            }

            // SubActivities

            if (!ProcedureInitiatedDate.HasValue)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!string.IsNullOrWhiteSpace(ProcedureInitiatedDateDescription) && ProcedureInitiatedDateDescription.Length > ValidationConstants.TextMaxLength1024)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ExcessiveText);
            }

            if (ApplicantId <= 0 || Applicant == null)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }
            else
            {
                Applicant.ValidateProperties(validationService);
            }

            if (ApplicationTwoActivityOfferings.Any())
            {
                ApplicationTwoActivityOfferings.ForEach(a => a.ValidateProperties(validationService)); ;
            }

            if (ApplicationTwoAuthorities == null || !ApplicationTwoAuthorities.Any())
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }
            else
            {
                if (ApplicationTwoAuthorities.Any(a => a.AuthorityId <= 0 || a.Authority == null))
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                }

                ApplicationTwoAuthorities.ForEach(a => a.Authority.ValidateProperties(validationService));
            }

            if (Codes != null && Codes.Any())
            {
                Codes.ForEach(c => c.ValidateProperties(validationService));
            }

            if (!ProceedingInfoAbsence)
            {
                if (!ProceedingType.HasValue)
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                }

                if (!Enum.IsDefined(typeof(ProceedingType), ProceedingType))
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
                }

                if (ProceedingType == Data.Nomenclatures.StateAdministration.Enums.ProceedingType.PreTrial)
                {
                    if ((ApplicationTwoProsecutors == null || !ApplicationTwoProsecutors.Any()) 
                        && (ApplicationTwoMinistryOfInteriors == null || !ApplicationTwoMinistryOfInteriors.Any()))
                    {
                        validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                    }
                    else
                    {
                        if (ApplicationTwoProsecutors != null)
                        {
                            ApplicationTwoProsecutors.ForEach(a => a.ValidateProperties(validationService));
                        }
                        
                        if (ApplicationTwoMinistryOfInteriors != null)
                        {
                            ApplicationTwoMinistryOfInteriors.ForEach(a => a.ValidateProperties(validationService));
                        }
                    }
                }
                else if (ProceedingType == Data.Nomenclatures.StateAdministration.Enums.ProceedingType.Legal)
                {
                    if (ApplicationTwoAdministrativeCourts == null || !ApplicationTwoAdministrativeCourts.Any())
                    {
                        validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                    }

                    ApplicationTwoAdministrativeCourts.ForEach(a => a.ValidateProperties(validationService));
                }
                else if (ProceedingType == Data.Nomenclatures.StateAdministration.Enums.ProceedingType.Both)
                {
                    if ((ApplicationTwoProsecutors == null || !ApplicationTwoProsecutors.Any())
                        && (ApplicationTwoMinistryOfInteriors == null || !ApplicationTwoMinistryOfInteriors.Any()))
                    {
                        validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                    }

                    if (ApplicationTwoAdministrativeCourts == null || !ApplicationTwoAdministrativeCourts.Any())
                    {
                        validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                    }

                    if (ApplicationTwoProsecutors != null)
                    {
                        ApplicationTwoProsecutors.ForEach(a => a.ValidateProperties(validationService));
                    }

                    if (ApplicationTwoMinistryOfInteriors != null)
                    {
                        ApplicationTwoMinistryOfInteriors.ForEach(a => a.ValidateProperties(validationService));
                    }

                    ApplicationTwoAdministrativeCourts.ForEach(a => a.ValidateProperties(validationService));
                }
            }

            if (string.IsNullOrWhiteSpace(LegalProcedureResult))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!HasPreventionProcedureResult && string.IsNullOrWhiteSpace(PreventionProcedureResult))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!HasRemovalProcedureResult && string.IsNullOrWhiteSpace(RemovalProcedureResult))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!Enum.IsDefined(typeof(CaseStatus), CaseStatus))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }

            if (CaseStatus == CaseStatus.Completed && 
                (PreventionOrRemovalProcedureFinishDate == null ||
                string.IsNullOrWhiteSpace(PreventionOrRemovalProcedureFinishInformation)))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!string.IsNullOrWhiteSpace(ActionsTaken) && ActionsTaken.Length > ValidationConstants.TextMaxLength1024)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ExcessiveText);
            }

            if (PaidByResponsibleParties == null || PaidByResponsibleParties < 0)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }

            if (RecoveredSubsequentlyByResponsibleParties == null || RecoveredSubsequentlyByResponsibleParties < 0)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }

            if (!HasUnreimbursedExpense)
            {
                if (UnreimbursedExpenseValue == null || UnreimbursedExpenseValue < 0)
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
                }
            }

            if (PaymentSource == PaymentSource.Other && string.IsNullOrWhiteSpace(OtherPaymentSource))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (HasInsurancePolicy)
            {
                if (InsurancePolicy == null)
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                }

                InsurancePolicy.ValidateProperties(validationService);
            }

            if (HasBankGuarantee)
            {
                if (BankGuarantee == null)
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                }

                BankGuarantee.ValidateProperties(validationService);
            }

            if (HasMortgage)
            {
                if (Mortgage == null)
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                }

                Mortgage.ValidateProperties(validationService);
            }

            if (HasStake)
            {
                if (Stake == null)
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                }

                Stake.ValidateProperties(validationService);
            }

            if (HasNoCollateral && string.IsNullOrWhiteSpace(NoCollateralAdditionalInformation))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (HasNoCollateral && 
                (HasInsurancePolicy || HasBankGuarantee || HasMortgage || HasStake))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }

            if (!string.IsNullOrWhiteSpace(AdditionalInformation) && AdditionalInformation.Length > ValidationConstants.TextMaxLength1024)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ExcessiveText);
            }
        }
     }
}
