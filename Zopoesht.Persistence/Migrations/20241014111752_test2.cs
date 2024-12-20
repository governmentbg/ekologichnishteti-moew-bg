using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zopoesht.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bankguranteeid",
                table: "applicationtwo");

            migrationBuilder.RenameColumn(
                name: "guranteestart",
                table: "bankguarantee",
                newName: "guaranteestart");

            migrationBuilder.RenameColumn(
                name: "guranteenumber",
                table: "bankguarantee",
                newName: "guaranteenumber");

            migrationBuilder.RenameColumn(
                name: "guranteeend",
                table: "bankguarantee",
                newName: "guaranteeend");

            migrationBuilder.RenameColumn(
                name: "guranteedate",
                table: "bankguarantee",
                newName: "guaranteedate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "guaranteestart",
                table: "bankguarantee",
                newName: "guranteestart");

            migrationBuilder.RenameColumn(
                name: "guaranteenumber",
                table: "bankguarantee",
                newName: "guranteenumber");

            migrationBuilder.RenameColumn(
                name: "guaranteeend",
                table: "bankguarantee",
                newName: "guranteeend");

            migrationBuilder.RenameColumn(
                name: "guaranteedate",
                table: "bankguarantee",
                newName: "guranteedate");

            migrationBuilder.AddColumn<int>(
                name: "bankguranteeid",
                table: "applicationtwo",
                type: "integer",
                nullable: true);
        }
    }
}
