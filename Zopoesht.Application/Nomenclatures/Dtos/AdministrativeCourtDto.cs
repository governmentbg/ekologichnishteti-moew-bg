using System.Linq.Expressions;
using Zopoesht.Application.Common.Interfaces;
using Zopoesht.Data.Nomenclatures.StateAdministration;
using Zopoesht.Data.Nomenclatures.StateAdministration.Enums;

namespace Zopoesht.Application.Nomenclatures.Dtos
{
    public class AdministrativeCourtDto : NomenclatureMapperDto<AdministrativeCourt>, IMapping<AdministrativeCourt, AdministrativeCourtDto>
    {
        public AdministrativeCourtType AdministrativeCourtType { get; set; }

        Expression<Func<AdministrativeCourt, AdministrativeCourtDto>> IMapping<AdministrativeCourt, AdministrativeCourtDto>.Map()
        {
            return e => new AdministrativeCourtDto
            {
                Id = e.Id,
                Name = e.Name,
                Alias = e.Alias,
                IsActive = e.IsActive,
                AdministrativeCourtType = e.AdministrativeCourtType
            };
        }
    }
}
