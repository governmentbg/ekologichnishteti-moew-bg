using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addyearperiodannualexpencestables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "year",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_year", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "period",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    startyearid = table.Column<int>(type: "integer", nullable: false),
                    endyearid = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_period", x => x.id);
                    table.ForeignKey(
                        name: "FK_period_year_endyearid",
                        column: x => x.endyearid,
                        principalTable: "year",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_period_year_startyearid",
                        column: x => x.startyearid,
                        principalTable: "year",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "annualadministrativeexpenses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    creatoruserid = table.Column<int>(type: "integer", nullable: false),
                    authorityid = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    periodid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_annualadministrativeexpenses", x => x.id);
                    table.ForeignKey(
                        name: "FK_annualadministrativeexpenses_authority_authorityid",
                        column: x => x.authorityid,
                        principalTable: "authority",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_annualadministrativeexpenses_period_periodid",
                        column: x => x.periodid,
                        principalTable: "period",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_annualadministrativeexpenses_authorityid",
                table: "annualadministrativeexpenses",
                column: "authorityid");

            migrationBuilder.CreateIndex(
                name: "IX_annualadministrativeexpenses_periodid",
                table: "annualadministrativeexpenses",
                column: "periodid");

            migrationBuilder.CreateIndex(
                name: "IX_period_endyearid",
                table: "period",
                column: "endyearid");

            migrationBuilder.CreateIndex(
                name: "IX_period_startyearid",
                table: "period",
                column: "startyearid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "annualadministrativeexpenses");

            migrationBuilder.DropTable(
                name: "period");

            migrationBuilder.DropTable(
                name: "year");
        }
    }
}
