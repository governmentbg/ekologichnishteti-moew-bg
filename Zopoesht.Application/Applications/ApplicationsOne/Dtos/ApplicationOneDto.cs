using Zopoesht.Application.Applications.Common.Dtos;
using Zopoesht.Data.Applications.ApplicationOne.Enums;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;

namespace Zopoesht.Application.Applications.ApplicationsOne.Dtos
{
    public class ApplicationOneDto : ApplicationCommitDto
    {
        public string RegisterNumber { get; set; }
        public bool HasApplicationTwo { get; set; }
        public int? ApplicationTwoRootId { get; set; }

        public ApplicationOneType ApplicationOneType { get; set; }

        public List<ApplicationAuthorityDto> ApplicationOneAuthorities { get; set; } 

        public int? ApplicantId { get; set; }
        public ApplicantDto Applicant { get; set; }

        public ApplicationOneBasicDto ApplicationOneBasic { get; set; }

        public ApplicationOneLegalEntityDto ApplicationOneLegalEntity { get; set; }

        public ApplicationOneThreatDto ApplicationOneThreat { get; set; }

        public ApplicationOneDamageDto ApplicationOneDamage { get; set; }

        public List<ApplicationFileDto> ApplicationOneFiles { get; set; } = new List<ApplicationFileDto>();

        public override void ValidateProperties(DomainValidationService validationService)
        {
            if (!Enum.IsDefined(typeof(ApplicationOneType), ApplicationOneType))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }

            Applicant.ValidateProperties(validationService);

            ApplicationOneBasic.ValidateProperties(validationService);


            if (ApplicationOneType == ApplicationOneType.ReportedDamage)
            {
                ApplicationOneLegalEntity.ValidateProperties(validationService);
                ApplicationOneLegalEntity.ValidateApplicantViolations(Applicant.ApplicantType, validationService);
            }

            if (ApplicationOneType == ApplicationOneType.Threat)
            {
                ApplicationOneThreat.ValidateProperties(validationService);
            }

            if (ApplicationOneType == ApplicationOneType.Damage)
            {
                ApplicationOneDamage.ValidateProperties(validationService);
            }
        }
    }
}
