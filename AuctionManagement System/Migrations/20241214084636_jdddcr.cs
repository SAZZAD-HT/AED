using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionManagement_System.Migrations
{
    /// <inheritdoc />
    public partial class jdddcr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "ItemTables");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ItemTables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ItemTables");

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                table: "ItemTables",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
