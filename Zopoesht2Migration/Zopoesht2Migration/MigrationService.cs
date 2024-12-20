using Zopoesht2Migration.Migrations;

namespace Zopoesht2Migration
{
    public class MigrationService
    {
        public static void StartMigration()
        {
            OperatorsMigrationService.Start();
            TerrainsMigrationService.Start();
            ActivityOfferingsMigrationService.Start();
        }
    }
}
