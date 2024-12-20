using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeperiodyearstodates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_period_year_endyearid",
                table: "period");

            migrationBuilder.DropForeignKey(
                name: "FK_period_year_startyearid",
                table: "period");

            migrationBuilder.DropIndex(
                name: "IX_period_endyearid",
                table: "period");

            migrationBuilder.DropIndex(
                name: "IX_period_startyearid",
                table: "period");

            migrationBuilder.DropColumn(
                name: "endyearid",
                table: "period");

            migrationBuilder.DropColumn(
                name: "startyearid",
                table: "period");

            migrationBuilder.AddColumn<DateTime>(
                name: "enddate",
                table: "period",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "startdate",
                table: "period",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "enddate",
                table: "period");

            migrationBuilder.DropColumn(
                name: "startdate",
                table: "period");

            migrationBuilder.AddColumn<int>(
                name: "endyearid",
                table: "period",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "startyearid",
                table: "period",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_period_endyearid",
                table: "period",
                column: "endyearid");

            migrationBuilder.CreateIndex(
                name: "IX_period_startyearid",
                table: "period",
                column: "startyearid");

            migrationBuilder.AddForeignKey(
                name: "FK_period_year_endyearid",
                table: "period",
                column: "endyearid",
                principalTable: "year",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_period_year_startyearid",
                table: "period",
                column: "startyearid",
                principalTable: "year",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
