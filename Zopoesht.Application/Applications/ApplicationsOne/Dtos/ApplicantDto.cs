using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Data.Applications.ApplicationOne.Enums;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Applications.ApplicationsOne.Dtos
{
    public class ApplicantDto : IValidate
    {
        public ApplicantType ApplicantType { get; set; }
        public AuthorityDto Authority { get; set; }

        public IndividualDto Individual { get; set; }

        public LegalEntityDto LegalEntity { get; set; }

        public OperatorDto Operator { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (!Enum.IsDefined(typeof(ApplicantType), ApplicantType))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidApplicantType);
            }

            if (ApplicantType == ApplicantType.Authority)
            {
                Authority.ValidateProperties(validationService);
            }

            if (ApplicantType == ApplicantType.Operator)
            {
                Operator.ValidateProperties(validationService);
            }

            if (ApplicantType == ApplicantType.Individual)
            {
                Individual.ValidateProperties(validationService);
            }

            if (ApplicantType == ApplicantType.LegalEntity || ApplicantType == ApplicantType.NonGovernmentalOrganization)
            {
                LegalEntity.ValidateProperties(validationService);
            }
        }
    }
}
