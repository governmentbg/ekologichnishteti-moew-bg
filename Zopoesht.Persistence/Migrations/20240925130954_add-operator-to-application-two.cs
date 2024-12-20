using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addoperatortoapplicationtwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "operatorid",
                table: "applicationtwo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwo_operatorid",
                table: "applicationtwo",
                column: "operatorid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationtwo_operator_operatorid",
                table: "applicationtwo",
                column: "operatorid",
                principalTable: "operator",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationtwo_operator_operatorid",
                table: "applicationtwo");

            migrationBuilder.DropIndex(
                name: "IX_applicationtwo_operatorid",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "operatorid",
                table: "applicationtwo");
        }
    }
}
