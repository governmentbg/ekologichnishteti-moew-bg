using Microsoft.EntityFrameworkCore;
using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Reports.Dtos;
using Zopoesht.Application.Reports.Enums;
using Zopoesht.Application.Reports.Interfaces;
using Zopoesht.Data.Applications.ApplicationOne;
using Zopoesht.Data.Applications.ApplicationOne.Enums;
using Zopoesht.Infrastructure.Interfaces.Contexts;

namespace Zopoesht.Application.Reports.Services
{
    public class ReportService : IReportService
    {
        private readonly IAppDbContext context;

        public ReportService(IAppDbContext context)
        {
            this.context = context;
        }

        public async Task<SearchResultItemDto<ReportSearchResultDto>> GetFiltered(ReportSearchFilterDto model, CancellationToken cancellationToken)
        {
            var itemsDictionary = new Dictionary<string, ReportSearchResultDto>();

            switch (model.ReportType)
            {
                case ReportType.TerritorialRange:
                    await MakeTerritorialRangeReport(model, itemsDictionary);
                    break;
                case ReportType.ApplicantType:
                    await MakeApplicantTypeReport(model, itemsDictionary);
                    break;
            }

            return new SearchResultItemDto<ReportSearchResultDto>()
            {
                TotalCount = itemsDictionary.Count,
                Items = itemsDictionary.Values.ToList()
            };
        }

        private async Task MakeTerritorialRangeReport(ReportSearchFilterDto model, Dictionary<string, ReportSearchResultDto> itemsDictionary)
        {
            var applicationOneTerritorialRanges = context.Set<ApplicationOneTerritorialRange>()
                .Include(a => a.Authority)
                .Include(a => a.ApplicationOneBasic)
                .ThenInclude(a => a.ApplicationOne)
                .AsQueryable();

            var filteredTerritorialRanges = await model
                .WhereBuilder(applicationOneTerritorialRanges)
                .OrderBy(a => a.Authority.ViewOrder)
                .ToListAsync();

            foreach (var item in filteredTerritorialRanges)
            {
                if (!itemsDictionary.ContainsKey(item.Authority.Name))
                {
                    itemsDictionary.Add(item.Authority.Name, new ReportSearchResultDto(item.Authority.Name));
                }

                var currentItem = itemsDictionary[item.Authority.Name];
                var currentApplicationOneBasic = item.ApplicationOneBasic;

                IncreaseDamageCount(item.ApplicationOneBasic.ApplicationOne.ApplicationOneType, currentItem, currentApplicationOneBasic);
            }
        }

        private async Task MakeApplicantTypeReport(ReportSearchFilterDto model, Dictionary<string, ReportSearchResultDto> itemsDictionary)
        {
            string operatorString = "Оператор";
            string authorityString = "Компетентен орган";
            string otherString = "ФЛ/ЮЛ/НПО";

            var applications = context.Set<ApplicationOne>()
                .Include(a => a.Applicant)
                .Include(a => a.ApplicationOneBasic)
                .AsQueryable();

            var filteredApplications = await model
                .WhereBuilder(applications)
                .ToListAsync();

            itemsDictionary.Add(operatorString, new ReportSearchResultDto(operatorString));
            itemsDictionary.Add(authorityString, new ReportSearchResultDto(authorityString));
            itemsDictionary.Add(otherString, new ReportSearchResultDto(otherString));

            foreach (var item in filteredApplications)
            {
                ReportSearchResultDto currentItem;

                if (item.Applicant.ApplicantType == ApplicantType.Operator)
                {
                    currentItem = itemsDictionary[operatorString];
                }
                else if (item.Applicant.ApplicantType == ApplicantType.Authority)
                {
                    currentItem = itemsDictionary[authorityString];
                }
                else
                {
                    currentItem = itemsDictionary[otherString];
                }

                var currentApplicationOneBasic = item.ApplicationOneBasic;

                IncreaseDamageCount(item.ApplicationOneBasic.ApplicationOne.ApplicationOneType, currentItem, currentApplicationOneBasic);
            }
        }

        private void IncreaseDamageCount(ApplicationOneType applicationOneType, ReportSearchResultDto currentItem, ApplicationOneBasic currentApplicationOneBasic)
        {
            switch (applicationOneType)
            {
                case ApplicationOneType.Threat:
                    currentItem.Threat.TotalCount++;
                    currentItem.Threat.WaterDamageCount += currentApplicationOneBasic.HasWaterDamage ? 1 : 0;
                    currentItem.Threat.SoilDamageCount += currentApplicationOneBasic.HasSoilDamage ? 1 : 0;
                    currentItem.Threat.SpeciesDamageCount += currentApplicationOneBasic.HasSpeciesDamage ? 1 : 0;
                    break;
                case ApplicationOneType.Damage:
                    currentItem.Damage.TotalCount++;
                    currentItem.Damage.WaterDamageCount += currentApplicationOneBasic.HasWaterDamage ? 1 : 0;
                    currentItem.Damage.SoilDamageCount += currentApplicationOneBasic.HasSoilDamage ? 1 : 0;
                    currentItem.Damage.SpeciesDamageCount += currentApplicationOneBasic.HasSpeciesDamage ? 1 : 0;
                    break;
                case ApplicationOneType.ReportedDamage:
                    currentItem.ReportedDamage.TotalCount++;
                    currentItem.ReportedDamage.WaterDamageCount += currentApplicationOneBasic.HasWaterDamage ? 1 : 0;
                    currentItem.ReportedDamage.SoilDamageCount += currentApplicationOneBasic.HasSoilDamage ? 1 : 0;
                    currentItem.ReportedDamage.SpeciesDamageCount += currentApplicationOneBasic.HasSpeciesDamage ? 1 : 0;
                    break;
            }

            currentItem.ItemCount = currentItem.Threat.TotalCount + currentItem.Damage.TotalCount + currentItem.ReportedDamage.TotalCount;
        }
    }
}
