using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addannualadministrativeexpenseshistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rootid",
                table: "annualadministrativeexpenses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "annualadministrativeexpenseshistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    userfullname = table.Column<string>(type: "text", nullable: true),
                    modificationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    annualadministrativeexpenseid = table.Column<int>(type: "integer", nullable: false),
                    rootid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_annualadministrativeexpenseshistory", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "annualadministrativeexpenseshistory");

            migrationBuilder.DropColumn(
                name: "rootid",
                table: "annualadministrativeexpenses");
        }
    }
}
