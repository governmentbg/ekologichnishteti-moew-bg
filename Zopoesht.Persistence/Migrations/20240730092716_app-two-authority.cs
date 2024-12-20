using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class apptwoauthority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicationtwoauthority",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationtwoid = table.Column<int>(type: "integer", nullable: false),
                    authorityid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationtwoauthority", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationtwoauthority_applicationtwo_applicationtwoid",
                        column: x => x.applicationtwoid,
                        principalTable: "applicationtwo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationtwoauthority_authority_authorityid",
                        column: x => x.authorityid,
                        principalTable: "authority",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwoauthority_applicationtwoid",
                table: "applicationtwoauthority",
                column: "applicationtwoid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwoauthority_authorityid",
                table: "applicationtwoauthority",
                column: "authorityid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationtwoauthority");
        }
    }
}
