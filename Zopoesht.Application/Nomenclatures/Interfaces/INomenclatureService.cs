using System.Linq.Expressions;
using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Common.Interfaces;
using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Data.Common.Models;
using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Application.Nomenclatures.Interfaces
{
    public interface INomenclatureService<TNomenclature> : ITransientService
        where TNomenclature : Nomenclature
    {
        Task<SearchResultItemDto<TNomenclature>> GetNomenclaturesAsync<TFilter>(TFilter filter)
            where TFilter : BaseNomenclatureFilterDto<TNomenclature>;

        Task<IEnumerable<TDto>> SelectNomenclaturesAsync<TFilter, TDto>(TFilter filter, params int[] ids)
            where TFilter : BaseNomenclatureFilterDto<TNomenclature>
            where TDto : IMapping<TNomenclature, TDto>, new();

        Task<TNomenclature> InsertNomenclatureAsync(TNomenclature model);

        Task<TNomenclature> GetSingleOrDefaultNomenclatureAsync(Expression<Func<TNomenclature, bool>> predicate);

        Task<TNomenclature> UpdateNomenclatureAsync(int id, TNomenclature model);

        //Task DeleteNomenclatureAsync(int id);
    }
}