using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addactivityofferingtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "activityofferingtype",
                table: "applicationtwo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "diffusenaturedescription",
                table: "applicationtwo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "notlisteddescription",
                table: "applicationtwo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "activityofferingtype",
                table: "applicationonebasic",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "diffusenaturedescription",
                table: "applicationonebasic",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "notlisteddescription",
                table: "applicationonebasic",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "activityofferingtype",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "diffusenaturedescription",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "notlisteddescription",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "activityofferingtype",
                table: "applicationonebasic");

            migrationBuilder.DropColumn(
                name: "diffusenaturedescription",
                table: "applicationonebasic");

            migrationBuilder.DropColumn(
                name: "notlisteddescription",
                table: "applicationonebasic");
        }
    }
}
