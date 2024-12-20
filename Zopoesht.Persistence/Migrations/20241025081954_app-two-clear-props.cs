using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class apptwoclearprops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "damageconfirmeddate",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "damageoccurrencedate",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "hassoildamagedamage",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "hassoildamagethreat",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "hasspeciesdamagedamage",
                table: "applicationtwo");

            migrationBuilder.RenameColumn(
                name: "threatoccurrencedate",
                table: "applicationtwo",
                newName: "occurrencedate");

            migrationBuilder.RenameColumn(
                name: "threatconfirmeddate",
                table: "applicationtwo",
                newName: "confirmeddate");

            migrationBuilder.RenameColumn(
                name: "haswaterdamagethreat",
                table: "applicationtwo",
                newName: "haswaterdamage");

            migrationBuilder.RenameColumn(
                name: "haswaterdamagedamage",
                table: "applicationtwo",
                newName: "hasspeciesdamage");

            migrationBuilder.RenameColumn(
                name: "hasspeciesdamagethreat",
                table: "applicationtwo",
                newName: "hassoildamage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "occurrencedate",
                table: "applicationtwo",
                newName: "threatoccurrencedate");

            migrationBuilder.RenameColumn(
                name: "haswaterdamage",
                table: "applicationtwo",
                newName: "haswaterdamagethreat");

            migrationBuilder.RenameColumn(
                name: "hasspeciesdamage",
                table: "applicationtwo",
                newName: "haswaterdamagedamage");

            migrationBuilder.RenameColumn(
                name: "hassoildamage",
                table: "applicationtwo",
                newName: "hasspeciesdamagethreat");

            migrationBuilder.RenameColumn(
                name: "confirmeddate",
                table: "applicationtwo",
                newName: "threatconfirmeddate");

            migrationBuilder.AddColumn<DateTime>(
                name: "damageconfirmeddate",
                table: "applicationtwo",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "damageoccurrencedate",
                table: "applicationtwo",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "hassoildamagedamage",
                table: "applicationtwo",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "hassoildamagethreat",
                table: "applicationtwo",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "hasspeciesdamagedamage",
                table: "applicationtwo",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
