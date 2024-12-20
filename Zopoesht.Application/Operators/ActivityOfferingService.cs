using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Application.Operators.Interfaces;
using Zopoesht.Data.Nomenclatures;
using Zopoesht.Data.Nomenclatures.Operators;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces.Contexts;

namespace Zopoesht.Application.Operators
{
    public class ActivityOfferingService : IActivityOfferingService
    {
        private readonly IAppDbContext context;
        private readonly IMapper mapper;
        private readonly DomainValidationService validation;

        public ActivityOfferingService(
            IAppDbContext context,
            IMapper mapper,
            DomainValidationService validation)
        {
            this.context = context;
            this.mapper = mapper;
            this.validation = validation;
        }

        public async Task Add(ActivityOfferingDto model, CancellationToken cancellationToken)
        {
            model.ValidateProperties(validation);

            await EnsureOperatorExistsAndHaveTerrain(model.OperatorId, model.TerrainId ?? 0);

            await EnsureSubActivityExists(model.SubActivityId, model.ActivityId);

            var activityOffering = mapper.Map<ActivityOfferingDto, ActivityOffering>(model);

            await context.Set<ActivityOffering>().AddAsync(activityOffering);
            await context.SaveChangesAsync();
        }

        public async Task Update(ActivityOfferingDto model, CancellationToken cancellationToken)
        {
            model.ValidateProperties(validation);

            await EnsureOperatorExistsAndHaveTerrain(model.OperatorId, model.TerrainId ?? 0);

            await EnsureSubActivityExists(model.SubActivityId, model.ActivityId);

            var activityOffering = await GetActivityOffering(model.Id);

            UpdateActivtyOffering(model, activityOffering);

            await context.SaveChangesAsync();
        }

        private async Task EnsureOperatorExistsAndHaveTerrain(int operatorId, int terrainId)
        {
            var operatorData = await context.Set<Operator>()
                .Include(o => o.Terrains)
                .SingleOrDefaultAsync(o => o.Id == operatorId);

            if (operatorData == null)
            {
                validation.ThrowErrorMessage(OperatorErrorCode.Operator_DoesNotExist);
            }

            if (terrainId > 0)
            { 
                if (!operatorData.Terrains.Select(t => t.Id).Contains(terrainId))
                {
                    validation.ThrowErrorMessage(OperatorErrorCode.Operator_TerrainDoesNotBelongToTheOperator);
                }
            }
        }

        private async Task EnsureSubActivityExists(int subActivityId, int activityId)
        {
            var subActivity = await context.Set<SubActivity>()
                .SingleOrDefaultAsync(s => s.Id == subActivityId);

            if (subActivity == null)
            {
                validation.ThrowErrorMessage(OperatorErrorCode.Operator_SubActivityDoesNotExist);
            }

            if (subActivity.ActivityId != activityId)
            {
                validation.ThrowErrorMessage(OperatorErrorCode.Operator_SubActivityDoesNotBelongToTheActivity);
            }
        }

        private async Task<ActivityOffering> GetActivityOffering(int id)
        {
            var activityOffering = await context.Set<ActivityOffering>()
                .SingleOrDefaultAsync(a => a.Id == id);

            if (activityOffering == null)
            {
                validation.ThrowErrorMessage(OperatorErrorCode.Operator_ActivityOfferingDoesNotExist);
            }

            return activityOffering;
        }

        private void UpdateActivtyOffering(ActivityOfferingDto modifiedActivityOffering, ActivityOffering activityOffering)
        {
            activityOffering.ActivityId = modifiedActivityOffering.ActivityId;
            activityOffering.SubActivityId = modifiedActivityOffering.SubActivityId;
            activityOffering.TerrainId = modifiedActivityOffering.TerrainId;
            activityOffering.AuthorityRiosvId = modifiedActivityOffering.AuthorityRiosvId;
            activityOffering.AuthorityBasinId = modifiedActivityOffering.AuthorityBasinId;
        }
    }
}
