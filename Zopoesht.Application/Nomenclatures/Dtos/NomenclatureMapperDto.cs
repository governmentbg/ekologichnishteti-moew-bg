using System.Linq.Expressions;
using Zopoesht.Application.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Application.Nomenclatures.Dtos
{
    public class NomenclatureMapperDto<TNomenclature> : IMapping<TNomenclature, NomenclatureMapperDto<TNomenclature>>
        where TNomenclature : Nomenclature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }

        public bool IsActive { get; set; }

        public virtual Expression<Func<TNomenclature, NomenclatureMapperDto<TNomenclature>>> Map()
        {
            return e => new NomenclatureMapperDto<TNomenclature>
            {
                Id = e.Id,
                Name = e.Name,
                Alias = e.Alias,
                IsActive = e.IsActive
            };
        }
    }
}