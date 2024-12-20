using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addfinancialassurance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "bankguaranteeid",
                table: "applicationtwo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "bankguranteeid",
                table: "applicationtwo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "financialassurancetype",
                table: "applicationtwo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "insurancepolicyid",
                table: "applicationtwo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "mortgageid",
                table: "applicationtwo",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "stakeid",
                table: "applicationtwo",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "bankguarantee",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bankname = table.Column<string>(type: "text", nullable: true),
                    guranteenumber = table.Column<string>(type: "text", nullable: true),
                    guranteedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    guranteestart = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    guranteeend = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    additionalinformation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bankguarantee", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "insurancepolicy",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    companyname = table.Column<string>(type: "text", nullable: true),
                    policynumber = table.Column<string>(type: "text", nullable: true),
                    policydate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    insurancestart = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    insuranceend = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    responsibilitylimit = table.Column<decimal>(type: "numeric", nullable: false),
                    additionalinformation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurancepolicy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mortgage",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mortgagetype = table.Column<int>(type: "integer", nullable: false),
                    mortgagenumber = table.Column<string>(type: "text", nullable: true),
                    mortgagedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    mortgagedescription = table.Column<string>(type: "text", nullable: true),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    additionalinformation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mortgage", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stake",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stakenumber = table.Column<string>(type: "text", nullable: true),
                    stakedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    stakedescription = table.Column<string>(type: "text", nullable: true),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    additionalinformation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stake", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwo_bankguaranteeid",
                table: "applicationtwo",
                column: "bankguaranteeid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwo_insurancepolicyid",
                table: "applicationtwo",
                column: "insurancepolicyid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwo_mortgageid",
                table: "applicationtwo",
                column: "mortgageid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationtwo_stakeid",
                table: "applicationtwo",
                column: "stakeid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationtwo_bankguarantee_bankguaranteeid",
                table: "applicationtwo",
                column: "bankguaranteeid",
                principalTable: "bankguarantee",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationtwo_insurancepolicy_insurancepolicyid",
                table: "applicationtwo",
                column: "insurancepolicyid",
                principalTable: "insurancepolicy",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationtwo_mortgage_mortgageid",
                table: "applicationtwo",
                column: "mortgageid",
                principalTable: "mortgage",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationtwo_stake_stakeid",
                table: "applicationtwo",
                column: "stakeid",
                principalTable: "stake",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationtwo_bankguarantee_bankguaranteeid",
                table: "applicationtwo");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationtwo_insurancepolicy_insurancepolicyid",
                table: "applicationtwo");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationtwo_mortgage_mortgageid",
                table: "applicationtwo");

            migrationBuilder.DropForeignKey(
                name: "FK_applicationtwo_stake_stakeid",
                table: "applicationtwo");

            migrationBuilder.DropTable(
                name: "bankguarantee");

            migrationBuilder.DropTable(
                name: "insurancepolicy");

            migrationBuilder.DropTable(
                name: "mortgage");

            migrationBuilder.DropTable(
                name: "stake");

            migrationBuilder.DropIndex(
                name: "IX_applicationtwo_bankguaranteeid",
                table: "applicationtwo");

            migrationBuilder.DropIndex(
                name: "IX_applicationtwo_insurancepolicyid",
                table: "applicationtwo");

            migrationBuilder.DropIndex(
                name: "IX_applicationtwo_mortgageid",
                table: "applicationtwo");

            migrationBuilder.DropIndex(
                name: "IX_applicationtwo_stakeid",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "bankguaranteeid",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "bankguranteeid",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "financialassurancetype",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "insurancepolicyid",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "mortgageid",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "stakeid",
                table: "applicationtwo");
        }
    }
}
