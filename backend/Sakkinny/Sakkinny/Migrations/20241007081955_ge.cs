using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sakkinny.Migrations
{
    /// <inheritdoc />
    public partial class ge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "RentalEndDate",
                table: "Renters");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Renters",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Renters_UserId",
                table: "Renters",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Renters_Users_UserId",
                table: "Renters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Renters_Users_UserId",
                table: "Renters");

            migrationBuilder.DropIndex(
                name: "IX_Renters_UserId",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Renters");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Renters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RentalEndDate",
                table: "Renters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
