using AutoMapper;
using System.Globalization;
using Zopoesht.Application.AdministrativeExpenses.Dtos;
using Zopoesht.Data.AdministrativeExpenses;
using Zopoesht.Data.Nomenclatures;

namespace Zopoesht.Application.Infrastructure.AutoMapperProfiles
{
    public class AdministrativeExpensesProfile : Profile
    {
        public AdministrativeExpensesProfile()
        {
            // Dto to Model
            this.CreateMap<PeriodDto, Period>()
                .ForMember(m => m.AnnualAdministrativeExpenses, cfg => cfg.Ignore())
                .ForMember(m => m.Name, cfg => cfg.MapFrom(src => GetPeriodName(src.StartDate, src.EndDate)))
                ;

            this.CreateMap<AnnualAdministrativeExpensesDto, AnnualAdministrativeExpenses>()
                .ForMember(m => m.Id, cfg => cfg.Ignore())
                .ForMember(m => m.Authority, cfg => cfg.Ignore())
                ;

            // Model to Dto
            this.CreateMap<Period, PeriodDto>()
                .ForMember(m => m.HasAnnualAdministrativeExpenses, cfg => cfg.MapFrom(src => src.AnnualAdministrativeExpenses.Any()))
                ;

            this.CreateMap<AnnualAdministrativeExpenses, AnnualAdministrativeExpensesDto>()
                ;

            this.CreateMap<AnnualAdministrativeExpensesHistory, AnnualAdministrativeExpensesHistoryDto>()
                .ForMember(m => m.UserAuthority, cfg => cfg.MapFrom(src => src.User.Authority))
                .ForMember(m => m.UserFullName, cfg => cfg.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(m => m.AnnualAdministrativeExpenseAmount, cfg => cfg.MapFrom(src => src.AnnualAdministrativeExpense.Amount))
                ;
        }

        private static string GetPeriodName(DateTime? start, DateTime? end)
        {
            if (start == null || end == null)
            {
                return string.Empty;
            }

            var startDate = start.Value;
            var endDate = end.Value;

            string startString = startDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            string endString = endDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);

            return $"{startString} г. - {endString} г.";
        }
    }
}
