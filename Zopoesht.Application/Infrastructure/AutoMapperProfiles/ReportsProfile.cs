using AutoMapper;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Application.Reports.Dtos.Summary;
using Zopoesht.Application.Reports.Enums.Summary;
using Zopoesht.Data.Applications.ApplicationTwo;
using Zopoesht.Data.Applications.ApplicationTwo.Enums;

namespace Zopoesht.Application.Infrastructure.AutoMapperProfiles
{
    public class ReportsProfile : Profile
    {
        public ReportsProfile() 
        {
            this.CreateMap<ApplicationTwo, SummaryDto>()
                .ForMember(m => m.SummaryStageType, cfg => cfg.MapFrom(src => src.CaseStatus == CaseStatus.Active ? SummaryStageType.OnGoing : SummaryStageType.Finished))
                .ForMember(m => m.ActivityOfferings, cfg => cfg.MapFrom(src => src.ApplicationTwoActivityOfferings))
                .ForMember(m => m.StartYear, cfg => cfg.MapFrom(src => src.OccurrenceDate.Value.Year))
                .ForMember(m => m.EndYear, cfg => cfg.MapFrom(src => src.PreventionOrRemovalProcedureFinishDate.Value.Year))
                ;
        }
    }
}
