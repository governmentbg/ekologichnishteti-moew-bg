using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addfieldstoapptwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "actionstaken",
                table: "applicationtwo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "occurrencedatedescription",
                table: "applicationtwo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "procedureinitiateddatedescription",
                table: "applicationtwo",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_annualadministrativeexpenseshistory_annualadministrativeexp~",
                table: "annualadministrativeexpenseshistory",
                column: "annualadministrativeexpenseid");

            migrationBuilder.CreateIndex(
                name: "IX_annualadministrativeexpenseshistory_userid",
                table: "annualadministrativeexpenseshistory",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_annualadministrativeexpenseshistory_annualadministrativeexp~",
                table: "annualadministrativeexpenseshistory",
                column: "annualadministrativeexpenseid",
                principalTable: "annualadministrativeexpenses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_annualadministrativeexpenseshistory_user_userid",
                table: "annualadministrativeexpenseshistory",
                column: "userid",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_annualadministrativeexpenseshistory_annualadministrativeexp~",
                table: "annualadministrativeexpenseshistory");

            migrationBuilder.DropForeignKey(
                name: "FK_annualadministrativeexpenseshistory_user_userid",
                table: "annualadministrativeexpenseshistory");

            migrationBuilder.DropIndex(
                name: "IX_annualadministrativeexpenseshistory_annualadministrativeexp~",
                table: "annualadministrativeexpenseshistory");

            migrationBuilder.DropIndex(
                name: "IX_annualadministrativeexpenseshistory_userid",
                table: "annualadministrativeexpenseshistory");

            migrationBuilder.DropColumn(
                name: "actionstaken",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "occurrencedatedescription",
                table: "applicationtwo");

            migrationBuilder.DropColumn(
                name: "procedureinitiateddatedescription",
                table: "applicationtwo");
        }
    }
}
