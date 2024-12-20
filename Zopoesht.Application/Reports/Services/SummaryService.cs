using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Application.Common;
using Zopoesht.Application.Reports.Dtos.Summary;
using Zopoesht.Application.Reports.Enums.Summary;
using Zopoesht.Application.Reports.Interfaces;
using Zopoesht.Data.Applications.ApplicationOne;
using Zopoesht.Data.Applications.ApplicationTwo;
using Zopoesht.Data.Common.Enums;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.Interfaces.Contexts;

namespace Zopoesht.Application.Reports.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly IAppDbContext context;
        private readonly IMapper mapper;
        private readonly DomainValidationService validation;

        public SummaryService(
            IAppDbContext context,
            IMapper mapper,
            DomainValidationService validation
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.validation = validation;
        }

        public async Task<SummaryResultDto> GetSummary(SummaryFilterDto filter)
        {
            filter.ValidateProperties(validation);

            var result =  new SummaryResultDto();

            var applicationTwos = this.context.Set<ApplicationTwo>()
                .Where(s => s.CommitState == CommitState.Pending || s.CommitState == CommitState.Completed)
                .OrderByDescending(s => s.CaseStatus)
                .Include(s => s.ApplicationTwoActivityOfferings)
                    .ThenInclude(s => s.ActivityOffering.Activity)
                .Include(s => s.Applicant)
                .Include(s => s.Applicant.Operator)
                .Include(s => s.Applicant.Authority)
                .Include(s => s.Applicant.Individual)
                .Include(s => s.Applicant.LegalEntity)
                .AsQueryable();

            var filteredApplicationTwos = filter
                .WhereBuilder(applicationTwos)
                .AsQueryable();

            foreach (var applicationTwo in filteredApplicationTwos)
            {
                var applicationOne = await this.context.Set<ApplicationOne>()
                    .Where(s => s.Id == applicationTwo.ApplicationOneId)
                    .Include(s => s.ApplicationOneBasic)
                    .SingleOrDefaultAsync();

                var summaryDto = MapFromApplications(applicationOne, applicationTwo);

                if (summaryDto.SummaryStageType == SummaryStageType.Finished)
                {
                    result.FinishedSummaries.Add(summaryDto);
                }
                else
                {
                    result.OnGoingSummaries.Add(summaryDto);
                }
            }

            result.Count = result.FinishedSummaries.Count() + result.OnGoingSummaries.Count();
            return result;
        }

        public async Task<byte[]> ExportWordFile(SummaryFilterDto filter, CancellationToken cancellationToken)
        {
            var result = await GetSummary(filter);

            return WordService.CreateWordDoc(result.FinishedSummaries, result.OnGoingSummaries, filter.IsBg);
        }

        private SummaryDto MapFromApplications(ApplicationOne applicationOne, ApplicationTwo applicationTwo)
        {
            var summaryDto = this.mapper.Map<SummaryDto>(applicationTwo);

            summaryDto.ApplicationOneType = applicationOne.ApplicationOneType;
            summaryDto.Description = applicationOne.ApplicationOneBasic.Name;
            summaryDto.DescriptionAlt = applicationOne.ApplicationOneBasic.NameAlt;

            return summaryDto;
        }
    }
}
