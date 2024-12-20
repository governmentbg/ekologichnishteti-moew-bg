using Zopoesht.Data.Nomenclatures.Operators;
using Zopoesht.Data.Nomenclatures;
using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Infrastructure.Interfaces;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;

namespace Zopoesht.Application.Operators.Dtos
{
    public class ActivityOfferingDto : IValidate
    {
        public int Id { get; set; }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        public int SubActivityId { get; set; }
        public SubActivity SubActivity { get; set; }

        public int OperatorId { get; set; }
        public OperatorDto Operator { get; set; }

        public int? TerrainId { get; set; }
        public Terrain Terrain { get; set; }

        public int? AuthorityRiosvId { get; set; }
        public AuthorityDto AuthorityRiosv { get; set; }

        public int? AuthorityBasinId { get; set; }
        public AuthorityDto AuthorityBasin { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (ActivityId <= 0 || Activity == null)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (SubActivityId <= 0 || SubActivity == null)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            //if (AuthorityRiosvId <= 0 || AuthorityRiosv == null)
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            //}

            //if (AuthorityBasinId <= 0 || AuthorityBasin == null)
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            //}
        }
    }
}
