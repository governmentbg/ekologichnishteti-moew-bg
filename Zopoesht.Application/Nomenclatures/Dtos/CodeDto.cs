﻿using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Helpers.Dtos;
using Zopoesht.Infrastructure.Helpers.Extensions;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Nomenclatures.Dtos
{
    public class CodeDto : NomenclatureDto, IValidate
    {
        public void ValidateProperties(DomainValidationService validationService)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!ValidatePropertiesHelperExtension.IsValidCyrillicAndNumber(Name))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }
        }
    }
}
