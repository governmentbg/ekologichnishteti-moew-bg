using AutoMapper;
using Zopoesht.Application.Applications.ApplicationsOne.Dtos;
using Zopoesht.Application.Applications.Common.Dtos;
using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Data.Applications.ApplicationOne;
using Zopoesht.Data.Applications.ApplicationOne.Enums;
using Zopoesht.Data.Applications.ApplicationTwo;
using Zopoesht.Data.Common.Enums;
using Zopoesht.Data.Nomenclatures;
using Zopoesht.Infrastructure.Helpers.Extensions;
using Zopoesht.Data.Nomenclatures.Operators;
using Zopoesht.Infrastructure.Helpers.Dtos;

namespace Zopoesht.Application.Infrastructure.AutoMapperProfiles
{
    public class ApplicationOneProfile : Profile
    {
        public ApplicationOneProfile()
        {
            // Dto to Model
            this.CreateMap<ApplicationOneDto, ApplicationOne>()
                .ForMember(m => m.Id, cfg => cfg.Ignore())
                .ForMember(m => m.ApplicationOneLegalEntity,
                    cfg => cfg.MapFrom(src =>
                        src.ApplicationOneType == ApplicationOneType.ReportedDamage ? src.ApplicationOneLegalEntity : null))
                .ForMember(m => m.ApplicationOneThreat,
                    cfg => cfg.MapFrom(src =>
                        src.ApplicationOneType == ApplicationOneType.Threat ? src.ApplicationOneThreat : null))
                 .ForMember(m => m.ApplicationOneDamage,
                    cfg => cfg.MapFrom(src =>
                        src.ApplicationOneType == ApplicationOneType.Damage ? src.ApplicationOneDamage : null))
                ;

            this.CreateMap<ApplicantDto, Applicant>()
                 .ForMember(m => m.Authority, cfg => cfg.Ignore())
                 .ForMember(m => m.Operator, cfg => cfg.Ignore())
                 .ForMember(m => m.AuthorityId, cfg => cfg.MapFrom(src => src.Authority.Id))
                 .ForMember(m => m.Individual, cfg => cfg.MapFrom(src => 
                    src.ApplicantType == ApplicantType.Individual ? src.Individual : null ))
                 .ForMember(m => m.LegalEntity, cfg => cfg.MapFrom(src =>
                    src.ApplicantType == ApplicantType.LegalEntity || src.ApplicantType == ApplicantType.NonGovernmentalOrganization ? src.LegalEntity : null))
                ;

            this.CreateMap<ApplicationOneBasicDto, ApplicationOneBasic>()
                .ForMember(m => m.Id, cfg => cfg.Ignore())
                .ForMember(m => m.ApplicationOne, cfg => cfg.Ignore())
                .ForMember(m => m.CulpritCountry, cfg => cfg.Ignore())
                .ForMember(m => m.Operator, cfg => cfg.Ignore())
                .ForMember(m => m.CulpritCountryId, cfg => cfg.MapFrom(src => src.HasInternationalElement ?  src.CulpritCountryId : null))
                .ForMember(m => m.InternationalElementDescription, cfg => cfg.MapFrom(src => src.HasInternationalElement ?  src.InternationalElementDescription : null))
                ;

            this.CreateMap<ApplicationOneLegalEntityDto, ApplicationOneLegalEntity>()
                ;

            this.CreateMap<ApplicationOneThreatDto, ApplicationOneThreat>()
                ;

            this.CreateMap<ApplicationOneDamageDto, ApplicationOneDamage>()
                ; 
            
            this.CreateMap<AuthorityDto, Authority>()
                ;

            this.CreateMap<OperatorDto, Operator>()
                .ForMember(m => m.Settlement, cfg => cfg.Ignore())
                .ForMember(m => m.OperatorCorrespondence, cfg => cfg.Ignore())
               ;

            this.CreateMap<LegalEntityDto, LegalEntity>()
                ;

            this.CreateMap<IndividualDto, Individual>()
                ;

            this.CreateMap<ApplicationFileDto, ApplicationOneFile>()
                ;

            this.CreateMap<AuthorityDto, ApplicationOneTerritorialRange>()
                .ForMember(m => m.Id, cfg => cfg.Ignore())
                .ForMember(m => m.AuthorityId, cfg => cfg.MapFrom(src => src.Id))
                ;

            this.CreateMap<NomenclatureDto, ApplicationOneAffectedCountry>()
                .ForMember(m => m.Id, cfg => cfg.Ignore())
                .ForMember(m => m.CountryId, cfg => cfg.MapFrom(src => src.Id))
                ;

            this.CreateMap<ActivityOfferingDto, ApplicationOneActivityOffering>()
                .ForMember(m => m.Id, cfg => cfg.Ignore())
                .ForMember(m => m.ActivityOfferingId, cfg => cfg.MapFrom(src => src.Id))
                ;

            this.CreateMap<ApplicationAuthorityDto, ApplicationOneAuthority>()
                .ForMember(m => m.Authority, cfg => cfg.Ignore())
                ;

            // Model to Dto
            this.CreateMap<ApplicationOne, ApplicationOneDto>()
                ;

            this.CreateMap<Applicant, ApplicantDto>()
                ;

            this.CreateMap<ApplicationOneBasic, ApplicationOneBasicDto>()
                .ForMember(m => m.CulpritCountry, cfg => cfg.MapFrom(src => src.CulpritCountry.ToNomenclatureDto()))
                ;

            this.CreateMap<ApplicationOneLegalEntity, ApplicationOneLegalEntityDto>()
                ;

            this.CreateMap<ApplicationOneThreat, ApplicationOneThreatDto>()
                ;

            this.CreateMap<ApplicationOneDamage, ApplicationOneDamageDto>()
                ;

            this.CreateMap<Authority, AuthorityDto>()
                ;

            this.CreateMap<Operator, OperatorDto>()
                ;

            this.CreateMap<LegalEntity, LegalEntityDto>()
                ;

            this.CreateMap<Individual, IndividualDto>()
                ;

            this.CreateMap<ApplicationOne, ApplicationOneSearchResultDto>()
                .ForMember(m => m.ApplicationAuthorities, cfg => cfg.MapFrom(src => src.ApplicationTwos.OrderByDescending(s=> s.Id).FirstOrDefault().ApplicationTwoAuthorities))
                .ForMember(m => m.ApplicantType, cfg => cfg.MapFrom(src => src.Applicant.ApplicantType))
                .ForMember(m => m.ApplicantName, cfg => cfg.MapFrom(src => GetApplicantName(src.Applicant)))
                .ForMember(m => m.BasicName, cfg => cfg.MapFrom(src => src.ApplicationOneBasic.Name))
                .ForMember(m => m.HasApplicationTwo, cfg => cfg.MapFrom(src => src.ApplicationTwos.Any()))
                .ForMember(m => m.ApplicationTwoCommitState, cfg => cfg.MapFrom(src => GetApplicationTwoCommitState(src.ApplicationTwos)))
                ;

            this.CreateMap<ApplicationOneFile, ApplicationFileDto>()
                ;

            this.CreateMap<ApplicationOneTerritorialRange, AuthorityDto>()
                .ForMember(m => m.Id, cfg => cfg.MapFrom(src => src.AuthorityId))
                .ForMember(m => m.Name, cfg => cfg.MapFrom(src => src.Authority.Name))
                ;

            this.CreateMap<ApplicationOneAffectedCountry, NomenclatureDto>()
                .ForMember(m => m.Id, cfg => cfg.MapFrom(src => src.CountryId))
                .ForMember(m => m.Name, cfg => cfg.MapFrom(src => src.Country.Name))
                ;

            this.CreateMap<ApplicationOneActivityOffering, ActivityOfferingDto>()
                .ForMember(m => m.Id, cfg => cfg.MapFrom(src => src.ActivityOfferingId))
                .ForMember(m => m.ActivityId, cfg => cfg.MapFrom(src => src.ActivityOffering.ActivityId))
                .ForMember(m => m.Activity, cfg => cfg.MapFrom(src => src.ActivityOffering.Activity))
                .ForMember(m => m.SubActivityId, cfg => cfg.MapFrom(src => src.ActivityOffering.SubActivityId))
                .ForMember(m => m.SubActivity, cfg => cfg.MapFrom(src => src.ActivityOffering.SubActivity))
                .ForMember(m => m.OperatorId, cfg => cfg.MapFrom(src => src.ActivityOffering.OperatorId))
                .ForMember(m => m.Operator, cfg => cfg.MapFrom(src => src.ActivityOffering.Operator))
                .ForMember(m => m.TerrainId, cfg => cfg.MapFrom(src => src.ActivityOffering.TerrainId))
                .ForMember(m => m.Terrain, cfg => cfg.MapFrom(src => src.ActivityOffering.Terrain))
                .ForMember(m => m.AuthorityRiosvId, cfg => cfg.MapFrom(src => src.ActivityOffering.AuthorityRiosvId))
                .ForMember(m => m.AuthorityRiosv, cfg => cfg.MapFrom(src => src.ActivityOffering.AuthorityRiosv))
                .ForMember(m => m.AuthorityBasinId, cfg => cfg.MapFrom(src => src.ActivityOffering.AuthorityBasinId))
                .ForMember(m => m.AuthorityBasin, cfg => cfg.MapFrom(src => src.ActivityOffering.AuthorityBasin))
                ;

            this.CreateMap<ApplicationOneAuthority, ApplicationAuthorityDto>()
                ;
        }

        private static string GetApplicantName(Applicant applicant)
        {
            switch (applicant.ApplicantType)
            {
                case ApplicantType.LegalEntity:
                case ApplicantType.NonGovernmentalOrganization:
                    return applicant.LegalEntity.LegalEntityName;
                case ApplicantType.Individual:
                    return $"{applicant.Individual.FirstName} {applicant.Individual.LastName}";
                case ApplicantType.Operator:
                    return applicant.Operator.Name;
                case ApplicantType.Authority:
                    return applicant.Authority.Name;
                default:
                    return string.Empty;
            }
        }

        private static CommitState? GetApplicationTwoCommitState(List<ApplicationTwo> applicationTwos)
        {
            var latestAppTwo = applicationTwos
                .OrderByDescending(a => a.CreateDate)
                .FirstOrDefault();

            return latestAppTwo != null ? 
                latestAppTwo.CommitState : 
                null;
        }
    }
}
