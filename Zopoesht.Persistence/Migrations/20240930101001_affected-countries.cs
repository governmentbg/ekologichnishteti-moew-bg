using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class affectedcountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationonebasic_country_countryid",
                table: "applicationonebasic");

            migrationBuilder.DropColumn(
                name: "countryauthority",
                table: "applicationonebasic");

            migrationBuilder.RenameColumn(
                name: "countryid",
                table: "applicationonebasic",
                newName: "culpritcountryid");

            migrationBuilder.RenameIndex(
                name: "IX_applicationonebasic_countryid",
                table: "applicationonebasic",
                newName: "IX_applicationonebasic_culpritcountryid");

            migrationBuilder.CreateTable(
                name: "applicationoneaffectedcountry",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationonebasicid = table.Column<int>(type: "integer", nullable: false),
                    countryid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationoneaffectedcountry", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationoneaffectedcountry_applicationonebasic_applicati~",
                        column: x => x.applicationonebasicid,
                        principalTable: "applicationonebasic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationoneaffectedcountry_country_countryid",
                        column: x => x.countryid,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationoneaffectedcountry_applicationonebasicid",
                table: "applicationoneaffectedcountry",
                column: "applicationonebasicid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationoneaffectedcountry_countryid",
                table: "applicationoneaffectedcountry",
                column: "countryid");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationonebasic_country_culpritcountryid",
                table: "applicationonebasic",
                column: "culpritcountryid",
                principalTable: "country",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationonebasic_country_culpritcountryid",
                table: "applicationonebasic");

            migrationBuilder.DropTable(
                name: "applicationoneaffectedcountry");

            migrationBuilder.RenameColumn(
                name: "culpritcountryid",
                table: "applicationonebasic",
                newName: "countryid");

            migrationBuilder.RenameIndex(
                name: "IX_applicationonebasic_culpritcountryid",
                table: "applicationonebasic",
                newName: "IX_applicationonebasic_countryid");

            migrationBuilder.AddColumn<string>(
                name: "countryauthority",
                table: "applicationonebasic",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_applicationonebasic_country_countryid",
                table: "applicationonebasic",
                column: "countryid",
                principalTable: "country",
                principalColumn: "id");
        }
    }
}
