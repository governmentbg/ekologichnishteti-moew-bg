using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addapplicationoneactivityofferingtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "applicationoneactivityoffering",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicationonebasicid = table.Column<int>(type: "integer", nullable: false),
                    activityofferingid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationoneactivityoffering", x => x.id);
                    table.ForeignKey(
                        name: "FK_applicationoneactivityoffering_activityoffering_activityoff~",
                        column: x => x.activityofferingid,
                        principalTable: "activityoffering",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_applicationoneactivityoffering_applicationonebasic_applicat~",
                        column: x => x.applicationonebasicid,
                        principalTable: "applicationonebasic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationoneactivityoffering_activityofferingid",
                table: "applicationoneactivityoffering",
                column: "activityofferingid");

            migrationBuilder.CreateIndex(
                name: "IX_applicationoneactivityoffering_applicationonebasicid",
                table: "applicationoneactivityoffering",
                column: "applicationonebasicid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationoneactivityoffering");

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
    }
}
