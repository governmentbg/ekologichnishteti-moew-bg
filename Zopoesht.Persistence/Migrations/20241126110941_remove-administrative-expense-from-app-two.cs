using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removeadministrativeexpensefromapptwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "administrativeexpense",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "hasadministrativeexpense",
                table: "applicationtwo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "administrativeexpense",
                table: "applicationtwo",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "hasadministrativeexpense",
                table: "applicationtwo",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
