using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addaltfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "actionstakenalt",
                table: "applicationtwo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "occurrencedatedescriptionalt",
                table: "applicationtwo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "procedureinitiateddatedescriptionalt",
                table: "applicationtwo",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "actionstakenalt",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "occurrencedatedescriptionalt",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "procedureinitiateddatedescriptionalt",
                table: "applicationtwo");
        }
    }
}
