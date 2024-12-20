using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class apptwostateconnections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "proceedinginfo",
                table: "applicationtwo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "proceedinginfoabsence",
                table: "applicationtwo",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "proceedingtype",
                table: "applicationtwo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "applicationtwoadministrativecourt",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationtwoid = table.Column<int>(type: "integer", nullable: false),
                    administrativecourtid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationtwoadministrativecourt", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationtwoadministrativecourt_administrativecourt_admin~",
                        column: x => x.administrativecourtid,
                        principalTable: "administrativecourt",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationtwoadministrativecourt_applicationtwo_applicatio~",
                        column: x => x.applicationtwoid,
                        principalTable: "applicationtwo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "applicationtwoministryofinterior",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationtwoid = table.Column<int>(type: "integer", nullable: false),
                    ministryofinteriorid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationtwoministryofinterior", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationtwoministryofinterior_applicationtwo_application~",
                        column: x => x.applicationtwoid,
                        principalTable: "applicationtwo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationtwoministryofinterior_ministryofinterior_ministr~",
                        column: x => x.ministryofinteriorid,
                        principalTable: "ministryofinterior",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "applicationtwoprosecutor",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationtwoid = table.Column<int>(type: "integer", nullable: false),
                    prosecutorid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationtwoprosecutor", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationtwoprosecutor_applicationtwo_applicationtwoid",
                        column: x => x.applicationtwoid,
                        principalTable: "applicationtwo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationtwoprosecutor_prosecutor_prosecutorid",
                        column: x => x.prosecutorid,
                        principalTable: "prosecutor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwoadministrativecourt_administrativecourtid",
                table: "applicationtwoadministrativecourt",
                column: "administrativecourtid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwoadministrativecourt_applicationtwoid",
                table: "applicationtwoadministrativecourt",
                column: "applicationtwoid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwoministryofinterior_applicationtwoid",
                table: "applicationtwoministryofinterior",
                column: "applicationtwoid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwoministryofinterior_ministryofinteriorid",
                table: "applicationtwoministryofinterior",
                column: "ministryofinteriorid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwoprosecutor_applicationtwoid",
                table: "applicationtwoprosecutor",
                column: "applicationtwoid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwoprosecutor_prosecutorid",
                table: "applicationtwoprosecutor",
                column: "prosecutorid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationtwoadministrativecourt");

            migrationBuilder.DropTable(
                name: "applicationtwoministryofinterior");

            migrationBuilder.DropTable(
                name: "applicationtwoprosecutor");

            migrationBuilder.DropColumn(
                name: "proceedinginfo",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "proceedinginfoabsence",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "proceedingtype",
                table: "applicationtwo");
        }
    }
}
