using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addapplicationoneactivityoffering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "activityofferingid",
                table: "applicationonebasic",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_applicationonebasic_activityofferingid",
                table: "applicationonebasic",
                column: "activityofferingid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationonebasic_activityoffering_activityofferingid",
                table: "applicationonebasic",
                column: "activityofferingid",
                principalTable: "activityoffering",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationonebasic_activityoffering_activityofferingid",
                table: "applicationonebasic");

            migrationBuilder.DropIndex(
                name: "IX_applicationonebasic_activityofferingid",
                table: "applicationonebasic");

            migrationBuilder.DropColumn(
                name: "activityofferingid",
                table: "applicationonebasic");
        }
    }
}
