using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Application.Operators.Interfaces;

namespace Zopoesht.Hosting.Controllers.Operators
{
    [ApiController]
    [Route("api/[controller]")]
    public class TerrainController : ControllerBase
    {
        private readonly ITerrainService terrainService;

        public TerrainController(ITerrainService terrainService)
        {
            this.terrainService = terrainService;
        }


        [HttpPost]
        public async Task Add([FromBody] TerrainDto model, CancellationToken cancellationToken)
            => await terrainService.Add(model, cancellationToken);

        [HttpPut]
        public async Task Update([FromBody] TerrainDto model, CancellationToken cancellationToken)
            => await terrainService.Update(model, cancellationToken);
    }
}
