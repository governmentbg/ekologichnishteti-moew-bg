using System.Linq.Expressions;
using Zopoesht.Application.Common.Interfaces;
using Zopoesht.Data.Nomenclatures.Settlements;

namespace Zopoesht.Application.Nomenclatures.Dtos.Settlements
{
    public class SettlementMapperDto : NomenclatureMapperDto<Settlement>, IMapping<Settlement, SettlementMapperDto>
    {
        public int DistrictId { get; set; }
        public District District { get; set; }
        public int MunicipalityId { get; set; }
        public Municipality Municipality { get; set; }

        Expression<Func<Settlement, SettlementMapperDto>> IMapping<Settlement, SettlementMapperDto>.Map()
        {
            return e => new SettlementMapperDto
            {
                Id = e.Id,
                Name = e.Name,
                Alias = e.Alias,
                IsActive = e.IsActive,
                District = e.District,
                DistrictId = e.DistrictId,
                Municipality = e.Municipality,
                MunicipalityId = e.MunicipalityId
            };
        }
    }
}
