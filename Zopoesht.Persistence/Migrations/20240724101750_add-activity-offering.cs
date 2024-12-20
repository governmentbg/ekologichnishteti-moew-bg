using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addactivityoffering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_operator_administrativeunit_administrativeunitbasinid",
                table: "operator");

            migrationBuilder.DropForeignKey(
                name: "FK_operator_administrativeunit_administrativeunitriosvid",
                table: "operator");

            migrationBuilder.DropForeignKey(
                name: "FK_terrain_operator_operatorid",
                table: "terrain");

            migrationBuilder.DropTable(
                name: "administrativeunit");

            migrationBuilder.DropIndex(
                name: "IX_terrain_operatorid",
                table: "terrain");

            migrationBuilder.DropIndex(
                name: "IX_operator_administrativeunitbasinid",
                table: "operator");

            migrationBuilder.DropIndex(
                name: "IX_operator_administrativeunitriosvid",
                table: "operator");

            migrationBuilder.DropColumn(
                name: "operatorid",
                table: "terrain");

            migrationBuilder.DropColumn(
                name: "administrativeunitbasinid",
                table: "operator");

            migrationBuilder.DropColumn(
                name: "administrativeunitriosvid",
                table: "operator");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "subactivity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "authoritytype",
                table: "authority",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "activity",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "activityoffering",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    activityid = table.Column<int>(type: "integer", nullable: true),
                    subactivityid = table.Column<int>(type: "integer", nullable: true),
                    operatorid = table.Column<int>(type: "integer", nullable: true),
                    terrainid = table.Column<int>(type: "integer", nullable: true),
                    authorityriosvid = table.Column<int>(type: "integer", nullable: true),
                    authoritybasinid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activityoffering", x => x.id);
                    table.ForeignKey(
                        name: "FK_activityoffering_activity_activityid",
                        column: x => x.activityid,
                        principalTable: "activity",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_activityoffering_authority_authoritybasinid",
                        column: x => x.authoritybasinid,
                        principalTable: "authority",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_activityoffering_authority_authorityriosvid",
                        column: x => x.authorityriosvid,
                        principalTable: "authority",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_activityoffering_operator_operatorid",
                        column: x => x.operatorid,
                        principalTable: "operator",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_activityoffering_subactivity_subactivityid",
                        column: x => x.subactivityid,
                        principalTable: "subactivity",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_activityoffering_terrain_terrainid",
                        column: x => x.terrainid,
                        principalTable: "terrain",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_activityoffering_activityid",
                table: "activityoffering",
                column: "activityid");

            migrationBuilder.CreateIndex(
                name: "IX_activityoffering_authoritybasinid",
                table: "activityoffering",
                column: "authoritybasinid");

            migrationBuilder.CreateIndex(
                name: "IX_activityoffering_authorityriosvid",
                table: "activityoffering",
                column: "authorityriosvid");

            migrationBuilder.CreateIndex(
                name: "IX_activityoffering_operatorid",
                table: "activityoffering",
                column: "operatorid");

            migrationBuilder.CreateIndex(
                name: "IX_activityoffering_subactivityid",
                table: "activityoffering",
                column: "subactivityid");

            migrationBuilder.CreateIndex(
                name: "IX_activityoffering_terrainid",
                table: "activityoffering",
                column: "terrainid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activityoffering");

            migrationBuilder.DropColumn(
                name: "code",
                table: "subactivity");

            migrationBuilder.DropColumn(
                name: "authoritytype",
                table: "authority");

            migrationBuilder.DropColumn(
                name: "code",
                table: "activity");

            migrationBuilder.AddColumn<int>(
                name: "operatorid",
                table: "terrain",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "administrativeunitbasinid",
                table: "operator",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "administrativeunitriosvid",
                table: "operator",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "administrativeunit",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    administrativeunittype = table.Column<int>(type: "integer", nullable: false),
                    alias = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    vieworder = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrativeunit", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_terrain_operatorid",
                table: "terrain",
                column: "operatorid");

            migrationBuilder.CreateIndex(
                name: "IX_operator_administrativeunitbasinid",
                table: "operator",
                column: "administrativeunitbasinid");

            migrationBuilder.CreateIndex(
                name: "IX_operator_administrativeunitriosvid",
                table: "operator",
                column: "administrativeunitriosvid");

            migrationBuilder.AddForeignKey(
                name: "FK_operator_administrativeunit_administrativeunitbasinid",
                table: "operator",
                column: "administrativeunitbasinid",
                principalTable: "administrativeunit",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_operator_administrativeunit_administrativeunitriosvid",
                table: "operator",
                column: "administrativeunitriosvid",
                principalTable: "administrativeunit",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_terrain_operator_operatorid",
                table: "terrain",
                column: "operatorid",
                principalTable: "operator",
                principalColumn: "id");
        }
    }
}
