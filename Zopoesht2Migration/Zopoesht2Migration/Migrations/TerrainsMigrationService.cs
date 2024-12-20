using Microsoft.EntityFrameworkCore;
using Zopoesht2Migration.DbContexts.MosvEcologicalDamage;
using Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Enums;
using Zopoesht2Migration.DbContexts.Zopoesht2;

namespace Zopoesht2Migration.Migrations
{
    public class TerrainsMigrationService
    {
        public static void Start()
        {
            using (var zopoesht2Context = new Zopoesht2Context())
            {
                using (var zoposht1Context = new MosvEcologicalDamageContext())
                {
                    Console.WriteLine("=== Starting terrains migration ===");
                    MigrateTerrains(zopoesht2Context, zoposht1Context);
                    Console.WriteLine("=== Finished terrains migration ===");
                }
            }
        }

        private static void MigrateTerrains(Zopoesht2Context zopoesht2Context, MosvEcologicalDamageContext zoposht1Context)
        {
            var operatorCommits = zoposht1Context.OperatorCommits
                .AsNoTracking()
                .Include(e => e.OperatorBasicPart.Entity)
                .Include(e => e.TerrainParts)
                   .ThenInclude(e => e.Entity)
                   .Where(e => (e.State == CommitState.Actual
                    || e.State == CommitState.Modification
                    || e.State == CommitState.Deleted)
                    && e.OperatorBasicPart != null)
                   .ToList();

            var operatorsIds = operatorCommits.Select(e => e.OperatorBasicPart.EntityId).ToList();

            var operators = zopoesht2Context.Operators.ToList();

            var terrainsToAdd = new List<DbContexts.Zopoesht2.Models.Terrain>();

            using (var zopoeshtTransaction = zopoesht2Context.Database.BeginTransaction())
            {
                foreach (var operatorCommit in operatorCommits)
                {
                    if (operatorCommit.OperatorBasicPart == null)
                    {
                        continue;
                    }

                    var terrainEntities = operatorCommit.TerrainParts.Select(e => e.Entity).ToList();


                    if (terrainEntities.Any())
                    {
                        var migratedOperator = operators
                            .SingleOrDefault(e => e.Migrationid == operatorCommit.OperatorBasicPart.EntityId);

                        if (migratedOperator != null)
                        {
                            foreach (var terrainEntity in terrainEntities)
                            {
                                var newTerrain = new DbContexts.Zopoesht2.Models.Terrain();

                                newTerrain = AddNewTerrain(terrainEntity, migratedOperator.Id);
                                terrainsToAdd.Add(newTerrain);
                            }
                        }
                    }
                }

                zopoesht2Context.Terrains.AddRange(terrainsToAdd);
                zopoesht2Context.SaveChanges();

                zopoeshtTransaction.Commit();
            }
        }

        private static DbContexts.Zopoesht2.Models.Terrain AddNewTerrain(DbContexts.MosvEcologicalDamage.Models.Terrain terrainEntity, int operatorId)
        {
            var newTerrain = new DbContexts.Zopoesht2.Models.Terrain
            {
                Name = terrainEntity.Name,
                Districtid = terrainEntity.DistrictId,
                Municipalityid = terrainEntity.MunicipalityId,
                Settlementid = terrainEntity.MunicipalityId,
                Address = terrainEntity.Address,
                Operatorid = operatorId,
                Namealt = null,
                Alias = null,
                Isactive = true,
                Version = 0,
                Migrationid = terrainEntity.Id
            };

            return newTerrain;
        }
    }
}
