using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sakkinny.Migrations
{
    /// <inheritdoc />
    public partial class rentWithList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRented",
                table: "Apartments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRented",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
