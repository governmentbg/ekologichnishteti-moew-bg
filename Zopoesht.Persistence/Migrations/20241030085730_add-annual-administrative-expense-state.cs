using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addannualadministrativeexpensestate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "state",
                table: "annualadministrativeexpenses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "state",
                table: "annualadministrativeexpenses");
        }
    }
}
