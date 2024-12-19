using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionManagement_System.Migrations
{
    /// <inheritdoc />
    public partial class jdddcre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "ItemTables");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "ItemTables",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
