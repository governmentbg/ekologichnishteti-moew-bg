using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class maketerrainnomenclature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "alias",
                table: "terrain",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isactive",
                table: "terrain",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "namealt",
                table: "terrain",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "version",
                table: "terrain",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "vieworder",
                table: "terrain",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "alias",
                table: "terrain");

            migrationBuilder.DropColumn(
                name: "isactive",
                table: "terrain");

            migrationBuilder.DropColumn(
                name: "namealt",
                table: "terrain");

            migrationBuilder.DropColumn(
                name: "version",
                table: "terrain");

            migrationBuilder.DropColumn(
                name: "vieworder",
                table: "terrain");
        }
    }
}
