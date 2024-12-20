using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addterrainstooperator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "operatorid",
                table: "terrain",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_terrain_operatorid",
                table: "terrain",
                column: "operatorid");

            migrationBuilder.AddForeignKey(
                name: "FK_terrain_operator_operatorid",
                table: "terrain",
                column: "operatorid",
                principalTable: "operator",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_terrain_operator_operatorid",
                table: "terrain");

            migrationBuilder.DropIndex(
                name: "IX_terrain_operatorid",
                table: "terrain");

            migrationBuilder.DropColumn(
                name: "operatorid",
                table: "terrain");
        }
    }
}
