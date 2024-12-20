using AutoMapper;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Data.Nomenclatures.Operators;
using Zopoesht.Data.Nomenclatures.Operators.Enums;

namespace Zopoesht.Application.Infrastructure.AutoMapperProfiles
{
    public class OperatorProfile : Profile
    {
        public OperatorProfile()
        {
            // Dto to Model
            this.CreateMap<OperatorDto, Operator>()
                .PreserveReferences()
                .ForMember(m => m.Name, cfg => cfg.MapFrom(src => src.Type == OperatorType.LegalEntity ?
                    src.LegalEntityName :
                    $"{src.FirstName} {src.MiddleName} {src.LastName}"))
                .ForMember(m => m.FirstName, cfg => cfg.MapFrom(src => src.Type == OperatorType.LegalEntity ? 
                    null : 
                    src.FirstName))
                .ForMember(m => m.MiddleName, cfg => cfg.MapFrom(src => src.Type == OperatorType.LegalEntity ?
                    null :
                    src.MiddleName))
                .ForMember(m => m.LastName, cfg => cfg.MapFrom(src => src.Type == OperatorType.LegalEntity ?
                    null :
                    src.LastName))
                .ForMember(m => m.LegalEntityName, cfg => cfg.MapFrom(src => src.Type == OperatorType.Person ?
                    null :
                    src.LegalEntityName))
                .ForMember(m => m.LegalEntityUic, cfg => cfg.MapFrom(src => src.Type == OperatorType.Person ?
                    null :
                    src.LegalEntityUic))
                .ForMember(m => m.Terrains, cfg => cfg.Ignore())
                .ForMember(m => m.ActivityOfferings, cfg => cfg.Ignore())
                .ForMember(m => m.Settlement, cfg => cfg.Ignore())
                ;

            this.CreateMap<OperatorCorrespondenceDto, OperatorCorrespondence>()
                .ForMember(m => m.Settlement, cfg => cfg.Ignore())
                ;

            this.CreateMap<TerrainDto, Terrain>()
                .ForMember(m => m.District, cfg => cfg.Ignore())
                .ForMember(m => m.Municipality, cfg => cfg.Ignore())
                .ForMember(m => m.Settlement, cfg => cfg.Ignore())
                ;

            this.CreateMap<ActivityOfferingDto, ActivityOffering>()
                .ForMember(m => m.Activity, cfg => cfg.Ignore())
                .ForMember(m => m.SubActivity, cfg => cfg.Ignore())
                .ForMember(m => m.Operator, cfg => cfg.Ignore())
                .ForMember(m => m.Terrain, cfg => cfg.Ignore())
                .ForMember(m => m.TerrainId, cfg => cfg.MapFrom(src => src.TerrainId <= 0 ?
                    null :
                    src.TerrainId))
                .ForMember(m => m.AuthorityRiosv, cfg => cfg.Ignore())
                .ForMember(m => m.AuthorityBasin, cfg => cfg.Ignore())
                ;

            // Model to Dto
            this.CreateMap<Operator, OperatorSearchResultDto>()
                .ForMember(m => m.Fullname, cfg => cfg.MapFrom(src =>  src.Type == OperatorType.Person ?
                                                   src.FirstName + " " + src.MiddleName + " " + src.LastName : 
                                                   src.LegalEntityName)
                                            );

            this.CreateMap<Operator, OperatorDto>()
                .ForMember(m => m.Fullname, cfg => cfg.MapFrom(src => src.Type == OperatorType.Person ?
                                                   src.FirstName + " " + src.MiddleName + " " + src.LastName :
                                                   src.LegalEntityName)
                                            )
                ;

            this.CreateMap<OperatorCorrespondence, OperatorCorrespondenceDto>()
                ;

            this.CreateMap<Terrain, TerrainDto>()
                .ForMember(m => m.Operator, cfg => cfg.Ignore())
            ;

            this.CreateMap<ActivityOffering, ActivityOfferingDto>()
                .ForMember(m => m.Operator, cfg => cfg.Ignore())
                ;
        }
    }
}
