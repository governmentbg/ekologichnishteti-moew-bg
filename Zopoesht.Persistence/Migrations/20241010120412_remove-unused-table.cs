using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removeunusedtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationtwosubactivity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicationtwosubactivity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationtwoid = table.Column<int>(type: "integer", nullable: false),
                    subactivityid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationtwosubactivity", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationtwosubactivity_applicationtwo_applicationtwoid",
                        column: x => x.applicationtwoid,
                        principalTable: "applicationtwo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationtwosubactivity_subactivity_subactivityid",
                        column: x => x.subactivityid,
                        principalTable: "subactivity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwosubactivity_applicationtwoid",
                table: "applicationtwosubactivity",
                column: "applicationtwoid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwosubactivity_subactivityid",
                table: "applicationtwosubactivity",
                column: "subactivityid");
        }
    }
}
