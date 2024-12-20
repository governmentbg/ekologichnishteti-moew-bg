using System.Linq.Expressions;
using Zopoesht.Application.Common.Interfaces;
using Zopoesht.Data.Nomenclatures.Settlements;

namespace Zopoesht.Application.Nomenclatures.Dtos.Settlements
{
    public class MunicipalityMapperDto : NomenclatureMapperDto<Municipality>, IMapping<Municipality, MunicipalityMapperDto>
    {
        public int DistrictId { get; set; }
        public District District { get; set; }

        Expression<Func<Municipality, MunicipalityMapperDto>> IMapping<Municipality, MunicipalityMapperDto>.Map()
        {
            return e => new MunicipalityMapperDto
            {
                Id = e.Id,
                Name = e.Name,
                Alias = e.Alias,
                IsActive = e.IsActive,
                District = e.District,
                DistrictId = e.DistrictId
            };
        }
    }
}
