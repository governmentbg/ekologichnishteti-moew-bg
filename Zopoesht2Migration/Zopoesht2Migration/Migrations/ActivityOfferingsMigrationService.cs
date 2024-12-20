using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zopoesht2Migration.DbContexts.MosvEcologicalDamage;
using Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Enums;
using Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Models;
using Zopoesht2Migration.DbContexts.Zopoesht2;
using Zopoesht2Migration.DbContexts.Zopoesht2.Models;

namespace Zopoesht2Migration.Migrations
{
    public class ActivityOfferingsMigrationService
    {
        public static void Start()
        {
            using (var zopoesht2Context = new Zopoesht2Context())
            {
                using (var zoposht1Context = new MosvEcologicalDamageContext())
                {
                    Console.WriteLine("=== Starting activity offerings migration ===");
                    MigrateActivityOfferings(zopoesht2Context, zoposht1Context);
                    Console.WriteLine("=== Finished activity offerings migration ===");
                }
            }
        }

        private static void MigrateActivityOfferings(Zopoesht2Context zopoesht2Context, MosvEcologicalDamageContext zoposht1Context)
        {
            var operatorCommits = zoposht1Context.OperatorCommits
                .AsNoTracking()
                 .Include(e => e.OperatorBasicPart.Entity)
                 .Include(e => e.OperatorCorrespondenceDataPart.Entity)
                 .Include(e => e.ActivityParts)
                    .ThenInclude(e => e.Entity)
                 .Include(e => e.ActivityParts)
                    .ThenInclude(e => e.Entity.ActivityType)
                 .Include(e => e.ActivityParts)
                    .ThenInclude(e => e.Entity.AdministrativeUnitBasein)
                 .Include(e => e.ActivityParts)
                    .ThenInclude(e => e.Entity.AdministrativeUnitRiosv)
                 .Where(e => (e.State == CommitState.Actual
                     || e.State == CommitState.Modification
                     || e.State == CommitState.Deleted)
                     && e.OperatorBasicPart != null)
            .ToList();

            var operatorsIds = operatorCommits.Select(e => e.OperatorBasicPart.EntityId).ToList();

            var operators = zopoesht2Context.Operators
                .AsNoTracking()
                .ToList();

            var authorities = zopoesht2Context.Authorities
                .AsNoTracking()
                .Where(e => e.Migrationid != null)
                .ToList();

            var activities = zopoesht2Context.Activities
                .AsNoTracking()
                .ToList();

            var subActivities = zopoesht2Context.Subactivities
                .AsNoTracking()
                .ToList();

            var terrains = zopoesht2Context.Terrains
                .AsNoTracking()
                .ToList();

            var activityOfferingsToAdd = new List<Activityoffering>();

            using (var zopoeshtTransaction = zopoesht2Context.Database.BeginTransaction())
            {
                foreach (var operatorCommit in operatorCommits)
                {
                    if (operatorCommit.OperatorBasicPart == null)
                    {
                        continue;
                    }

                    var activityEntities = operatorCommit.ActivityParts.Select(e => e.Entity).ToList();

                    if (activityEntities.Any())
                    {
                        var migratedOperator = operators
                            .SingleOrDefault(e => e.Migrationid == operatorCommit.OperatorBasicPart.EntityId);

                        if (migratedOperator != null)
                        {
                            foreach (var activityEntity in activityEntities)
                            {
                                var newActivityOffering = new Activityoffering();

                                newActivityOffering = AddNewActivityOffering(activityEntity, migratedOperator.Id, activities, subActivities, authorities, terrains);

                                if (newActivityOffering != null)
                                {
                                    activityOfferingsToAdd.Add(newActivityOffering);
                                }
                            }
                        }
                    }
                }

                zopoesht2Context.Activityofferings.AddRange(activityOfferingsToAdd);
                zopoesht2Context.SaveChanges();

                zopoeshtTransaction.Commit();
            }
        }

        private static Activityoffering? AddNewActivityOffering(DbContexts.MosvEcologicalDamage.Models.Activity activityEntity, int operatorId, List<DbContexts.Zopoesht2.Models.Activity> activities, List<Subactivity> subActivities, List<Authority> authorities, List<DbContexts.Zopoesht2.Models.Terrain> terrains)
        {
            var migratedActivity = activities.SingleOrDefault(e => e.Id == activityEntity.ActivityType.ParentId);

            var migratedSubActivity = subActivities.SingleOrDefault(e => e.Code == activityEntity.ActivityType.Code);

            if (migratedActivity != null && migratedSubActivity != null)
            {
                var authorityBasinId = authorities.SingleOrDefault(e => e.Migrationid == activityEntity.AdministrativeUnitBasein?.Id)?.Id;
                var authorityRiosvId = authorities.SingleOrDefault(e => e.Migrationid == activityEntity.AdministrativeUnitRiosv?.Id)?.Id;
                var terrainId = terrains.SingleOrDefault(e => e.Migrationid == activityEntity.TerrainInitialPartId)?.Id;

                var newActivityOffering = new Activityoffering
                {
                    Activityid = migratedActivity.Id,
                    Subactivityid = migratedSubActivity.Id,
                    Operatorid = operatorId,
                    Authoritybasinid = authorityBasinId != null ? authorityBasinId : null,
                    Authorityriosvid = authorityRiosvId != null ? authorityRiosvId : null,
                    Terrainid = migratedActivity != null ? terrainId : null
                };

                return newActivityOffering;
            }


            return null;
        }
    }
}
