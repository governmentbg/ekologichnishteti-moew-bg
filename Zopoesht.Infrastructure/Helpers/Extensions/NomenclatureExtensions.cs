using Zopoesht.Data.Common.Models;
using Zopoesht.Infrastructure.Helpers.Dtos;

namespace Zopoesht.Infrastructure.Helpers.Extensions
{
    public static class NomenclatureExtensions
    {
        public static NomenclatureDto ToNomenclatureDto(this Nomenclature nomenclature)
        {
            return nomenclature != null
                ? new NomenclatureDto
                {
                    Id = nomenclature.Id,
                    Name = nomenclature.Name,
                    NameAlt = nomenclature.NameAlt,
                    Alias = nomenclature.Alias
                }
                : null;
        }
    }
}
