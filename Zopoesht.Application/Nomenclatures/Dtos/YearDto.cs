using System.Linq.Expressions;
using Zopoesht.Application.Common.Interfaces;
using Zopoesht.Data.Nomenclatures;

namespace Zopoesht.Application.Nomenclatures.Dtos
{
    public class YearDto : NomenclatureMapperDto<Year>, IMapping<Year, YearDto>
    {
        Expression<Func<Year, YearDto>> IMapping<Year, YearDto>.Map()
        {
            return e => new YearDto
            {
                Id = e.Id,
                Name = e.Name,
                Alias = e.Alias,
                IsActive = e.IsActive
            };
        }
    }
}
