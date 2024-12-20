using AutoMapper;
using Zopoesht.Application.Applications.ApplicationsTwo.Dtos;
using Zopoesht.Application.Applications.ApplicationsTwo.Dtos.FinancialAssurance;
using Zopoesht.Application.Applications.Common.Dtos;
using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Data.Applications.ApplicationTwo;
using Zopoesht.Data.Applications.ApplicationTwo.FinancialAssurance;
using Zopoesht.Data.Nomenclatures.StateAdministration;
using Zopoesht.Infrastructure.Helpers.Dtos;


namespace Zopoesht.Application.Infrastructure.AutoMapperProfiles
{
    public class ApplicationTwoProfile : Profile
    {

        public ApplicationTwoProfile() 
        {
            // Dto to Model
            this.CreateMap<ApplicationTwoDto, ApplicationTwo>()
                .ForMember(m => m.Id, cfg => cfg.Ignore())
                .ForMember(m => m.Applicant, cfg => cfg.Ignore())
                .ForMember(m => m.Operator, cfg => cfg.Ignore())
                ;

            this.CreateMap<ApplicationFileDto, ApplicationTwoFile>()
                ;

            this.CreateMap<CodeDto, ApplicationTwoCode>()
                .ForMember(m => m.Id, cfg => cfg.Ignore())
                .ForMember(m => m.CodeId, cfg => cfg.MapFrom(src => src.Id))
                ;

            this.CreateMap<ActivityOfferingDto, ApplicationTwoActivityOffering>()
                .ForMember(m => m.Id, cfg => cfg.Ignore())
                .ForMember(m => m.ActivityOfferingId, cfg => cfg.MapFrom(src => src.Id))
                ;

            this.CreateMap<ApplicationAuthorityDto, ApplicationTwoAuthority>()
                .ForMember(m => m.Authority, cfg => cfg.Ignore())
                ;

            this.CreateMap<ApplicationTwoAdministrativeCourtDto, ApplicationTwoAdministrativeCourt>()
                .ForMember(m => m.AdministrativeCourt, cfg => cfg.Ignore())
                ;

            this.CreateMap<ApplicationTwoProsecutorDto, ApplicationTwoProsecutor>()
                .ForMember(m => m.Prosecutor, cfg => cfg.Ignore())
                ;

            this.CreateMap<ApplicationTwoMinistryOfInteriorDto, ApplicationTwoMinistryOfInterior>()
                .ForMember(m => m.MinistryOfInterior, cfg => cfg.Ignore())
                ;

            this.CreateMap<InsurancePolicyDto, InsurancePolicy>()
                ;

            this.CreateMap<BankGuaranteeDto, BankGuarantee>()
               ;

            this.CreateMap<MortgageDto, Mortgage>()
               ;

            this.CreateMap<StakeDto, Stake>()
               ;


            // Model to Dto
            this.CreateMap<ApplicationTwo, ApplicationTwoDto>()
                .ForMember(m => m.HasInsurancePolicy, cfg => cfg.MapFrom(src => src.InsurancePolicy != null))
                .ForMember(m => m.HasBankGuarantee, cfg => cfg.MapFrom(src => src.BankGuarantee != null))
                .ForMember(m => m.HasMortgage, cfg => cfg.MapFrom(src => src.Mortgage != null))
                .ForMember(m => m.HasStake, cfg => cfg.MapFrom(src => src.Stake != null))
                ;

            this.CreateMap<ApplicationTwoFile, ApplicationFileDto>()
                ;

            this.CreateMap<ApplicationTwoCode, CodeDto>()
                .ForMember(m => m.Id, cfg => cfg.MapFrom(src => src.CodeId))
                .ForMember(m => m.Name, cfg => cfg.MapFrom(src => src.Code.Name))
                ;

            this.CreateMap<ApplicationTwoActivityOffering, ActivityOfferingDto>()
                .ForMember(m => m.Id, cfg => cfg.MapFrom(src => src.ActivityOfferingId))
                .ForMember(m => m.ActivityId, cfg => cfg.MapFrom(src => src.ActivityOffering.ActivityId))
                .ForMember(m => m.Activity, cfg => cfg.MapFrom(src => src.ActivityOffering.Activity))
                .ForMember(m => m.SubActivityId, cfg => cfg.MapFrom(src => src.ActivityOffering.SubActivityId))
                .ForMember(m => m.SubActivity, cfg => cfg.MapFrom(src => src.ActivityOffering.SubActivity))
                .ForMember(m => m.TerrainId, cfg => cfg.MapFrom(src => src.ActivityOffering.TerrainId))
                .ForMember(m => m.Terrain, cfg => cfg.MapFrom(src => src.ActivityOffering.Terrain))
                .ForMember(m => m.AuthorityRiosvId, cfg => cfg.MapFrom(src => src.ActivityOffering.AuthorityRiosvId))
                .ForMember(m => m.AuthorityRiosv, cfg => cfg.MapFrom(src => src.ActivityOffering.AuthorityRiosv))
                .ForMember(m => m.AuthorityBasinId, cfg => cfg.MapFrom(src => src.ActivityOffering.AuthorityBasinId))
                .ForMember(m => m.AuthorityBasin, cfg => cfg.MapFrom(src => src.ActivityOffering.AuthorityBasin))
                ;

            this.CreateMap<ApplicationTwoAuthority, ApplicationAuthorityDto>()
                ;

            this.CreateMap<ApplicationTwoAdministrativeCourt, ApplicationTwoAdministrativeCourtDto>()
                ;

            this.CreateMap<ApplicationTwoProsecutor, ApplicationTwoProsecutorDto>()
                ;

            this.CreateMap<ApplicationTwoMinistryOfInterior, ApplicationTwoMinistryOfInteriorDto>()
                ;

            this.CreateMap<AdministrativeCourt, AdministrativeCourtDto>()
               ;

            this.CreateMap<Prosecutor, NomenclatureDto>()
                ;

            this.CreateMap<MinistryOfInterior, NomenclatureDto>()
                ;

            this.CreateMap<InsurancePolicy, InsurancePolicyDto>()
                ;

            this.CreateMap<BankGuarantee, BankGuaranteeDto>()
                ;

            this.CreateMap<Mortgage, MortgageDto>()
                ;

            this.CreateMap<Stake, StakeDto>()
                ;
        }
    }
}
