using System.Linq.Expressions;
using Zopoesht.Application.Common.Interfaces;
using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Data.Nomenclatures.Operators;
using Zopoesht.Data.Nomenclatures.Settlements;

namespace Zopoesht.Application.Operators.Dtos
{
    public class TerrainNomenclatureMapperDto : NomenclatureMapperDto<Terrain>, IMapping<Terrain, TerrainNomenclatureMapperDto>
    {
        public int OperatorId { get; set; }
        public Operator Operator { get; set; }

        public int? DistrictId { get; set; }
        public District District { get; set; }

        public int? MunicipalityId { get; set; }
        public Municipality Municipality { get; set; }

        public int? SettlementId { get; set; }
        public Settlement Settlement { get; set; }

        public string Address { get; set; }

        Expression<Func<Terrain, TerrainNomenclatureMapperDto>> IMapping<Terrain, TerrainNomenclatureMapperDto>.Map()
        {
            return e => new TerrainNomenclatureMapperDto
            {
                Id = e.Id,
                Name = e.Name,
                Alias = e.Alias,
                IsActive = e.IsActive,
                OperatorId = e.OperatorId,
                Operator = e.Operator,
                DistrictId = e.DistrictId,
                District = e.District,
                MunicipalityId = e.MunicipalityId,
                Municipality = e.Municipality,
                SettlementId = e.SettlementId,
                Settlement = e.Settlement,
                Address = e.Address
            };
        }
    }
}
