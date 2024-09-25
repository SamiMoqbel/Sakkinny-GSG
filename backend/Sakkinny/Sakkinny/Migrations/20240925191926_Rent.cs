using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sakkinny.Migrations
{
    /// <inheritdoc />
    public partial class Rent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Apartments",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "subTitle",
                table: "Apartments",
                newName: "SubTitle");

            migrationBuilder.RenameColumn(
                name: "roomsNumber",
                table: "Apartments",
                newName: "RoomsNumber");

            migrationBuilder.RenameColumn(
                name: "roomsAvailable",
                table: "Apartments",
                newName: "RoomsAvailable");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Apartments",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "Apartments",
                newName: "Location");

            migrationBuilder.AlterColumn<int>(
                name: "RoomsAvailable",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Apartments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "IsRented",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentalEndDate",
                table: "Apartments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentalStartDate",
                table: "Apartments",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRented",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "RentalEndDate",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "RentalStartDate",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Apartments",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "SubTitle",
                table: "Apartments",
                newName: "subTitle");

            migrationBuilder.RenameColumn(
                name: "RoomsNumber",
                table: "Apartments",
                newName: "roomsNumber");

            migrationBuilder.RenameColumn(
                name: "RoomsAvailable",
                table: "Apartments",
                newName: "roomsAvailable");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Apartments",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Apartments",
                newName: "location");

            migrationBuilder.AlterColumn<int>(
                name: "roomsAvailable",
                table: "Apartments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Apartments",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
