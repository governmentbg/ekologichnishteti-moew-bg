using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Application.Operators.Interfaces;

namespace Zopoesht.Hosting.Controllers.Operators
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityOfferingController : ControllerBase
    {
        private readonly IActivityOfferingService activityOfferingService;

        public ActivityOfferingController(IActivityOfferingService activityOfferingService)
        {
            this.activityOfferingService = activityOfferingService;
        }


        [HttpPost]
        public async Task Add([FromBody] ActivityOfferingDto model, CancellationToken cancellationToken)
            => await activityOfferingService.Add(model, cancellationToken);

        [HttpPut]
        public async Task Update([FromBody] ActivityOfferingDto model, CancellationToken cancellationToken)
            => await activityOfferingService.Update(model, cancellationToken);
    }
}
