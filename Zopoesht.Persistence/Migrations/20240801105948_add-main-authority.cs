using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addmainauthority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "startedcurrentapplicationone",
                table: "applicationoneauthority");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "startedcurrentapplicationone",
                table: "applicationoneauthority",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
