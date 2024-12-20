using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Application.Operators.Interfaces;

namespace Zopoesht.Hosting.Controllers.Operators
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperatorController : ControllerBase
    {
        private readonly IOperatorService operatorService;

        public OperatorController(IOperatorService operatorService)
        {
            this.operatorService = operatorService;
        }

        [HttpPost]
        public async Task<SearchResultItemDto<OperatorSearchResultDto>> GetFiltered([FromBody] OperatorSearchFilterDto filter, CancellationToken cancellationToken)
            => await operatorService.GetFiltered(filter, cancellationToken);

        [HttpGet("{id:int}")]
        public async Task<OperatorDto> GetById([FromRoute] int id, CancellationToken cancellationToken)
           => await operatorService.GetById(id, cancellationToken);

        [HttpPost("Add")]
        public async Task Add([FromBody] OperatorDto model, CancellationToken cancellationToken)
            => await operatorService.Add(model, cancellationToken);

        [HttpPut]
        public async Task Update([FromBody] OperatorDto model, CancellationToken cancellationToken)
            => await operatorService.Update(model, cancellationToken);
    }
}
