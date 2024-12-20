using Zopoesht.Data.Applications.ApplicationTwo;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Reports.Dtos.Summary
{
    public class SummaryFilterDto : IValidate
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public bool IsBg { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (DateFrom.HasValue && DateFrom > DateTime.UtcNow)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ApplicationFromDateGreaterThanNow);
            }

            if (DateTo.HasValue && DateTo > DateTime.UtcNow)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ApplicationToDateGreaterThanNow);
            }

            if (DateFrom.HasValue && DateTo.HasValue && DateFrom > DateTo)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_FromDateGreaterThanToDate);
            }
        }

        public IQueryable<ApplicationTwo> WhereBuilder(IQueryable<ApplicationTwo> query)
        {
            if (DateFrom.HasValue)
            {
                query = query.Where(a => DateOnly.FromDateTime(a.OccurrenceDate.Value) >= DateOnly.FromDateTime(DateFrom.Value));
            }

            if (DateTo.HasValue)
            {
                query = query.Where(a => DateOnly.FromDateTime(a.OccurrenceDate.Value) <= DateOnly.FromDateTime(DateTo.Value));
            }

            return query;
        }
    }
}
