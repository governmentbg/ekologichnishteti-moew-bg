using Zopoesht.Application.Nomenclatures.Dtos;
﻿using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Data.Applications.Common.Enums;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Constants;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Helpers.Dtos;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Applications.ApplicationsOne.Dtos
{
    public class ApplicationOneBasicDto : IValidate
    {
        public int Id { get; set; }
        public string IncomingDocNumber { get; set; }

        public int ApplicationOneId { get; set; }

        public string Name { get; set; }
        public string NameAlt { get; set; }

        public int? OperatorId { get; set; }
        public OperatorDto Operator { get; set; }

        public ActivityOfferingType? ActivityOfferingType { get; set; }
        public string NotListedDescription { get; set; }
        public string DiffuseNatureDescription { get; set; }

        public List<ActivityOfferingDto> ActivityOfferings { get; set; }

        public bool HasWaterDamage { get; set; }
        public bool HasSoilDamage { get; set; }
        public bool HasSpeciesDamage { get; set; }

        public List<AuthorityDto> TerritorialRanges { get; set; }

        public string OccurenceLocation { get; set; }

        public string OccurenceReasons { get; set; }

        public string AdditionalInformation { get; set; }

        public bool HasInternationalElement { get; set; }
        public string InternationalElementDescription { get; set; }
        public int? CulpritCountryId { get; set; }
        public NomenclatureDto CulpritCountry { get; set; }
        public List<NomenclatureDto> AffectedCountries { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            //if (string.IsNullOrWhiteSpace(IncomingDocNumber))
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            //}

            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = null;
            }

            if (!HasWaterDamage && !HasSoilDamage && !HasSpeciesDamage)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_NotSelectedDamage);
            }

            if (!TerritorialRanges.Any())
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }
            else
            {
                TerritorialRanges.ForEach(t => t.ValidateProperties(validationService));
            }

            OperatorId = Operator?.Id == 0 ? null : Operator?.Id;

            if (!OperatorId.HasValue)
            {
                ActivityOfferings = null;
            }

            if (string.IsNullOrWhiteSpace(OccurenceLocation))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            //if (!ValidatePropertiesHelperExtension.IsValidLettersNumbersAndSymbols(OccurenceLocation))
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            //}

            if (OccurenceLocation?.Length > ValidationConstants.TextMaxLength1024)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ExcessiveText);
            }

            if (string.IsNullOrWhiteSpace(OccurenceReasons))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            //if (!ValidatePropertiesHelperExtension.IsValidLettersNumbersAndSymbols(OccurenceReasons))
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            //}

            if (OccurenceReasons?.Length > ValidationConstants.TextMaxLength1024)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ExcessiveText);
            }

            //if (!string.IsNullOrWhiteSpace(AdditionalInformation)
            //    && !ValidatePropertiesHelperExtension.IsValidLettersNumbersAndSymbols(AdditionalInformation))
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            //}

            if (AdditionalInformation?.Length > ValidationConstants.TextMaxLength1024)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ExcessiveText);
            }

            if (InternationalElementDescription?.Length > ValidationConstants.TextMaxLength1024)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ExcessiveText);
            }
        }
    }
}
