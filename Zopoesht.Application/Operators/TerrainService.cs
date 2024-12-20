using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Application.Operators.Interfaces;
using Zopoesht.Data.Nomenclatures.Operators;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces.Contexts;

namespace Zopoesht.Application.Operators
{
    public class TerrainService : ITerrainService
    {
        private readonly IAppDbContext context;
        private readonly IMapper mapper;
        private readonly DomainValidationService validation;

        public TerrainService(
            IAppDbContext context,
            IMapper mapper,
            DomainValidationService validation)
        {
            this.context = context;
            this.mapper = mapper;
            this.validation = validation;
        }

        public async Task Add(TerrainDto model, CancellationToken cancellationToken)
        {
            model.ValidateProperties(validation);

            await EnsureOperatorExists(model.OperatorId);

            var terrain = mapper.Map<TerrainDto, Terrain>(model);

            await context.Set<Terrain>().AddAsync(terrain);
            await context.SaveChangesAsync();
        }

        public async Task Update(TerrainDto model, CancellationToken cancellationToken)
        {
            model.ValidateProperties(validation);

            await EnsureOperatorExists(model.OperatorId);

            var terrain = await GetTerrain(model.Id);

            UpdateTerrain(model, terrain);

            await context.SaveChangesAsync();
        }

        private async Task EnsureOperatorExists(int operatorId)
        {
            var operatorData = await context.Set<Operator>()
                .SingleOrDefaultAsync(o => o.Id == operatorId);

            if (operatorData == null)
            {
                validation.ThrowErrorMessage(OperatorErrorCode.Operator_DoesNotExist);
            }
        }

        private async Task<Terrain> GetTerrain(int id)
        {
            var terrain = await context.Set<Terrain>()
                .SingleOrDefaultAsync(t => t.Id == id);

            if (terrain == null)
            {
                validation.ThrowErrorMessage(OperatorErrorCode.Operator_TerrainDoesNotExist);
            }

            return terrain;
        }

        private void UpdateTerrain(TerrainDto modifiedTerrain, Terrain terrain)
        {
            terrain.Name = modifiedTerrain.Name;
            terrain.DistrictId = modifiedTerrain.DistrictId;
            terrain.MunicipalityId = modifiedTerrain.MunicipalityId;
            terrain.SettlementId = modifiedTerrain.SettlementId;
            terrain.Address = modifiedTerrain.Address;
        }
    }
}
