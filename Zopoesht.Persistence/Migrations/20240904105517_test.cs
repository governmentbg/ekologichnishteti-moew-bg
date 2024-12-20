using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationone_applicationonebasic_applicationonebasicid",
                table: "applicationone");

            migrationBuilder.DropIndex(
                name: "IX_applicationone_applicationonebasicid",
                table: "applicationone");

            migrationBuilder.DropColumn(
                name: "applicationonebasicid",
                table: "applicationone");

            migrationBuilder.AddColumn<int>(
                name: "applicationoneid",
                table: "applicationonebasic",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_applicationonebasic_applicationoneid",
                table: "applicationonebasic",
                column: "applicationoneid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationonebasic_applicationone_applicationoneid",
                table: "applicationonebasic",
                column: "applicationoneid",
                principalTable: "applicationone",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationonebasic_applicationone_applicationoneid",
                table: "applicationonebasic");

            migrationBuilder.DropIndex(
                name: "IX_applicationonebasic_applicationoneid",
                table: "applicationonebasic");

            migrationBuilder.DropColumn(
                name: "applicationoneid",
                table: "applicationonebasic");

            migrationBuilder.AddColumn<int>(
                name: "applicationonebasicid",
                table: "applicationone",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_applicationone_applicationonebasicid",
                table: "applicationone",
                column: "applicationonebasicid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationone_applicationonebasic_applicationonebasicid",
                table: "applicationone",
                column: "applicationonebasicid",
                principalTable: "applicationonebasic",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
