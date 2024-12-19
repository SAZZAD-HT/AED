using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionManagement_System.Migrations
{
    /// <inheritdoc />
    public partial class jdddcreh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "ProductTables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductTables");
        }
    }
}
