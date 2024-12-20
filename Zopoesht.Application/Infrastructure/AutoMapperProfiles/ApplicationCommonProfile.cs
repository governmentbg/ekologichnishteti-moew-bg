using AutoMapper;
using Zopoesht.Application.Applications.Common.Dtos;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Data.Applications.Common.Models;
using Zopoesht.Data.Nomenclatures.Operators;

namespace Zopoesht.Application.Infrastructure.AutoMapperProfiles
{
    public class ApplicationCommonProfile : Profile
    {
        public ApplicationCommonProfile()
        {
            // Dto to Model

            this.CreateMap<ActivityOfferingDto, ActivityOffering>()
                  .ForMember(m => m.Id, cfg => cfg.Ignore())
                  .ForMember(m => m.Activity, cfg => cfg.Ignore())
                  .ForMember(m => m.Operator, cfg => cfg.Ignore())
                  .ForMember(m => m.SubActivity, cfg => cfg.Ignore())
                  .ForMember(m => m.AuthorityRiosv, cfg => cfg.Ignore())
                  .ForMember(m => m.AuthorityBasin, cfg => cfg.Ignore())
                  .ForMember(m => m.Terrain, cfg => cfg.Ignore())
               ;

            // Model to Dto

            this.CreateMap<ApplicationHistory, ApplicationHistoryDto>()
                ;

            this.CreateMap<ApplicationLock, ApplicationLockDto>()
                .ForMember(m => m.UserId, cfg => cfg.MapFrom(src => src.UserId))
                .ForMember(m => m.UserFullName, cfg => cfg.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(m => m.UserAuthorityName, cfg => cfg.MapFrom(src => src.User.Authority.Name))
                ;

            this.CreateMap<ActivityOffering, ActivityOfferingDto>()
                ;
        }
    }
}
