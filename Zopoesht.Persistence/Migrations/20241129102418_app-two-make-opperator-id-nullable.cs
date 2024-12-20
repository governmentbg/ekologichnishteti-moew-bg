using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class apptwomakeopperatoridnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationtwo_operator_operatorid",
                table: "applicationtwo");

            migrationBuilder.AlterColumn<int>(
                name: "operatorid",
                table: "applicationtwo",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationtwo_operator_operatorid",
                table: "applicationtwo",
                column: "operatorid",
                principalTable: "operator",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationtwo_operator_operatorid",
                table: "applicationtwo");

            migrationBuilder.AlterColumn<int>(
                name: "operatorid",
                table: "applicationtwo",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationtwo_operator_operatorid",
                table: "applicationtwo",
                column: "operatorid",
                principalTable: "operator",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
