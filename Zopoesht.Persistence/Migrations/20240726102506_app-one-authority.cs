using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class apponeauthority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicant_authority_authorityid",
                table: "applicant");

            migrationBuilder.DropIndex(
                name: "IX_applicant_authorityid",
                table: "applicant");

            migrationBuilder.DropColumn(
                name: "authorityid",
                table: "applicant");

            migrationBuilder.CreateTable(
                name: "applicationoneauthority",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationoneid = table.Column<int>(type: "integer", nullable: false),
                    authorityid = table.Column<int>(type: "integer", nullable: false),
                    startedcurrentapplicationone = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationoneauthority", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationoneauthority_applicationone_applicationoneid",
                        column: x => x.applicationoneid,
                        principalTable: "applicationone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationoneauthority_authority_authorityid",
                        column: x => x.authorityid,
                        principalTable: "authority",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationoneauthority_applicationoneid",
                table: "applicationoneauthority",
                column: "applicationoneid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationoneauthority_authorityid",
                table: "applicationoneauthority",
                column: "authorityid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationoneauthority");

            migrationBuilder.AddColumn<int>(
                name: "authorityid",
                table: "applicant",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_applicant_authorityid",
                table: "applicant",
                column: "authorityid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicant_authority_authorityid",
                table: "applicant",
                column: "authorityid",
                principalTable: "authority",
                principalColumn: "id");
        }
    }
}
