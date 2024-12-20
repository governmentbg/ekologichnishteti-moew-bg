using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Common.Interfaces;
using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Application.Nomenclatures.Interfaces;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Hosting.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseNomenclatureController<T, TDto, TFilter> : ControllerBase
        where T : Nomenclature, IIncludeAll<T>, new()
        where TDto : IMapping<T, TDto>, new()
        where TFilter : BaseNomenclatureFilterDto<T>
    {
        protected readonly INomenclatureService<T> nomenclatureService;

        public BaseNomenclatureController(INomenclatureService<T> nomenclatureService)
        {
            this.nomenclatureService = nomenclatureService;
        }

        [HttpGet]
        public Task<SearchResultItemDto<T>> GetNomenclatures([FromQuery] TFilter filter)
            => this.nomenclatureService.GetNomenclaturesAsync(filter);

        [HttpGet("Select")]
        public Task<IEnumerable<TDto>> SelectNomenclatures([FromQuery] TFilter filter, [FromQuery] params int[] ids)
            => this.nomenclatureService.SelectNomenclaturesAsync<TFilter, TDto>(filter, ids);

        [HttpPost]
        public Task<T> AddNomenclature([FromBody] T model)
            => this.nomenclatureService.InsertNomenclatureAsync(model);

        [HttpPut("{id:int}")]
        public Task<T> UpdateNomenclature([FromRoute] int id, [FromBody] T model)
            => this.nomenclatureService.UpdateNomenclatureAsync(id, model);

        //[HttpDelete("{id:int}")]
        //public Task DeleteNomenclature([FromRoute] int id)
        //    => this.nomenclatureService.DeleteNomenclatureAsync(id);
    }
}
