using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addapplicationtwoactivityofferingtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicationtwoactivityoffering",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationtwoid = table.Column<int>(type: "integer", nullable: false),
                    activityofferingid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationtwoactivityoffering", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationtwoactivityoffering_activityoffering_activityoff~",
                        column: x => x.activityofferingid,
                        principalTable: "activityoffering",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationtwoactivityoffering_applicationtwo_applicationtw~",
                        column: x => x.applicationtwoid,
                        principalTable: "applicationtwo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwoactivityoffering_activityofferingid",
                table: "applicationtwoactivityoffering",
                column: "activityofferingid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwoactivityoffering_applicationtwoid",
                table: "applicationtwoactivityoffering",
                column: "applicationtwoid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationtwoactivityoffering");
        }
    }
}
