using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Data.Applications.ApplicationOne;
using Zopoesht.Data.Applications.ApplicationOne.Enums;
using Zopoesht.Data.Common.Enums;
using Zopoesht.Data.Nomenclatures;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Applications.ApplicationsOne.Dtos
{
    public class ApplicationOneSearchFilterDto : IValidate
    {
        public ApplicationOneType? Type { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public CommitState? ApplicationOneState { get; set; }

        public ApplicantType? ApplicantType { get; set; }

        public Authority Authority { get; set; }
        public Authority ApplicantAuthority { get; set; }

        public int? RiosvId { get; set; }

        public Authority Riosv { get; set; }

        public int? BasinId { get; set; }

        public Authority Basin { get; set; }

        public int? OperatorId { get; set; }

        public OperatorDto Operator { get; set; }

        public ApplicationOneDamageType? ApplicationOneDamage { get; set; }


        public int Limit { get; set; }

        public int Offset { get; set; }

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

        public IQueryable<ApplicationOne> WhereBuilder(IQueryable<ApplicationOne> query)
        {
            if (Type.HasValue)
            {
                query = query.Where(a => a.ApplicationOneType == Type);
            }

            if (DateFrom.HasValue)
            {
                query = query.Where(a => DateOnly.FromDateTime(a.CreateDate) >= DateOnly.FromDateTime(DateFrom.Value));
            }

            if (DateTo.HasValue)
            {
                query = query.Where(a => DateOnly.FromDateTime(a.CreateDate) <= DateOnly.FromDateTime(DateTo.Value));
            }

            if (ApplicationOneState.HasValue)
            {
                if (ApplicationOneState == CommitState.Entered)
                {
                    query = query.Where(a => a.CommitState == ApplicationOneState && (!a.ApplicationTwos.Any() || a.ApplicationTwos.Any(t => t.CommitState == CommitState.Pending)));
                }
                else if (ApplicationOneState == CommitState.Completed)
                {
                    query = query.Where(a => a.ApplicationTwos.Any(t => t.CommitState == ApplicationOneState));
                }
                else if (ApplicationOneState == CommitState.Pending)
                {
                    query = query.Where(a => a.CommitState == ApplicationOneState && !a.ApplicationTwos.Any());
                }
                else if (ApplicationOneState == CommitState.Modification)
                {
                    query = query.Where(a => a.CommitState == ApplicationOneState || a.ApplicationTwos.Any(t => t.CommitState == ApplicationOneState));
                }
            }

            if (ApplicantType.HasValue)
            {
                query = query.Where(a => a.Applicant.ApplicantType == ApplicantType);
            }

            if (Authority != null)
            {
                query = query.Where(a => a.ApplicationTwos
                    .Any(appTwo => appTwo.ApplicationTwoAuthorities
                        .Any(auth => auth.AuthorityId == Authority.Id)));
            }

            if (ApplicantAuthority != null)
            {
                query = query.Where(a => a.Applicant.AuthorityId == ApplicantAuthority.Id);
            }

            if (RiosvId.HasValue)
            {
                query = query.Where(a => a.ApplicationOneBasic.TerritorialRanges.Any(t => t.AuthorityId == RiosvId));
            }

            if (BasinId.HasValue)
            {
                query = query.Where(a => a.ApplicationOneBasic.TerritorialRanges.Any(t => t.AuthorityId == BasinId));
            }

            if (OperatorId.HasValue)
            {
                query = query.Where(a => a.ApplicationOneBasic.OperatorId == OperatorId);

            }

            if (ApplicationOneDamage.HasValue)
            {
                if (ApplicationOneDamage == ApplicationOneDamageType.WaterDamage)
                {
                    query = query.Where(a => a.ApplicationOneBasic.HasWaterDamage);
                }

                if (ApplicationOneDamage == ApplicationOneDamageType.SoilDamage)
                {
                    query = query.Where(a => a.ApplicationOneBasic.HasSoilDamage);
                }

                if (ApplicationOneDamage == ApplicationOneDamageType.SpeciesDamage)
                {
                    query = query.Where(a => a.ApplicationOneBasic.HasSpeciesDamage);
                }
            }

            return query;
        }
    }
}
